using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class PressurePlateRecorder : MonoBehaviour, StaticRecorder {

		private PressurePlateController plate;
		
		private bool wasActive, hasChanged;

		void Start(){
			plate = GetComponent<PressurePlateController>();
		}

		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				changed = true;
				wasActive = playe.IsActive();
				return;
			}

			// in all other cases,

			// has the plate's activation changed?

			if(wasActive == plate.IsActive()){
				hasChanged = false;
			} else {
				hasChanged = true;
			}

			// either way, update wasActive

			wasActive = plate.IsActive();
		}


		/// undefined behaviour if you call this before calling
		/// check for changes at least once
		public bool StateHasChanged() {
			return hasChanged;
		}

		/// undefined behaviour if you call this before calling
		/// check for changes at least once
		/// // (is that bad?)
		public bool GetState(){
			return wasTaken;
		}
	}
}