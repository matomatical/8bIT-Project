/*
 * Post game menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace.ui {
	public class PostGameMenuController : LeaderboardsMenuController {

		public void Start() {
			currentLevelName = SceneManager.levelToLoad;
			LoadLevelStats();
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
