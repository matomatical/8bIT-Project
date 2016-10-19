/*
 * Post game menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class PostGameMenuController : MonoBehaviour {

		public Text message;
		
		public void Update() {
			int position = FinalizeLevel.position;
			if (FinalizeLevel.requestComplete) {
				if (position == 0) {
					message.text = "Sorry you didn't make the leaderboard.";
				} else {
					message.text = "You placed " + position + " on the leaderboard.";
				}
			} else {
				message.text = "Contacting the leaderboard server...";
			}
		}

		// public method to handle play again button behaviour
		public void PlayAgainButtonHandler() {
		}

		// public method to handle back to main menu button behaviour
		public void MainMenuButtonHandler() {
			SceneManager.Load ("Main GUI");
		}

		// public method to handle save recording button behaviour
		public void SaveRecordingButtonHandler() {
			if (SceneManager.newRecording != null) {
				PersistentStorage.Write(
					"Recordings/" + SceneManager.levelToLoad + ".crr",
					Recording.ToString(SceneManager.newRecording));
			}
		}

	}	
}
