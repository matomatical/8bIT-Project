/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		public ClockController clock;

		void OnTriggerEnter2D (Collider2D other) {
			// the timer is stopped when the player touches the finish line
			if (other.gameObject.CompareTag("Player")) {
				clock.StopTiming();
			}
		}

	}
}
