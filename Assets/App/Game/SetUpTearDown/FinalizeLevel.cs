using UnityEngine;
using System.Collections;
using System.Threading;
using xyz._8bITProject.cooperace.leaderboard;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.multiplayer {
	public static class FinalizeLevel {

		public static byte position;

		public static UpdateManager updateManager;

		public static void FinalizeGame (float time) {

			// The Leaderboards to submit our time to
			Leaderboards leaderboards = new Leaderboards ();

			// The response from the leaderboards after submitting
			SubmissionResponse response;


			// Our and our partners three letter names
			string ourName = PersistentStorage.Read (NameInputHelper.filename);
			string theirName = "xyz";

            UILogger.Log("checking if I'm the host");
			if (MultiPlayerController.Instance.IsHost ()) {
                UILogger.Log("about to submit to the leaderboards server");
				// Submit the time the the leaderboards
				leaderboards.SubmitScoreAsync ("levelname", new Score (ClockController.SecsToHSecs (time), ourName, theirName),
					delegate (SubmissionResponse r, ServerException e) {
						response = r;
						UILogger.Log ("Congratulations! You're on the leaderboards in position " + r.position);
                        if(e!=null) UILogger.Log(e.Message);
                        position = (byte)response.position;

						if (updateManager != null) {
							updateManager.SendLeaderboardsUpdate (position);
						}
					});
			}
		}
	}
}
