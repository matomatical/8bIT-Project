/*
 * Abstract class for anything that wants to be recorded as a dynamic
 * object (e.g. player, push block, anything that moves) using delta
 * compression (only storing initial state and subsequent changed states)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public abstract class DynamicRecorder : MonoBehaviour {


		/// keeping track of state changes
		private DynamicState state;
		private bool hasChanged;

		/// force change in first frame
		private bool theFirstTime = true;

		/// update the state change info, should be called
		/// once per frame
		public void CheckForChanges(){
			
			// always report changes first time

			if(theFirstTime){
				theFirstTime = false;
				hasChanged = true;
				state = GetState();
				return;
			}

			// in all other cases,

			// has the state actually changed?

			hasChanged = HasChanged (state, GetState());
			
			// if so, update change info for next time

			if(hasChanged){
				state = GetState();	
			}
		}

		/// changes smaller than this will be ignored
		private const float EPSILON = 0.0005f;

		/// compare two dynamic states to detect any changes
		private bool HasChanged(DynamicState old, DynamicState now){

			return (Mathf.Abs(old.position.x - now.position.x) > EPSILON)
				|| (Mathf.Abs(old.position.y - now.position.y) > EPSILON)
				|| (Mathf.Abs(old.velocity.x - now.velocity.x) > EPSILON)
				|| (Mathf.Abs(old.velocity.y - now.velocity.y) > EPSILON);
		}

		/// has the state actually changed since last frame?
		/// (for delta compression)
		public bool StateHasChanged() {
			return hasChanged;
		}

		/// what is the state that should go into the recording?
		public DynamicState LastState(){
			return state;
		}

		/// Helper method to actually get the dynamic state that should be
		/// recorded for this object, to see if it has changed
		public abstract DynamicState GetState();
	}
}

