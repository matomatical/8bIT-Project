/*
 * Finish line component to end a replaying
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * 
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.recording {
	public class ReplayingEnder : MonoBehaviour {

		static bool ended = false;

		public bool terminal = false;

		public ReplayingController replayer;

		void Start(){

			// link components together
			if (replayer == null) {
				replayer = FindObjectOfType<ReplayingController> ();
			}
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {

					if(ended == false && replayer != null){

						replayer.EndReplaying();

						ended = true;

						// if this component is meant to end
						// the level, end it

						if (terminal) {
							SceneManager.ExitGame (ExitType.FINISH);
						}
					}
				}
			}
		}
	}
}
