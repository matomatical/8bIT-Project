using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class StaticRecorder : MonoBehaviour {

		public abstract void CheckForChanges();
		public abstract bool GetState();
		public abstract bool StateHasChanged();
	}
}