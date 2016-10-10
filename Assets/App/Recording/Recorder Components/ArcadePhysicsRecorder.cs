/*
 * A concrete recorder object for collecting the state of an arcade physics
 * object each frame to build a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (ArcadePhysicsController))]
	public class ArcadePhysicsRecorder : DynamicRecorder {

		/// the arcade physics object to track
		private ArcadePhysicsController physics;

		void Start() {

			// link components

			// find the first active APC (there may be more than one)

			foreach(ArcadePhysicsController physics in
					GetComponents<ArcadePhysicsController> ()){

				if (physics.enabled) {
					this.physics = physics;
					break;
				}
			}
		}

		/// get the object's dynamic state this frame
		public override DynamicState GetState(){
			return new DynamicState(
				physics.GetPosition(), physics.GetVelocity());
		}
	}

}