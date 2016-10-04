using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class KeyBlockRecorder : MonoBehaviour, StaticRecorder {

		private KeyBlockController block;
		
		private bool wasOpen, hasChanged;

		void Start(){
			block = GetComponent<KeyBlockController>();
		}

		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				changed = true;
				wasOpen = block.IsOpen();
				return;
			}

			// in all other cases,

			// has the block's open-ness changed?

			if(wasOpen == block.IsOpen()){
				hasChanged = false;
			} else {
				hasChanged = true;
			}

			// either way, update wasOpen

			wasOpen = block.IsOpen();
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
			return wasOpen;
		}
	}
}