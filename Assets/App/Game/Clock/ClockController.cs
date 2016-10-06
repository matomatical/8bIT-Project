/*
 * Controller for the time text ui object.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {

	public class ClockController : MonoBehaviour {

		float time = 0;
		bool isTiming = true;

		void Update () {
			if (isTiming) {
				// count
				time += Time.deltaTime;
			}
		}

		public void StopTiming() {
			isTiming = false;
		}

		public void StartTiming() {
			isTiming = true;
		}

		public float GetTime(){
			return time;
		}
	}
}
