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

		/// The Clock to stop when we cross the line
		ClockController clock;

		void Start(){

			// find the clock
			clock = FindObjectOfType<ClockController> ();
		
			// reset static state between games
			ended = false;

			// link components together
			if (recorder == null) {
				recorder = FindObjectOfType<RecordingController> ();
			}
		}

		void OnTriggerEnter2D (Collider2D other) {
			if (ArcadePhysics.SameWorld(this, other)) {

				// only trigger if this component is on
				if (enabled) {
					
					if(ended == false && recorder != null){

						recorder.EndRecording(clock.GetTime());

						ended = true;
					}
				}
			}
		}
	}
}
