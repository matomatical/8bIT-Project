using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public interface StaticRecorder {

		void CheckForChanges();
		bool GetState();
		bool StateHasChanged();
	}
}