/*
 * Main menu logic, also responsible for logging into google play games.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class MainMenuController : MonoBehaviour {

		public Text gamerTag;

		public Text	tapText; // tap to start text
		string originalTapText;
		
		void Awake() {
			// Make a back up so we can restore later;
			originalTapText = tapText.text;
		}
		
		void OnEnable() {
			// Set gamer tag text to the player's actual gamer tag
			string tag = GamerTagManager.GetGamerTag();
			if (tag != null) {
				gamerTag.text = tag;
			}

			// If the player isn't logged in, try to log in
			RestoreTapText();
			LoginToGooglePlay(false);
		}
		
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
			#if UNITY_EDITOR
			UIStateMachine.instance.GoTo(UIState.LevelSelect);
			return;

			#else
			// Make sure to login
			LoginToGooglePlay(true);
			#endif
		}
		
		void LoginToGooglePlay(bool processToLevelSelect) {
			if (!Authentication.IsLoggedIn()) {
				Authentication.Login((bool success) => {
					if (success) {
						if (processToLevelSelect) {
							UIStateMachine.instance.GoTo(UIState.LevelSelect);
						}
					} else {
						SetTapText("Unable to login to\nGoogle Play Games.\n\nTap to Try Again");
					}
				});
			} else {
				if (processToLevelSelect) {
					UIStateMachine.instance.GoTo(UIState.LevelSelect);
				}
			}
		}
		
		// Manipulate the "tap to start" text, used for error messages
		void SetTapText(string msg) {
			tapText.text = msg;
		}
		void RestoreTapText() {
			tapText.text = originalTapText;
		}

	}
}