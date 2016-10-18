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
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	public class RecordingFinalizer : MonoBehaviour {

		
		static bool finalized = false;

		RecordingController recorder;

		void Start(){

			// link components together

			recorder = FindObjectOfType<RecordingController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {
					
					if(finalized == false){

						if(recorder != null){

							recorder.EndRecording();

							SceneManager.newRecording = recorder.GetRecording();
							
						}	
					}
				}
			}
		}
	}
}
