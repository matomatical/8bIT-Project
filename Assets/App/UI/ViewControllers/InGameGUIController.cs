/*
 * Pause menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class InGameGUIController : MonoBehaviour {

		// ui elements
		public GameObject OnScreenControls;
		public GameObject PauseMenu;
		public GameObject PauseButton;
		public GameObject ChatButton;
		public GameObject TextClock;
		bool menu = true;


		void Awake(){
			menu = PauseMenu.activeSelf;
		}

		// public method to show/hide the pause menu when we click the pause button
		public void PauseButtonHandler() {
			menu = !menu;
			PauseMenu.SetActive(menu);
			OnScreenControls.SetActive(!menu);
		}

		// public method to handle resume button behaviour
		public void ResumeButtonHandler() {
			PauseMenu.SetActive(false);
			OnScreenControls.SetActive(true);
			menu = false;
		}

		// public method to handle exit button behaviour
		public void ExitButtonHandler() {

			MultiPlayerController.Instance.LeaveGame ();
			SceneManager.LoadMainMenu ();
		}

	}
}