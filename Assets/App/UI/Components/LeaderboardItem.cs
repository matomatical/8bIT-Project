/*
 * Very simple class representing one leaderboard ui item.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.leaderboard;

namespace xyz._8bITProject.cooperace.ui {

	public class LeaderboardItem : MonoBehaviour {

		public Text player1Text;
		public Text player2Text;
		public Text scoreText;

		public string player1 {
			get {
				return player1Text.text;
			}
			set {
				player1Text.text = value;
			}
		}

		public string player2 {
			get {
				return player2Text.text;
			}
			set {
				player2Text.text = value;
			}
		}

		public string score {
			get {
				return scoreText.text;
			}
			set {
				scoreText.text = Score.TimeToString(int.Parse(value));
			}
		}

	}

}
