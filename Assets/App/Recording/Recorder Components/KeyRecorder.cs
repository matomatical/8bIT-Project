/*
 * A concrete recorder object for collecting the state of a key object
 * each frame to build a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (Key))]
	public class KeyRecorder : StaticRecorder {

		/// the key to track
		private Key key;

		void Start(){

			// link components

			key = GetComponent<Key>();
		}

		/// get the actual state of the key being tracked this frame
		protected override bool GetState(){
			return key.IsTaken();
		}
	}
}