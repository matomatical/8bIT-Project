/*
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using Tiled2Unity;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MultiplayerInit : MonoBehaviour {

		#if UNITY_EDITOR
		static bool editor = true;
		#else
		static bool editor = false;
		#endif

		public static void Init(TiledMap level){
			if(editor){
				InEditorInit (level);
			} else {
				OnAndroidInit (level);
			}
		}

		static void InEditorInit (TiledMap level){

			// Get player1 and player2 game objects
			LocalPlayerController[] players = level.GetComponentsInChildren<LocalPlayerController>();
			GameObject player1 = players[0].gameObject;
			GameObject player2 = players[1].gameObject;

			UpdateManager updateManager = new UpdateManager();

			// Make sure one player is remote
			player2.GetComponent<RemotePhysicsController> ().enabled = true;

			// Tell update manager about the serialiser for player 2 so updates get recieved
			player2.GetComponent<PlayerSerializer> ().enabled = true;
			updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());

			// Make sure one player is local
			player1.GetComponent<LocalPlayerController> ().enabled = true;

			// Tell player 1 to send updates to the update manager
			player1.GetComponent<PlayerSerializer> ().enabled = true;
			player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;


			// camera should have a reference to the player to-be-followed

			CameraController camera = FindObjectOfType<CameraController> ();
			camera.target = player1.GetComponent<ArcadePhysicsController>();



			// push blocks should also respond to collisions rather than
			// remote updates

			foreach (PushBlockController pbc in
				level.GetComponentsInChildren<PushBlockController>()){

				pbc.enabled = true;
			}


			// keys, key blocks and pressure plates should respond to
			// collisions, not remote updates!

			foreach (KeyController key in
				level.GetComponentsInChildren<KeyController>()) {

				key.enabled = true;
			}

			foreach (PressurePlateController plate in
				level.GetComponentsInChildren<PressurePlateController>()) {

				plate.enabled = true;
			}

			foreach (KeyBlockController block in
				level.GetComponentsInChildren<KeyBlockController>()) {

				block.enabled = true;
			}


		}

		// Use this for initialization
		static void OnAndroidInit (TiledMap level) {

			// variables for deciding which player is which
			string myID = MultiPlayerController.Instance.GetMyParticipantId ();
			List<Participant> participants = MultiPlayerController.Instance.GetAllPlayers ();
			Participant partner;

			// Get player1 and player2 game objects
			LocalPlayerController[] players = level.GetComponentsInChildren<LocalPlayerController>();
			GameObject player1 = players[0].gameObject;
			GameObject player2 = players[1].gameObject;

			CameraController camera = FindObjectOfType<CameraController> ();


			// Get the Chat Controller
			ChatController chat = FindObjectOfType<ChatController>();

			// Get the update manager ready
			UpdateManager updateManager = new UpdateManager();
			MultiPlayerController.Instance.updateManager = updateManager;

			// Tell updateManager and chatController about each other
			updateManager.chatController = chat;
			chat.updateManager = updateManager;
            // Tell player 1 to send updates to the update manager
            player1.GetComponent<PlayerSerializer>().enabled = true;
            player2.GetComponent<PlayerSerializer>().enabled = true;


            // Decide if the local user is player 1 or player 2
            // Activate appropriate components
            if (participants [0].ParticipantId.Equals (myID)) {
                UILogger.Log("I am player 1");
                // Partner is player 2
                partner = participants [1];

				// Disable the controller for the partner
                player2.GetComponent<RemotePhysicsController>().enabled = true;

				// And make sure the controller is enabled for the player
				player1.GetComponent<LocalPlayerController> ().enabled = true;

                


                // follow the first player
                camera.target = player1.GetComponent<ArcadePhysicsController>();

				// Tell update manager about the serialiser for player 2 so updates get recieved
				updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());

				// Tell player 1 to send updates to the update manager
				player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;



			} else {
                UILogger.Log("I am player 2");
                // Partner is player 1
                partner = participants [0];

				// Disable the controller for the partner

                player1.GetComponent<RemotePhysicsController>().enabled = true;

                // And make sure the controller is enabled for the player
                player2.GetComponent<LocalPlayerController> ().enabled = true;
                
				// follow this player
				camera.target = player2.GetComponent<ArcadePhysicsController>();

                // Tell update manager about the serialiser for player 1 so updates get recieved
                updateManager.Subscribe(player1.GetComponent<PlayerSerializer> ());

				// Tell player 2 to send updates to the update manager
				player2.GetComponent<PlayerSerializer> ().updateManager = updateManager;
                
            }


			// push blocks should also respond to collisions rather than
			// remote updates

			foreach (PushBlockController pbc in
				level.GetComponentsInChildren<PushBlockController>()){

				pbc.enabled = true;
			}


			// keys, key blocks and pressure plates should respond to
			// collisions, not remote updates!

			foreach (KeyController key in
				level.GetComponentsInChildren<KeyController>()) {

				key.enabled = true;
			}

			foreach (PressurePlateController plate in
				level.GetComponentsInChildren<PressurePlateController>()) {

				plate.enabled = true;
			}

			foreach (KeyBlockController block in
				level.GetComponentsInChildren<KeyBlockController>()) {

				block.enabled = true;
			}
		}
	}
}
