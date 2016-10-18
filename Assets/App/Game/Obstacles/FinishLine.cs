/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		/// The Clock to stop when we cross the line
		ClockController clock;

		public UpdateManager updateManager;

		void Start(){

			// link components together

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {


				// only trigger if this component is on
				if (enabled) {
					
				

					// Send an update saying the clock has stopped
					if (updateManager != null)
						updateManager.SendStopClock ();

					// the timer is stopped when the player touches the finish line
					clock.StopTiming();

					// and now it's time to stop the level
					FinalizeLevel.FinalizeGame (clock.GetTime ());
				}
			}
		}
	}
}
