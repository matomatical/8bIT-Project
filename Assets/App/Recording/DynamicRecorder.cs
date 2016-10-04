using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public interface DynamicRecorder {

		DynamicState GetState();
	}

	public struct DynamicState{
		public Vector2 position, velocity;
		public DynamicState(Vector2 position, Vector2 velocity){
			this.position = position;
			this.velocity = velocity;
		}
	}
}