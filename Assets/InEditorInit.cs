using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class InEditorInit : MonoBehaviour {

		// Use this for initialization
		void Start () {

			// Get player1 and player2 game objects
			GameObject player1 = GameObject.Find("Player1");
			GameObject player2 = GameObject.Find("Player2");

			UpdateManager updateManager = new UpdateManager();

			// Disable the controller for the partner
			player2.GetComponent<PlayerController> ().enabled = false;
			// And make sure the controller is enabled for the player
			player1.GetComponent<PlayerController> ().enabled = true;

			// Tell update manager about the serialiser for player 2 so updates get recieved
			updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());

			// Tell player 1 to send updates to the update manager
			player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;
		}
	}
}
