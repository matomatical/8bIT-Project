using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class PressurePlateRecorder : MonoBehaviour, StaticRecorder {

		private PressurePlate plate;
		
		private bool wasPressed, hasChanged;

		void Start(){
			plate = GetComponent<PressurePlate>();
		}

		bool theFirstTime = true;

		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				hasChanged = true;
				wasPressed = plate.IsPressed();
				return;
			}

			// in all other cases,

			// has the plate's activation changed?

			if(wasPressed == plate.IsPressed()){
				hasChanged = false;
			} else {
				hasChanged = true;
			}

			// either way, update wasActive

			wasPressed = plate.IsPressed();
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
			return wasPressed;
		}
	}
}