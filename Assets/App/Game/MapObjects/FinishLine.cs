/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace _8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		public ClockController clock;

		void OnTriggerEnter2D (Collider2D other) {
			if (clock != null && other.gameObject.CompareTag("Player")) {
				clock.StopTiming();
			}
		}

	}
}
