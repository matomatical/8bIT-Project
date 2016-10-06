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

		public int SubmitScore(string level, Score score) {
			Connect();

			// form a submission message
			string request = SubmissionRequest.ToJson(level, score);
			//UILogger.Log("lb submission request:", request);

			// send it to the server
			WriteLine(request);
			
			// get a response
			string response = ReadLine();
			//UILogger.Log("lb submission response:", response);

			Disconnect();

			// convert back to an object and report to the user
			return SubmissionResponse.FromJson(response).position;
		}

		public Score[] RequestScores(string level) {
			Connect();

			// form a request message
			string request = ScoresRequest.ToJson(level);
			//UILogger.Log("lb request request:", request);

			// send it to the server
			WriteLine(request);

			// get a response
			string response = ReadLine();
			//UILogger.Log("lb request response:", response);

			Disconnect();

			// convert back to an object and report to the user
			return ScoresResponse.FromJson(response).leaders;
		}
		
		struct RequestScoresState {
			public string level;
			public Action<Score[], ServerException> callback;
			public RequestScoresState(string level, Action<Score[], ServerException> callback) {
				this.level = level;
				this.callback = callback;
			}
		}
		
		public void RequestScoresAsync(string level, Action<Score[], ServerException> callback) {
			RequestScoresState state = new RequestScoresState(level, callback);
			ConnectAsync(new AsyncCallback(RequestScoresOnConnectCallback), state);
		}

		void RequestScoresOnConnectCallback(IAsyncResult ar) {
			Score[] scores = null;
			ServerException error = null;
			RequestScoresState state = (RequestScoresState) ar.AsyncState;

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
					scores = ScoresResponse.FromJson(response).leaders;
				} catch (ServerException e) {
					error = e;
				}
			}

			// report back to user
			state.callback.Invoke(scores, error);
			
			// note: even if connection failed, clean up still happens
			DisconnectAsync(ar);
		}

	}

}
