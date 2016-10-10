using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class InEditorInit : MonoBehaviour {

		// Use this for initialization
		void Start () {

			// Get player1 and player2 game objects
			LocalPlayerController[] players = FindObjectsOfType<LocalPlayerController>();
            GameObject player1 = players[0].gameObject;
            GameObject player2 = players[1].gameObject;

            UpdateManager updateManager = new UpdateManager();

			// Make sure one player is remote
			if (player2) {
				player2.GetComponent<RemotePhysicsController> ().enabled = true;

				// Tell update manager about the serialiser for player 2 so updates get recieved
				updateManager.Subscribe(player2.GetComponent<PlayerSerializer> ());
			}

			// Make sure one player is local
			if (player1) {
				player1.GetComponent<LocalPlayerController> ().enabled = true;

				// Tell player 1 to send updates to the update manager
				player1.GetComponent<PlayerSerializer> ().updateManager = updateManager;
			}
		}
	}
}
