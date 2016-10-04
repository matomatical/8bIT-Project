/*
 * Abstract class for anything that wants to be recorded as a dynamic
 * object (e.g. player, push block, anything that moves)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class DynamicRecorder : MonoBehaviour {

		/// query the dynamic state of the object (position, velocity pair)
		public abstract DynamicState GetState();
	}
}

