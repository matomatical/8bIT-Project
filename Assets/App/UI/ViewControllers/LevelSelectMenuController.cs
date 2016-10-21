/*
 * Level select menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class LevelSelectMenuController : MonoBehaviour {

		// ui elements
		public Text levelNameText;
		public GameObject messageText;
		public RawImage preview;

		// the currently displayed level
		public static int currentLevelIndex_ = 0;
		public int currentLevelIndex {
			get {
				return currentLevelIndex_;
			}
			set {
				// wraps around the list of maps
				if (value >= Maps.maps.Length) {
					value = 0;
				}
				if (value < 0) {
					value = Maps.maps.Length - 1;
				}
				currentLevelIndex_ = value;

				UpdateLevelDetails();
			}
		}

		void OnEnable() {
			// make sure something is loaded when visible
			UpdateLevelDetails();
		}

		// display the level details
		void UpdateLevelDetails() {
			string levelName = Maps.maps[currentLevelIndex];
			levelNameText.text = levelName;
			LevelPreview.LoadPreview(levelName, preview);
		}

		// public methods to switch the currently displayed level
		public void SwitchToNextLevel() {
			currentLevelIndex += 1;
		}
		public void SwitchToPrevLevel() {
			currentLevelIndex -= 1;
		}

		// public method to handle play button behaviour
		public void PlayButtonHandler() {

			// set up matchmaking for multiplayer game

			MatchmakingMenuController.options =
				new MatchmakingMenuController.Options (
					UIState.LevelSelect,
					GameType.MULTI,
					Maps.maps [currentLevelIndex]
				);

			// start matchmaking!

			UIStateMachine.instance.GoTo(UIState.Matchmaking);
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}
	}
}
