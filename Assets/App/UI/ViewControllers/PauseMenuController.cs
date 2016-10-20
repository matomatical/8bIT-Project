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
	public class PauseMenuController : MonoBehaviour {

		// ui elements
		public GameObject OnScreenControls;
		public GameObject PauseMenu;

		// public method to handle resume button behaviour
		public void ResumeButtonHandler() {
			PauseMenu.SetActive(false);
			OnScreenControls.SetActive(true);
		}

		// public method to handle exit button behaviour
		public void ExitButtonHandler() {
			MultiPlayerController.Instance.LeaveGame ();
			SceneManager.LoadMainMenu ();
		}

	}
}