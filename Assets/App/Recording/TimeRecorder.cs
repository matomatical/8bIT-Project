/*
 * Abstract class for anything that wants to be recorded as
 * a timer object (e.g. the game clock)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class TimeRecorder : MonoBehaviour {

		/// Get the time value for the recording for this frame
		public abstract float GetTime();
	}
}