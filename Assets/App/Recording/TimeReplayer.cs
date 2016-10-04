/*
 * Abstract class for anything that wants to be replayed
 * as a timer object (e.g. a game clock)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class TimeReplayer : MonoBehaviour {

		/// Set the time to a value from a recording
		public abstract void SetTime(float time);
	}
}
