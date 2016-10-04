using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class ArcadePhysicsRecorder : MonoBehaviour, DynamicRecorder {

		private ArcadePhysicsController physics;

		void Start() {
			physics = GetComponent<ArcadePhysicsController> ();
		}

		public DynamicState GetState(){
			return new DynamicState(
				physics.GetPosition(), physics.GetVelocity());
		}
	}

}