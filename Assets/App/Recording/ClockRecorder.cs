/*
 * A concrete recorder object for collecting the time off a clock object
 * each frame to build a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (ClockController))]
	public class ClockRecorder : TimeRecorder {
		
		/// the clock object to track
		private ClockController clock;

		void Start() {

			// link components

			clock = GetComponent<ClockController>();
		}

		/// get the clock's time this frame
		public override float GetTime(){
			return clock.GetTime();
		}
	}

}