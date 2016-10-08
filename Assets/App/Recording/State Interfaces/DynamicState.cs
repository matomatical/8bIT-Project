/*
 * Struct for sending two-vector dynamic state information (position, velocity)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public struct DynamicState{

		/// the vectors comprising the state
		public Vector2 position, velocity;

		/// make a new state struct with these positions/velocities
		public DynamicState(Vector2 position, Vector2 velocity){
			this.position = position;
			this.velocity = velocity;
		}
	}
}