using UnityEngine;
using System.Collections;
using GooglePlayGames;
using System;

/**
 * This class is responsible for connection to a room and initialising a new multiplayer game.
 * 
 * Mariam Shahid <mariams@student.unimelb.edu.au>
 */

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MatchMaking : MonoBehaviour, IRoomListener {
		// Holds the image that will be the background of the dialogue box
		public GUISkin guiSkin;

		// Whether to display the room dialogue box
		private bool showRoomDialogue;

		// The message the room dialogue box should contain
		private string roomMessage;

		// use this for initialization
		void Start()
		{
			showRoomDialogue = false;
		}

		// prints the status of how establishing connection with the room is going to the player's screen
		void OnGUI () {
			// start a new multiplayer game
			if (!showRoomDialogue)
			{
				roomMessage = "Starting a multi-player game...";
				showRoomDialogue = true;
				MultiPlayerController.Instance.roomListener = this;
				MultiPlayerController.Instance.StartMPGame();
			}
			
			// Print the room's status to the player's screen to let them know how well establishing connection is going
			if (showRoomDialogue)
			{
				GUI.skin = guiSkin;
				GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), roomMessage);
			}
		}

		// Assigns the room a message i.e how well establishing connection is going
		public void SetRoomStatusMessage(string message)
		{
			roomMessage = message;
		}

		// Hides the room from the player 
		public void HideRoom()
		{
			roomMessage = "";
			showRoomDialogue = false;
		}
	}
}
