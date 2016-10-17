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

		ClockController clock;

		void Start () {
			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (enabled) { // only trigger if this component is on
				// the timer is stopped when the player touches the finish line
				if (other.gameObject.CompareTag ("Player")) {

					clock.StopTiming ();
				}
			}
		}

	}
}
