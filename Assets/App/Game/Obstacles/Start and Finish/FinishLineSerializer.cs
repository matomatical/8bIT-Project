/*
 * Finish line synchronisation component
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Sam Beyer 	<sbeyer@student.unimelb.edu.au>
 * Mariam Shahid <mariams@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace {
	public class FinishLineSerializer : MonoBehaviour {

		/// The Clock to get the time from
		ClockController clock;

		void Start(){

			// link components together

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {

					// and now it's time to stop the level
					FinalizeLevel.CrossFinishLine (clock.GetTime ());
				}
			}
		}
	}
}
