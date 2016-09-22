/*
 * Struct to hold a single score's data
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {
	public struct Score {							// TODO: use a struct?

		public int time;
		public string player1, player2;

		public Score(int time, string player1, string player2) {

			// make sure it's all valid

			this.time = time;
			this.player1 = player1;
			this.player2 = player2;
		}

		public static string TimeToString(int time){
			int fractions = time % 10;
			int seconds = (time / 10) % 60;
			int minutes = (time / 10) / 60;

			return minutes + ":" + seconds + "." + fractions;
		}
	}
}
