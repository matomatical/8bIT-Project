using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {
	public class LeaderboardsTest : MonoBehaviour {
		
		Leaderboards lb;

		void Start () {
			lb = new Leaderboards();
			lb.Connect();

			string level = "test level";

			lb.SubmitScore(level, new Score(10, "aaa", "bbb"));
			lb.SubmitScore(level, new Score(20, "ccc", "ddd"));
			lb.SubmitScore(level, new Score(30, "eee", "fff"));

			foreach (Score s in lb.RequestScores(level)) {
				UILogger.Logf("{0} + {1}: {2}", s.player1, s.player2, s.time);
			}
		}

	}
}
