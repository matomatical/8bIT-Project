/*
 * Global controller object for managing the replaying process.
 * Reads a recording object and dispatches its recorded states to
 * replayer objets each frame, so that they can follow along
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class ReplayingController : MonoBehaviour {

		DynamicReplayer[] dynamics;
		StaticReplayer[] statics;
		ClockController timer;

		Recording recording;

		bool isReplaying = false, hasStarted = false;

		void Start(){

			// get all replayables in this level

			dynamics = FindObjectsOfType<DynamicReplayer> ();
			
			statics = FindObjectsOfType<StaticReplayer> ();
			
			timer = FindObjectOfType<ClockController> ();

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
		}

		/// start replaying if we haven't already
		public void StartReplaying(){

			if(!hasStarted){
				if(recording != null){ // recording has been set
					isReplaying = true;
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
				hasStarted = false;	
			}
		}

		void FixedUpdate(){

			if(isReplaying){

				recording.ApplyFrame(timer, dynamics, statics);
			}
		}
	}
}