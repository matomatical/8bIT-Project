/*
 * Provides an API for accessing the leaderboards server.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;

namespace xyz._8bITProject.cooperace.leaderboard {
	public class Leaderboards {

		#if UNITY_EDITOR
		private string host = "localhost";
		#else
		private string host = "lb.cooperace.8bITProject.xyz";
		#endif
		private short port = 2693;

		private TcpClient client;
		private NetworkStream networkStream;
		private StreamReader reader;
		private StreamWriter writer;

		public Leaderboards(string host=null, short? port=null) {
			if (host != null) {
				this.host = host;
			}	
			if (port.HasValue) {
				this.port = port.Value;
			}
			client = new TcpClient();
		}

		public void Connect() {
			// open the connection
			client.Connect(host, port);

			networkStream = client.GetStream();
			reader = new StreamReader(networkStream);
			writer = new StreamWriter(networkStream);
		}

		public void Disconnect(){
			if (client.Connected) {
				// close the connection
				reader.Close();
				writer.Close();
				networkStream.Close();
				client.Close();

				client = new TcpClient(); // to allow reconnecting
			}
		}
		public int SubmitScore(string level, Score score) {
			if (!client.Connected) {
				// raise exception!	
			}

			// form a submission message
			string request = SubmissionRequest.ToJson(level, score);
			UILogger.Log("lb submission request:", request);

			// send it to the server
			writer.WriteLine(request);
			writer.Flush();
			
			// get a response
			string response = reader.ReadLine();
			UILogger.Log("lb submission response:", response);

			// convert back to an object and report to the user
			return -1;
			return SubmissionResponse.FromJson(response).position;
		}

		public Score[] RequestScores(string level) {
			if (!client.Connected) {
				// raise exception!	
			}

			// form a request message
			string request = ScoresRequest.ToJson(level);
			UILogger.Log("lb request request:", request);

			// send it to the server
			writer.WriteLine(request);
			writer.Flush();

			// get a response
			string response = reader.ReadLine();
			UILogger.Log("lb request response:", response);

			// convert back to an object and report to the user
			return ScoresResponse.FromJson(response).leaders;
		}

	}
}
