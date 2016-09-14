using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

namespace _8bITProject.cooperace.multiplayer
{
	public class MultiplayerInit : MonoBehaviour {

		// Use this for initialization
		void Start () {

			// variables for deciding which player is which
			string myID = MultiplayerController.Instance.GetMyParticipantId ();
			List<Participant> participants = MultiplayerController.Instance.GetAllPlayers ();
			Participant partner;

			// Get player1 and player2 game objects
			GameObject player1 = GameObject.Find("Player1");
			GameObject player2 = GameObject.Find("Player2");

			// Get the update manager ready
			UpdateManager updateManager = new UpdateManager();
			MultiplayerController.Instance.updateManager = updateManager;

			// Decide if the local user is player 1 or player 2
			// Activate appropriate components
			if (participants [0].ParticipantId.Equals (myID)) {
				// Partner is player 2
				partner = participants [1];

				// Disable the controller for the partner
				player2.GetComponent<PlayerController> ().enabled = false;

				// Tell update manager about the serialiser for player 2 so updates get recieved
				updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());

				// Tell player 1 to send updates to the update manager
				player1.GetComponent<PlayerSerializer> ().Subscribe(updateManager);
			} else {
				// Partner is player 1
				partner = participants [0];

				// Disable the controller for the partner
				player1.GetComponent<PlayerController> ().enabled = false;

				// Tell update manager about the serialiser for player 1 so updates get recieved
				updateManager.Subscribe(player1.GetComponent<PlayerSerializer> ());

				// Tell player 2 to send updates to the update manager
				player2.GetComponent<PlayerSerializer> ().Subscribe(updateManager);
			}
		}

		void Update () {
			// Nothing to do here...
		}
	}
}
