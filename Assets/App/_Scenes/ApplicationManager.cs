using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.multiplayer;


namespace xyz._8bITProject.cooperace {
	public class ApplicationManager : MonoBehaviour {
//
//
//		void Start(){
//			Application.runInBackground = true;
//		}


		void OnApplicationPause (bool paused) {
			if (paused) {
				UILogger.Log ("Pausing");

				// if we're in a networked game, we should leave
				// the room
				MultiPlayerController.Instance.OnPeersDisconnected (new string[] {MultiPlayerController.Instance.GetMyParticipantId()});

			} else {
				UILogger.Log ("Unpausing");
			}
		}
	}
}