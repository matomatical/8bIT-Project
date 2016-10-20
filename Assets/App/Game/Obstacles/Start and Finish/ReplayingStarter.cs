/*
 * Start line component to begin a replaying
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * 
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.recording {
	public class ReplayingStarter : MonoBehaviour {

		static bool started = false;

		public ReplayingController replayer;

		void Start(){

			// reset static state between games
			started = false;

			// link components together
			if (replayer == null) {
				replayer = FindObjectOfType<ReplayingController> ();
			}
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {

					if(started == false && replayer != null){

						replayer.StartReplaying ();

						started = true;
					}
				}
			}
		}
	}
}
