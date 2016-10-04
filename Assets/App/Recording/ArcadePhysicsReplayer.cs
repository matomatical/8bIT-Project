using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class ArcadePhysicsReplayer : DynamicReplayer {

		private RemotePhysicsController remote;

		void Start() {
			remote = GetComponent<RemotePhysicsController> ();
		}

		public override void SetState(DynamicState state){
			remote.SetState(state.position, state.velocity);
		}
	}
}