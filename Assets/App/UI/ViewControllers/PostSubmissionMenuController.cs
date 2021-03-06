/*
 * Logic for the screen that comes up after you finish
 * playing a game
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class PostSubmissionMenuController : MonoBehaviour {
		
		public Text leaderboardsMessage, recordingMessage;

		bool disconnected = false;

		public void OnDisconnect(){
			disconnected = true;
		}

		void OnEnable(){
			// This menu screen has just been opened, we should check
			// for which message to show the user!

			if (FinalizeLevel.positionSet) {
				DisplayPosition (FinalizeLevel.position);
			} else {
				if (SceneManager.outs.exit == ExitType.DISCONNECT) {
					leaderboardsMessage.text = "Could not check position. Check the leaderboards.";
					disconnected = true;
				} else {
					leaderboardsMessage.text = "Waiting for position...";
					disconnected = false;
				}
			}
		}

		void DisplayPosition(int position){
			if (position == 0) {
				leaderboardsMessage.text = "Sorry you didn't make the leaderboard.";
			} else {
				leaderboardsMessage.text = "You placed " + position + " on the leaderboard.";
			}
		}

		/// Every frame, check whether or not to display the position or a different message
		void Update() {
			
			if (FinalizeLevel.positionSet) {

				// we have a position to display
				DisplayPosition (FinalizeLevel.position);

			} else {

				// are we still connected?
				if (disconnected) {
					leaderboardsMessage.text = "Could not check position. Check the leaderboards.";
				} else {
					leaderboardsMessage.text = "Waiting for position...";
				}
			}
		}

		// public method to handle play again button behaviour
		public void PlayAgainButtonHandler() {
			
		}

		// public method to handle back to main menu button behaviour
		public void MainMenuButtonHandler() {
			SceneManager.LoadMainMenu ();
		}

		// public method to handle save recording button behaviour
		public void SaveRecordingButtonHandler() {
			
			if (SceneManager.outs.recording != null) {
				
				// and save the recording

				RecordingFileManager.WriteRecording (SceneManager.outs.recording);

				// we should set this message to notify the user of success

				recordingMessage.text = "Recording saved";

			} else {

				// if there's no recording, something must have gone wrong

				recordingMessage.text = "Failed to save recording";

			}
		}

	}	
}
