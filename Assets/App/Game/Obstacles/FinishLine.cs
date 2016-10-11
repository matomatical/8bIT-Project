/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		/// The Clock to stop when we cross the line
		ClockController clock;

		/// Collider for detecting collisions
		BoxCollider2D box;

		void Start(){

			// link components together

			box = GetComponent<BoxCollider2D> ();

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			// only trigger if this component is on
			if (enabled) {
				
				// the timer is stopped when the player touches the finish line
				if (other.gameObject.CompareTag ("Player")) {

					// making sure we're talking about the same level
					if (ArcadePhysics.SameLayer (other, box)) {

						clock.StopTiming ();
					}
				}
			}
		}

	}
}
