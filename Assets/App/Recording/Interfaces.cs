using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public interface TimeRecorder {

		public float GetTime();
	}

	public interface DynamicRecorder {
		public DynamicState GetState();
	}

	public interface StaticRecorder {
		public void CheckForChanges();
		public bool GetState();
		public bool StateHasChanged();
	}
}