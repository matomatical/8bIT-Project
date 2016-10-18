using UnityEngine;
using System.Collections;
using System.Threading;
using xyz._8bITProject.cooperace.leaderboard;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.multiplayer {
	public static class FinalizeLevel {

		public static byte position;

		public static UpdateManager updateManager;

		public static bool sentRequest = false;

		public static void FinalizeGame (float time) {
			if (!sentRequest) {
				sentRequest = true;
				// The Leaderboards to submit our time to
				Leaderboards leaderboards = new Leaderboards();

				// The response from the leaderboards after submitting
				SubmissionResponse response;

				// Our and our partners three letter names
				string ourName = "mar";
				string theirName = "sam";
				if (MultiPlayerController.Instance.IsHost()) {
					// Submit the time the the leaderboards
					leaderboards.SubmitScoreAsync("levelname", new Score(ClockController.SecsToHSecs(time), ourName, theirName),
						delegate (SubmissionResponse r, ServerException e) {
							response = r;
							if (e != null) UILogger.Log(e.Message);
							position = (byte)response.position;

							if (updateManager != null) {
								updateManager.SendLeaderboardsUpdate(position);
							}
						});
				}
			}
		}
	}
}
