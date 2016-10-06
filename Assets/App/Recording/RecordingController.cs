/*
 * Global controller object for managing the recording process.
 * Builds a recording object by collecting states from recording 
 * objects each frame
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class RecordingController : MonoBehaviour {

		DynamicRecorder[] dynamics;
		StaticRecorder[] statics;
		ClockController timer;

		Recording recording;
		bool isRecording = false, hasStarted = false;

		public const int fixedUpdatesPerFrame = 2; // gives FPS of 25 by default
		private int fixedUpdatesSinceLastFrame = 0;
		private int fps;

		void Start(){

			// calculate fps

			fps = (int)(1 / (fixedUpdatesPerFrame*Time.fixedDeltaTime));


			// get all recordables in this level

			dynamics = FindObjectsOfType<DynamicRecorder> ();
			
			statics = FindObjectsOfType<StaticRecorder> ();
			
			timer = FindObjectOfType<ClockController>();


			// TODO: someone else should start recording!?
			// then they can also pause it etc

 			// todo: use actual level nae

			StartRecording (Recording.global_level);
		}

		/// start recording if we haven't already
		public void StartRecording(string level){

			if(!hasStarted){
				recording = new Recording(level, this.fps);
				hasStarted = true;
				isRecording = true;

				// make sure we'll record the first frame!
				fixedUpdatesSinceLastFrame = fixedUpdatesPerFrame - 1;
			}
		}

		public void PauseRecording(){
			if(hasStarted){
				isRecording = false;
			}
		}

		public void ContinueRecording(){
			if(hasStarted){
				isRecording = true;
			}
		}

		public void EndRecording(){
			if(hasStarted){
				isRecording = false;
				hasStarted = false;
			}
		}

		void FixedUpdate(){

			// record a frame if we're recording at the moment
			if(isRecording){

				// count another frame

				fixedUpdatesSinceLastFrame = fixedUpdatesSinceLastFrame + 1;

				// we're going to 

				if(fixedUpdatesSinceLastFrame == fixedUpdatesPerFrame){

					// THIS is one of the updates where we add a frame
					// to the recording :)

					recording.AddFrame(timer, dynamics, statics);	

					fixedUpdatesSinceLastFrame = 0;
				}

				
			}
		}

		public string GetRecording () {

			return JsonUtility.ToJson (recording);
		}


	}
}