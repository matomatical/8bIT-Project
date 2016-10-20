/*
 * Logic for the screen that comes up after you finish
 * watching a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace.ui {
	public class PostReplayingMenuController : MonoBehaviour {
		
		// public method to handle play again button behaviour
		public void ReplayAgainButtonHandler() {
			
		}

		// public method to handle back to main menu button behaviour
		public void MainMenuButtonHandler() {
			SceneManager.LoadMainMenu ();
		}
	}
}
