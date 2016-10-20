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

		public static UIState previousState;
		public static GameType type;
		public static string level;
		public static Recording recording;

		void OnEnable() {
			HideRoomStatusMessage();
			levelNameText.text = level;
			MultiPlayerController.Instance.StartMatchMaking (level, this);
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			Debug.Log ("Pressed the back button!");
			MultiPlayerController.Instance.StopMatchMaking ();
			UIStateMachine.instance.GoTo(previousState);
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
			if (type == GameType.MULTI) {
				SceneManager.StartMultiplayerGame (level);
			} else if (type == GameType.GHOST) {
				SceneManager.StartPlayagainstGame (level, recording);
			}
		}

	}
}