/*
 * Global controller object for managing the replaying process.
 * Reads a recording object and dispatches its recorded states to
 * replayer objets each frame, so that they can follow along
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class ReplayingController : MonoBehaviour {

		int fps;

		DynamicReplayer[] dynamics;
		StaticReplayer[] statics;
		TimeReplayer timer;

		Recording recording;
		int i = 0; // the frame in the recording that we are up to

		bool isReplaying = false, hasStarted = false;

		void Start(){

			// get all replayables in this level

			dynamics = FindObjectsOfType<DynamicReplayer> ();
			
			statics = FindObjectsOfType<StaticReplayer> ();
			
			timer = FindObjectOfType<TimeReplayer> ();

			// ATM, have to TRUST that they are in the same structure as 
			// for the recorder
			// TODO: guarantee this by dynamically setting up replay objects


			// TODO: someone else should be setting up this recording
			// then they can also pause it etc

			SetRecording(Recording.json);

			StartReplaying ();
		}

		public void SetRecording(string recording){
			this.recording =
				JsonUtility.FromJson<Recording>(recording);
			this.fps = recording.fps;
			// this.level = recording.level;
		}

		/// start replaying if we haven't already
		public void StartReplaying(){

			if(!hasStarted){
				if(recording != null){ // recording has been set
					isReplaying = true;
					i = 0;
					hasStarted = true;
				}
			}
		}

		public void PauseReplaying(){
			if(hasStarted){
				isReplaying = false;	
			}
		}

		public void ContinueReplaying(){
			if(hasStarted){
				isReplaying = true;	
			}
		}

		public void EndReplaying(){
			if(hasStarted){
				isReplaying = false;
				hsStarted = false;	
			}
		}

		void FixedUpdate(){

			// TODO: deal with FPS and pausing and stuff
			// i think we'll end up indexing by time instead of
			// frame number! then recording can have an index/head
			// like a real video tape would

			if(isReplaying){

				recording.ApplyFrame(i, timer, dynamics, statics);

				i++;
			}
		}
	}
}