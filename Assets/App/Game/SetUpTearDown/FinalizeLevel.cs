using UnityEngine;
using System.Collections;
using System.Threading;
using xyz._8bITProject.cooperace.leaderboard;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.ui;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace {
	public static class FinalizeLevel {

		public static byte position;
		public static float time;

		public static bool finishLineCrossed = false;
		public static bool positionSet = false;

		private static UpdateManager updateManager;

		/// Set up the static state of this class.
		/// Call before using it each game
		public static void Init(UpdateManager updateManager){
			FinalizeLevel.updateManager = updateManager;

			finishLineCrossed = false;
			positionSet = false;
		}

		/// exit the level through a menu beore actually
		/// finishing and walking off the edge of the level
		public static void ExitGame(bool disconnected){

			// the behaviour of exiting is different depending on
			// whether the race has ended or not. so, have we crossed
			// the finish line?

			if (!finishLineCrossed) {
				
				// transition to the main menu
				// (this will exit the multiplayer game)

				SceneManager.ExitGameQuit (disconnected);

			} else {

				// it's as if we finished the game, actually.
				// we need to get the recording and take it to
				// the postgame menu (instead of going to the
				// main menu)

				FinishGame (disconnected);

			}
		}

		public static void FinishGame(bool disconnected = false){

			// get the recording

			RecordingController recorder = GameObject.FindObjectOfType<RecordingController> ();
			Recording recording = null;
			if (recorder != null) {
				recording = recorder.GetRecording ();
			}

			// take it to the postgame menu

			SceneManager.ExitGameFinish (recording, disconnected);
		}



		public static void CrossFinishLine (float time) {

			// well, first of all, this method should only
			// be enacted ONCE per game:

			if (!finishLineCrossed) {
				finishLineCrossed = true;

				// when we cross the finish line for the first time,
				// the host and client have two different roles

				if (MultiPlayerController.Instance.IsHost ()) {

					// the host has to submit a score to the leaderboards,
					// and then update the client on receiving a response

					SubmitScore (time);

				} else {
					
					// it's the client's job to send a finish line/clock stop
					// update to the host

					updateManager.SendStopClock ();
					
				}

				// we also need to save the time to make tha available
				FinalizeLevel.time = time;
			}
		}

		/// submit a score to the leaderboards,
		/// updating the client on a response
		static void SubmitScore(float time){

			// The Leaderboards to submit our time to
			Leaderboards leaderboards = new Leaderboards();

			// The score to submit
			string ourName = GamerTagManager.GetGamerTag ();
			string theirName = MultiPlayerController.Instance.theirName;
			int tenthSeconds = ClockController.SecondsToTenthsOfSeconds (time);
			Score score = new Score (tenthSeconds, ourName, theirName);
			
			// Submit the time the the leaderboards, and send a message
			// to the client upon response
			leaderboards.SubmitScoreAsync(SceneManager.opts.level, score,
				delegate (SubmissionResponse r, ServerException e) {
					
					if (e != null) UILogger.Log(e.Message);
					position = (byte)r.position;
					positionSet = true;

					updateManager.SendLeaderboardsUpdate(position);
				});
		}
	}
}
