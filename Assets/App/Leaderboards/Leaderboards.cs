/*
 * API for accessing the leaderboards server.
 * Both synchronous (blocking) and asynchronous (non-blocking) methods are present.
 *
 * The API itself handles connecting and disconnecting, to keep things simple.
 *
 * The asynchronous methods take an Action object which will be invoked with the
 * server response.
 *
 * synchronous:
 *     SubmissionResponse SubmitScore(string level, Score score);
 *     ScoresResponse RequestScores(string level);
 *
 * asynchronous:
 *     void SubmitScoreAsync(string level, Score score,
 *              Action<SubmissionResponse, ServerException> callback);
 *     void RequestScoresAsync(string level,
 *              Action<ScoresResponse, ServerException> callback)
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.IO;

namespace xyz._8bITProject.cooperace.leaderboard {
	public class Leaderboards {

		// default host and port
		#if UNITY_EDITOR
		string host = "localhost"; // localhost in editor
		#else
		string host = "lb.cooperace.8bITProject.xyz"; // live server on build
		#endif
		int port = 2693;

		// networkStream, reader and writer all use lazy initialization to keep
		// connection code simple
		TcpClient client;
		NetworkStream networkStream_;
		NetworkStream networkStream {
			get {
				if (networkStream_ == null && client != null) {
					networkStream_ = client.GetStream();
				}
				return networkStream_;
			}
		}
		StreamReader reader_;
		StreamReader reader {
			get {
				if (reader_ == null) {
					reader_ = new StreamReader(networkStream);
				}
				return reader_;
			}
		}
		StreamWriter writer_;
		StreamWriter writer {
			get {
				if (writer_ == null) {
					writer_ = new StreamWriter(networkStream);
				}
				return writer_;
			}
		}

		// helper methods to operate on streams
		void WriteLine(string value) {
			try {
				writer.WriteLine(value);
				writer.Flush();
			} catch (IOException e) {
				throw new ServerException(e);
			}
		}
		string ReadLine() {
			try {
				return reader.ReadLine();
			} catch (IOException e) {
				throw new ServerException(e);
			}
		}

		// constructor can optionally override the default host and port
		public Leaderboards(string host=null, int? port=null) {
			if (host != null) {
				this.host = host;
			}
			if (port.HasValue) {
				this.port = port.Value;
			}
		}

		// synchronous connect
		void Connect() {
			try {
				client = new TcpClient();
				client.Connect(host, port);
			} catch (Exception e) {
				client = null;
				throw new ServerException(e);
			}
		}

		// asynchronous connect
		// state is an object used to pass information to the AsyncCallback
		void ConnectAsync(AsyncCallback callback, object state) {
			try {
				// TcpClient.BeginConnect doesn't throw an exception if the port
				// is invalid, so do it ourselves
				if (port < 0 || port > 65535) {
					throw new ArgumentOutOfRangeException("Invalid port");
				}

				client = new TcpClient();
				client.BeginConnect(host, port, callback, state);
			} catch (Exception e) {
				client = null;
				throw new ServerException(e);
			}
		}

		// synchronous disconnect
		// also responsible for closing all streams
		void Disconnect() {
			if (reader != null) {
				reader.Close();
				reader_ = null;
			}
			if (writer != null) {
				writer.Close();
				writer_ = null;
			}
			if (networkStream != null) {
				networkStream.Close();
				networkStream_ = null;
			}
			client.Close();
			client = null;
		}

		// asynchronous disconnect
		// same as the synchronous version, but also ends the callback
		void DisconnectAsync(IAsyncResult ar) {
			Disconnect();
			client.EndConnect(ar);
		}

		// synchronous submission api method
		public SubmissionResponse SubmitScore(string level, Score score) {
			// Ensure arguments are valid
			if (level == null) {
				throw new ArgumentNullException();
			}
			score.Validate();

			Connect();

			// form a submission message and send it to the server
			string request = SubmissionRequest.ToJson(level, score);
			WriteLine(request);

			// get the response and convert back to an object
			string response = ReadLine();
			SubmissionResponse position = SubmissionResponse.FromJson(response);

			Disconnect();

			// report back to user
			return position;
		}

		// asynchronous submission api method
		// most work is pushed off to an OnConnect callback
		public void SubmitScoreAsync(string level, Score score, Action<SubmissionResponse, ServerException> callback) {
			// Ensure arguments are valid
			if (level == null) {
				throw new ArgumentNullException();
			}
			score.Validate();

			// external callback and argument container
			SubmitScoreState state = new SubmitScoreState(level, score, callback);

			// make the async connection with the onconnect callback
			ConnectAsync(new AsyncCallback(SubmitScoreOnConnectCallback), state);
		}

		// OnConnect callback, the bulk of the work is handled here
		void SubmitScoreOnConnectCallback(IAsyncResult ar) {
			SubmitScoreState state = (SubmitScoreState) ar.AsyncState;

			// the arguments for the external callback
			SubmissionResponse position = new SubmissionResponse();
			ServerException error = null;

			// handle being unable to connect
			if (!client.Connected) {
				error = new ServerException("Unable to connect");
			} else {
				try {
					// form a submission message and send it to the server
					string request = SubmissionRequest.ToJson(state.level, state.score);
					WriteLine(request);

					// get a response from server and convert back to an object
					string response = ReadLine();
					position = SubmissionResponse.FromJson(response);
				} catch (ServerException e) {
					error = e;
				}
			}

			// report back to user
			if (state.callback != null) {
				state.callback.Invoke(position, error);
			}

			// note: even if connection failed, clean up is still necessary
			DisconnectAsync(ar);
		}

		// external callback and argument container for SubmitScoreAsync
		struct SubmitScoreState {
			public string level;
			public Score score;
			public Action<SubmissionResponse, ServerException> callback;
			public SubmitScoreState(string level, Score score, Action<SubmissionResponse, ServerException> callback) {
				this.level = level;
				this.score = score;
				this.callback = callback;
			}
		}

		// synchronous score request api method
		public ScoresResponse RequestScores(string level) {
			// Ensure arguments are valid
			if (level == null) {
				throw new ArgumentNullException();
			}

			Connect();

			// form a request message and send it to the server
			string request = ScoresRequest.ToJson(level);
			WriteLine(request);

			// get a response from server and convert back to an object
			string response = ReadLine();
			ScoresResponse scores = ScoresResponse.FromJson(response, level);

			Disconnect();

			// report back to user
			return scores;
		}

		// asynchronous score request api method
		// most work is pushed off to an OnConnect callback
		public void RequestScoresAsync(string level, Action<ScoresResponse, ServerException> callback) {
			// Ensure arguments are valid
			if (level == null) {
				throw new ArgumentNullException();
			}

			// external callback and argument container
			RequestScoresState state = new RequestScoresState(level, callback);

			// make the async connection with the onconnect callback
			ConnectAsync(new AsyncCallback(RequestScoresOnConnectCallback), state);
		}

		// OnConnect callback, the bulk of the work is handled here
		void RequestScoresOnConnectCallback(IAsyncResult ar) {
			RequestScoresState state = (RequestScoresState) ar.AsyncState;

			// the arguments for the external callback
			ServerException error = null;
			ScoresResponse scores = new ScoresResponse(state.level);

			// handle being unable to connect
			if (!client.Connected) {
				error = new ServerException("Unable to connect");
			} else {
				try {
					// form a request message and send it to the server
					string request = ScoresRequest.ToJson(state.level);
					WriteLine(request);

					// get a response from server and convert back to an object
					string response = ReadLine();
					scores = ScoresResponse.FromJson(response, state.level);
				} catch (ServerException e) {
					error = e;
				}
			}

			// report back to user
			if (state.callback != null) {
				state.callback.Invoke(scores, error);
			}

			// note: even if connection failed, clean up is still necessary
			DisconnectAsync(ar);
		}

		// external callback and argument container for RequestScoresAsync
		struct RequestScoresState {
			public string level;
			public Action<ScoresResponse, ServerException> callback;
			public RequestScoresState(string level, Action<ScoresResponse, ServerException> callback) {
				this.level = level;
				this.callback = callback;
			}
		}

	}

}
