﻿/*
 * Struct to hold a single score's data
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {

	[System.Serializable]
	public struct Score {

		public int time;
		public string player1, player2;

		public Score(int time, string player1, string player2) {
			this.time = time;
			this.player1 = player1;
			this.player2 = player2;
			
			Validate();
		}
		
		public void Validate() {
			if (time <= 0) {
				throw new InvalidScoreException("Time " + time + " is less than or equal to zero.");
			}

			if (player1 == null) {
				throw new InvalidScoreException("Player 1 name is null.");
			}
			if (player2 == null) {
				throw new InvalidScoreException("Player 2 name is null.");
			}

			if (player1.Length != 3) {
				throw new InvalidScoreException("Player 1 name " + player1 + " is not 3 letters.");
			}
			if (player2.Length != 3) {
				throw new InvalidScoreException("Player 2 name " + player2 + " is not 3 letters.");
			}
		}

		public static string TimeToString(int time) {
			string fractions = (time % 10).ToString();
			string seconds = ((time / 10) % 60).ToString ().PadLeft (2, '0');
			string minutes = ((time / 10) / 60).ToString();

			return minutes + ":" + seconds + "." + fractions;
		}
		
		override public string ToString() {
			return string.Format("Score({0}, {1}, {2})",
				player1, player2, TimeToString(time));	
		}
		
		public class InvalidScoreException : System.Exception {
			public InvalidScoreException(string message) : base(message) {}
		}

	}

}
