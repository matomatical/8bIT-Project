/*
 * Provides an API for accessing the leaderboards server.
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

		#if UNITY_EDITOR
		string host = "localhost";
		#else
		string host = "lb.cooperace.8bITProject.xyz";
		#endif
		short port = 2693;

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

		public Leaderboards(string host=null, short? port=null) {
			if (host != null) {
				this.host = host;
			}	
			if (port.HasValue) {
				this.port = port.Value;
			}
		}

		void Connect() {
			try {
				client = new TcpClient();
				client.Connect(host, port);
			} catch (Exception e) {
				throw new ServerException(e);
			}
		}
		
		void ConnectAsync(AsyncCallback callback, object state) {
			try {
				client = new TcpClient();
				client.BeginConnect(host, port, callback, state);
			} catch (Exception e) {
				throw new ServerException(e);
			}
		}

		void Disconnect() {
			// close the connection
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
		void DisconnectAsync(IAsyncResult ar) {
			Disconnect();
			client.EndConnect(ar);
		}

		public SubmissionResponse SubmitScore(string level, Score score) {
			Connect();

			// form a submission message
			string request = SubmissionRequest.ToJson(level, score);

			// send it to the server
			WriteLine(request);
			
			// get a response
			string response = ReadLine();

			Disconnect();

			// convert back to an object and report to the user
			return SubmissionResponse.FromJson(response);
		}
		
		public void SubmitScoreAsync(string level, Score score, Action<SubmissionResponse, ServerException> callback) {
			SubmitScoreState state = new SubmitScoreState(level, score, callback);
			ConnectAsync(new AsyncCallback(SubmitScoreOnConnectCallback), state);
		}
		void SubmitScoreOnConnectCallback(IAsyncResult ar) {
			SubmitScoreState state = (SubmitScoreState) ar.AsyncState;

			SubmissionResponse position = new SubmissionResponse();
			ServerException error = null;

			// handle being unable to connect
			if (!client.Connected) {
				error = new ServerException("Unable to connect");
			} else {
				try {
					// send submission to the server
					string request = SubmissionRequest.ToJson(state.level, state.score);
					WriteLine(request);
					
					// get a response
					string response = ReadLine();

					// convert back to an object
					position = SubmissionResponse.FromJson(response);
				} catch (ServerException e) {
					error = e;
				}
			}

			// report back to user
			if (state.callback != null) {
				state.callback.Invoke(position, error);
			}
			
			// note: even if connection failed, clean up still happens
			DisconnectAsync(ar);
		}
		
		// callback and argument container for SubmitScoreAsync
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

		public ScoresResponse RequestScores(string level) {
			Connect();

			// form a request message
			string request = ScoresRequest.ToJson(level);

			// send it to the server
			WriteLine(request);

			// get a response
			string response = ReadLine();

			Disconnect();

			// convert back to an object and report to the user
			return ScoresResponse.FromJson(response, level);
		}
		
		public void RequestScoresAsync(string level, Action<ScoresResponse, ServerException> callback) {
			RequestScoresState state = new RequestScoresState(level, callback);
			ConnectAsync(new AsyncCallback(RequestScoresOnConnectCallback), state);
		}

		void RequestScoresOnConnectCallback(IAsyncResult ar) {
			RequestScoresState state = (RequestScoresState) ar.AsyncState;

			ServerException error = null;
			ScoresResponse scores = new ScoresResponse(state.level);

			// handle being unable to connect
			if (!client.Connected) {
				error = new ServerException("Unable to connect");
			} else {
				try {
					// send request to the server
					string request = ScoresRequest.ToJson(state.level);
					WriteLine(request);

					// get a response from server
					string response = ReadLine();

					// convert back to an object
					scores = ScoresResponse.FromJson(response, state.level);
				} catch (ServerException e) {
					error = e;
				}
			}

			// report back to user
			if (state.callback != null) {
				state.callback.Invoke(scores, error);
			}
			
			// note: even if connection failed, clean up still happens
			DisconnectAsync(ar);
		}
		
		// callback and argument container for RequestScoresAsync
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
