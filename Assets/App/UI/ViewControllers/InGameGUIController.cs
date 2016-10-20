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
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace.ui {
	public class InGameGUIController : MonoBehaviour {

		// ui elements
		public GameObject OnScreenControls;
		public GameObject PauseMenu;
		public GameObject DisconnectMenu;
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
		public void ExitButtonHandler(bool disconnected) {

			FinalizeLevel.ExitGame (disconnected);
		}

		// public method to handle a disconnection notification
		// from the multiplayer controller
		public void DisconnectionHandler(){

			// we have to shut down all other menus and bring up the disconnect dialog

			PauseMenu.SetActive (false);
			OnScreenControls.SetActive (false);
			ChatButton.SetActive (false);
			PauseButton.SetActive (false);
			TextClock.SetActive (false);

			DisconnectMenu.SetActive (true);

			// there is no way back from here, so it's
			// all good, no need to undo this change
		}
	}
}