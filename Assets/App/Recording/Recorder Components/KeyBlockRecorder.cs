/*
 * A concrete recorder object for collecting the state of a key block
 * each frame to build a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (KeyBlock))]
	public class KeyBlockRecorder : StaticRecorder {

		/// the key block to track
		private KeyBlock block;

		public void Start(){

			// link components

			block = GetComponent<KeyBlock>();
		}

		/// get the actual state of the key block being tracked this frame
		protected override bool GetState(){
			return block.IsOpen();
		}
	}
}
