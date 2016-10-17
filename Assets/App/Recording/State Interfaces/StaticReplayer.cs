/*
 * Abstract class for anything that wants to replay something
 * with a boolean state. The SetState method is called
 * once at the beginning and then also every time the state changes
 * in the recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class StaticReplayer : MonoBehaviour {

		/// Notify the replay object of a state change (to state)
		/// in the recording (called once in the beginning to set
		/// the initial state, too)
		public abstract void SetState(bool state);

	}
}