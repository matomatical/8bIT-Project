/*
 * Main menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.ui {
	public class MainMenuController : MonoBehaviour {

		// All methods simply changes state.
		
		public void LeaderboardsButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.LeaderboardsMenu);
		}
		
		public void GamerTagButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.GamerTagMenu);
		}
		
		public void RecordingsButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.RecordingsMenu);
		}
		
		public void StartButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.LevelSelect);
		}

	}
}