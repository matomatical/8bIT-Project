/*
 * Start line syncronisation component.
 *
 * Sam Beyer 	 <sbeyer@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace {
	public class StartLineSerializer : MonoBehaviour {

		public UpdateManager updateManager;

		void Start(){
			// we need this so that unity lets us disable the component
		}

		void OnTriggerEnter2D (Collider2D other) {
			
			if (ArcadePhysics.SameWorld(this, other)) {

				if (enabled) { // only trigger if this component is on

					// start the clock the first time a player comes through!
					if (other.gameObject.CompareTag (Magic.Tags.PLAYER)) {

						// Send an update saying the clock has started

						if (updateManager != null)
							updateManager.SendStartClock ();
					}
				}
			}
		}
	}
}
