/*
 * Start line logic.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	public class StartLine : MonoBehaviour {

		/// The Clock to start when we cross the line
		ClockController clock;

		/// Collider for detecting collisions
		BoxCollider2D box;

		void Start(){

			// link components together

			box = GetComponent<BoxCollider2D> ();

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {

			if (enabled) { // only trigger if this component is on

				// start the clock the first time a player comes through!
				if (other.gameObject.CompareTag ("Player")) {

					// making sure we're talking about the same level
					if (ArcadePhysics.SameLayer (other, box)) {

						clock.StartTiming ();

					}


				}
			}
		}

	}
}
