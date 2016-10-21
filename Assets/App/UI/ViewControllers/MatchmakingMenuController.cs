/*
 * Matchmaking menu screen logic
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using xyz._8bITProject.cooperace.multiplayer;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace.ui {
	public class MatchmakingMenuController : MonoBehaviour, IRoomListener {

		// ui elements
		public Text levelNameText;
		public GameObject messageText;

		// static state for setting up the menu matchmaking
		public struct Options {
			public readonly UIState previousState;
			public readonly GameType type;
			public readonly string level;
			public readonly Recording recording;
			public Options(UIState previousState, GameType type, string level, Recording recording = null){
				this.previousState = previousState;
				this.type = type;
				this.level = level;
				this.recording = recording;
			}
		}

		public static Options options;

		void OnEnable() {
			HideRoomStatusMessage();
			levelNameText.text = options.level;
			MultiPlayerController.Instance.StartMatchMaking (options.level, this);
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			Debug.Log ("Pressed the back button!");
			MultiPlayerController.Instance.StopMatchMaking ();
			UIStateMachine.instance.GoTo(options.previousState);
		}

		// IListener methods
		public void SetRoomStatusMessage(string message) {
			messageText.GetComponent<Text>().text = message;
			messageText.SetActive(true);
		}
		public void HideRoomStatusMessage() {
			messageText.SetActive(false);
		}

		public void OnConnectionComplete(){
			if (options.type == GameType.MULTI) {
				SceneManager.StartMultiplayerGame (options.level);
			} else if (options.type == GameType.GHOST) {
				SceneManager.StartPlayagainstGame (options.level, options.recording);
			}
		}

	}
}