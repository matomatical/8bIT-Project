/*
 * Global controller object for managing the recording process.
 * Builds a recording object by collecting states from recording 
 * objects each frame
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class RecordingController : MonoBehaviour {

		public const int fps = 25;
		public string level = "test";

		DynamicRecorder[] dynamics;
		StaticRecorder[] statics;
		TimeRecorder timer;

		Recording recording;
		bool isRecording = false, hasStarted = false;

		void Start(){

			// get all recordables in this level

			dynamics = FindObjectsOfType<DynamicRecorder> ();
			
			statics = FindObjectsOfType<StaticRecorder> ();
			
			timer = FindObjectOfType<TimeRecorder> ();

			// TODO: someone else should start recording!?
			// then they can also pause it etc

			StartRecording ();
		}

		/// start recording if we haven't already
		public void StartRecording(){

			if(!hasStarted){
				recording = new Recording(level, fps);
				hasStarted = true;
				isRecording = true;
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

			// TODO: deal with FPS and pausing and stuff

			if(isRecording){

				recording.AddFrame(timer, dynamics, statics);
			}
		}

		public string GetRecording () {

			return JsonUtility.ToJson (recording);
		}
	}
}