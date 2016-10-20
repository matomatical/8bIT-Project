/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Sam Beyer 	<sbeyer@student.unimelb.edu.au>
 * Mariam Shahid <mariams@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		/// The Clock to stop when we cross the line
		ClockController clock;

		void Start(){

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {
						
					// the timer is stopped when a player touches the line
					// idempotent, so don't worry about calling it more than once
					clock.StopTiming ();

				}
			}
		}
	}
}
