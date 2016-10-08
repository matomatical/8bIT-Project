/*
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MultiplayerInit : MonoBehaviour {

		// Use this for initialization
		void Start () {

			// variables for deciding which player is which
			string myID = MultiPlayerController.Instance.GetMyParticipantId ();
			List<Participant> participants = MultiPlayerController.Instance.GetAllPlayers ();
			Participant partner;

			// Get player1 and player2 game objects
			GameObject player1 = GameObject.Find ("Player1");
			GameObject player2 = GameObject.Find ("Player2");
			GameObject camera = GameObject.Find ("Camera");

			// Get the chat game object and chatConrtoller
			GameObject chat = GameObject.Find("ChatController");
			ChatController chatController = chat.GetComponent<ChatController> ();

			// Get the update manager ready
			UpdateManager updateManager = new UpdateManager();
			MultiPlayerController.Instance.updateManager = updateManager;

			// Tell updateManager and chatController about each other
			updateManager.chatController = chatController;
			chatController.updateManager = updateManager;

			// Decide if the local user is player 1 or player 2
			// Activate appropriate components
			if (participants [0].ParticipantId.Equals (myID)) {
				// Partner is player 2
				partner = participants [1];

				// Disable the controller for the partner
				player2.GetComponent<LocalPlayerController> ().enabled = false;
                player2.GetComponent<RemotePhysicsController>().enabled = true;

				// And make sure the controller is enabled for the player
				player1.GetComponent<LocalPlayerController> ().enabled = true;
                player1.GetComponent<RemotePhysicsController>().enabled = false;

				camera.GetComponent<CameraController>().target = player1.GetComponent<ArcadePhysicsController>();
				 
				// Tell update manager about the serialiser for player 2 so updates get recieved
				updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());

				// Tell player 1 to send updates to the update manager
				player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;

                player1.GetComponent<PlayerSpriteController>().SetPlayerController(player1.GetComponent<LocalPlayerController>());
                player2.GetComponent<PlayerSpriteController>().SetPlayerController(player2.GetComponent<RemotePhysicsController>());

			} else {
				// Partner is player 1
				partner = participants [0];

				// Disable the controller for the partner
				player1.GetComponent<LocalPlayerController> ().enabled = false;
                player1.GetComponent<RemotePhysicsController>().enabled = true;

                // And make sure the controller is enabled for the player
                player2.GetComponent<LocalPlayerController> ().enabled = true;
                player2.GetComponent<RemotePhysicsController>().enabled = false;

				camera.GetComponent<CameraController>().target = player2.GetComponent<ArcadePhysicsController>();
                // Tell update manager about the serialiser for player 1 so updates get recieved
                updateManager.Subscribe(player1.GetComponent<PlayerSerializer> ());

				// Tell player 2 to send updates to the update manager
				player2.GetComponent<PlayerSerializer> ().updateManager = updateManager;

                player1.GetComponent<PlayerSpriteController>().SetPlayerController(player1.GetComponent<RemotePhysicsController>());
                player2.GetComponent<PlayerSpriteController>().SetPlayerController(player2.GetComponent<LocalPlayerController>());
            }
		}
	}
}
