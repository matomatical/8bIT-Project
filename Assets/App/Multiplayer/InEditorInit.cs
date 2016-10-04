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

			// Make sure one player is remote
			if (player2) {
				player2.GetComponent<LocalPlayerController> ().enabled = false;
				player2.GetComponent<RemotePlayerController> ().enabled = true;

				// Tell update manager about the serialiser for player 2 so updates get recieved
				updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());
			}

			// Make sure one player is local
			if (player1) {
				player1.GetComponent<RemotePlayerController> ().enabled = false;
				player1.GetComponent<LocalPlayerController> ().enabled = true;

				// Tell player 1 to send updates to the update manager
				player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;
			}
		}
	}
}
