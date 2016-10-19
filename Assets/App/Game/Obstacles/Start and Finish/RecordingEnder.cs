/*
 * Finish line component to end the recording
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Sam Beyer 	<sbeyer@student.unimelb.edu.au>
 * Mariam Shahid <mariams@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.recording {
	public class RecordingEnder : MonoBehaviour {
		
		static bool ended = false;

		public RecordingController recorder;

		void Start(){

			// link components together
			if (recorder == null) {
				recorder = FindObjectOfType<RecordingController> ();
			}
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {
					
					if(ended == false && recorder != null){

						recorder.EndRecording();

						ended = true;
					}
				}
			}
		}
	}
}
