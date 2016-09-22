/*
 * Provides an API for accessing leaderboards server
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
//using xyz._8bITProject.cooperace.leaderboard.protocol; // ??

namespace xyz._8bITProject.cooperace.leaderboard {
	public class LeaderboardsServer {

		// TODO: Make Addresses Configurable

		private const string host = "lb.cooperace.8bITProject.xyz";
		private const short  port = 2693;

		public LeaderboardsServer() {
			
		}

		public void Connect(){

			// open the connection

		}

		public void Disconnect(){

			// close the connection

		}

		// TODO: who's responsible for picking the right level name?
		// MAYBE we take levels, not names? Maybe it's just TiledMap.name,
		// and we need to be more careful naming our levels?
		// How will names in the level selector be decided, anyway?

		public int SubmitScore(string level, Score score){

			// form a submission message

			Submission submission = new Submission(level, score);

			string message = JsonUtility.ToJson (submission, false);

			// send it to the server, get a response

			// ... send(message)

			string response = "{}"; // = recv() ... // TODO: the connection could end!!

			// convert back to an object and report to the user

			Position position = JsonUtility.FromJson<Position> (response);

			return position.position;
		}

		private struct Submission {
			public string level;
			public Score score;

			public Submission(string level, Score score){
				this.level = level;
				this.score = score; // TODO: validate, where?
			}
		}

		private struct Position {
			public int position;
		}

		public Score[] RequestScores(string level){
			
			// form a request message

			Request request = new Request(level);

			string message = JsonUtility.ToJson (request, false);

			// send it to the server, get a response

			// ... send(message)

			string response = "{}"; // = recv() ... // TODO: the connection could end!!

			// convert back to an object and report to the user

			Leaders leaders = JsonUtility.FromJson<Leaders> (response);

			return leaders.leaders;
		}

		private struct Request {
			public string level;

			public Request(string level){
				this.level = level;
			}
		}

		private struct Leaders {
			public Score[] leaders;
		}
	}
}
