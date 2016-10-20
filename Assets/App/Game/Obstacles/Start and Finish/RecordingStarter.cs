/*
 * Start line component to begin the recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.recording {
	public class RecordingStarter : MonoBehaviour {

		static bool started = false;

		public RecordingController recorder;

		void Start(){

			// reset static state between games
			started = false;

			// link components together
			if (recorder == null) {
				recorder = FindObjectOfType<RecordingController> ();
			}
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				if (enabled) { // only trigger if this component is on
					
					if(started == false && recorder != null){
					

						recorder.StartRecording();

						started = true;
					}
				}
			}
		}
	}
}
