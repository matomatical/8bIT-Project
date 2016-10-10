/*
 * A concrete replayer object for controlling a key block to follow along 
 * with a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (KeyBlock))]
	public class KeyBlockReplayer : StaticReplayer {

		/// the key block to control
		private KeyBlock block;

		void Start(){

			// link components

			block = GetComponent<KeyBlock>();
		}

		/// update the key block's state to match this state
		public override void SetState(bool state){

			if(state){
				block.Open();
			} else {
				block.Close();
			}
		}
		
	}
}
