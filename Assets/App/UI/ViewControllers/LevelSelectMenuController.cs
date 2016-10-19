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
	public class LevelSelectMenuController : MonoBehaviour, IRoomListener {

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
			PlayGamesPlatform.Activate();
			UpdateLevelDetails();
			HideMessage();
		}

		// display the level details
		void UpdateLevelDetails() {
			string levelName = Maps.maps[currentLevelIndex];
			levelNameText.text = levelName;
			LevelPreview.LoadPreview(levelName, preview);
		}

		// methods to manipulate the message
		void DisplayMessage(string msg) {
			messageText.GetComponent<Text>().text = msg;
			messageText.SetActive(true);
		}
		void HideMessage() {
			messageText.SetActive(false);
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
			#if UNITY_EDITOR
			SceneManager.levelToLoad = Maps.maps[currentLevelIndex_];
			SceneManager.Load("Game Scene");
			#else
			SceneManager.gameType = GameType.MULTI;
			SceneManager.levelToLoad = Maps.maps[currentLevelIndex_];
			SceneManager.playingAgainstGhosts = false;
			DisplayMessage("Starting Game...");
			MultiPlayerController.Instance.roomListener = this;
			MultiPlayerController.Instance.StartMPGame((uint)currentLevelIndex_);
			#endif
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			#if UNITY_EDITOR
			UIStateMachine.instance.GoTo(UIState.MainMenu);
			#else
			UIHelper.LeaveRoom();
			UIStateMachine.instance.GoTo(UIState.MainMenu);
			#endif
		}

		// IListener methods
		public void SetRoomStatusMessage(string message) {
			DisplayMessage(message);
		}
		public void HideRoom() {
			// HideMessage();
		}

	}
}
