/*
 * Abstract class for anything that wants to be put into the recording
 * as a boolean state using delta compression (only storing initial state
 * and subsequent changes)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class StaticRecorder : MonoBehaviour {

		/// keeping track of state changes
		private bool wasTrue, hasChanged;

		/// force change in first frame
		bool theFirstTime = true;

		/// update the state change info, should be called
		/// once per frame
		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				hasChanged = true;
				wasTrue = _GetState();
				return;
			}

			// in all other cases,

			// has the state actually changed?

			if(wasTrue == _GetState(){
				hasChanged = false;
			} else {
				hasChanged = true;
			}

			// either way, update change info for
			// next time

			wasTrue = IsTrue();
		}

		/// has the state actually changed since last frame?
		/// (for delta compression)
		public bool StateHasChanged() {
			return hasChanged;
		}

		/// what is the state that should go into the recording?
		public bool GetState(){
			return wasTrue;
		}

		/// Helper method to actually get the state that should be
		/// compared for changes and eventually recorded. override this one!
		protected abstract bool _GetState();
	}
}