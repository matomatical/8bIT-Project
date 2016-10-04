using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class KeyRecorder : MonoBehaviour, StaticRecorder {

		private Key key;
		
		private bool wasTaken, hasChanged;

		void Start(){
			key = GetComponent<Key>();
		}

		bool theFirstTime = true;

		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				hasChanged = true;
				wasTaken = key.IsTaken();
				return;
			}

			// in all other cases,

			// has the key's taken-ness changed?

			if(wasTaken == key.IsTaken()){
				hasChanged = false;
			} else {
				hasChanged = true;
			}

			// either way, update wasOpen

			wasTaken = key.IsTaken();
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