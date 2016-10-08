/*
 * Abstract class for anything that wants to be replayed
 * as a dynamic object (e.g. push blocks and players, anything that moves)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class DynamicReplayer : MonoBehaviour {

		/// set the state from a recording
		public abstract void SetState(DynamicState state);
	}
}