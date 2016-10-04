/*
 * A concrete replayer object for controlling a key object to follow along 
 * with a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (Key))]
	public class KeyReplayer : StaticReplayer {

		/// the key to control
		private Key key;

		void Start(){
			
			// link components

			key = GetComponent<Key>();
		}

		/// update the key's state to match this state
		public override void SetState(bool state){

			if(state){
				key.SimulateTake();
			} else {
				key.SimulateRestore();
			}
		}
		
	}
}
