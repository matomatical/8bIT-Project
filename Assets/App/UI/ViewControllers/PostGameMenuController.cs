/*
 * Post game menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.ui {
	public class PostGameMenuController : LeaderboardsMenuController {

		public void Start() {
			currentLevelName = SceneManager.levelToLoad;
			currentLevelName = Maps.maps[0];
			LoadLevelStats();
		}

		// public method to handle play again button behaviour
		public void PlayAgainButtonHandler() {
		}

		// public method to handle back to main menu button behaviour
		public void MainMenuButtonHandler() {
		}

		// public method to handle save recording button behaviour
		public void SaveRecordingButtonHandler() {
			if (SceneManager.newRecording != null) {
				PersistentStorage.Write(
					"Recording/" + SceneManager.levelToLoad + ".crr",
					SceneManager.newRecording);
			}
		}

	}	
}
