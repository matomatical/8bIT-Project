/*
 * A concrete replayer object for controlling a GUI clock to follow along 
 * with the times from a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (ClockController))]
	public class ClockReplayer : TimeReplayer {

		/// the clock to control
		private ClockController clock;

		void Start() {

			// link components

			clock = GetComponent<ClockController>();
		}

		/// set the clock's time to match this time
		public override void SetTime(float time){
			clock.SetTime(time);
		}
	}
}

