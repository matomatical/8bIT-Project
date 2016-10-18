/*
 * A concrete replayer object for controlling an arcade physics controller
 * to follow along with a recording (well, actually, we need to do this
 * through a remote physics controller!)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (RemotePhysicsController))]
	public class ArcadePhysicsReplayer : DynamicReplayer {

		/// the physics object to control
		private RemotePhysicsController remote;

		void Start() {

			// link components


			foreach(RemotePhysicsController remote in
				GetComponents<RemotePhysicsController> ()){

				if (remote.enabled) {
					this.remote = remote;
					break;
				}
			}
		}

		/// set the object's state to match this state
		/// TODO: interpolate between multiple previous states!!
		public override void SetState(DynamicState state){
			remote.SetState(state.position, state.velocity);
		}
	}
}