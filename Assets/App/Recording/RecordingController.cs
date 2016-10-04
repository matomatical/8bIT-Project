using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class RecordingController : MonoBehaviour {

		public int fps = 25; // for now, let's ignore this
		public string level = "test";

		DynamicRecorder[] dynamics;
		StaticRecorder[] statics;
		TimeRecorder timer;

		Recording recording;
		bool isRecording = true;

		void Start(){

			// get all recordables in this level

			dynamics = FindObjectsOfType(typeof(DynamicRecorder))
				as  DynamicRecorder[];
			
			statics = FindObjectsOfType(typeof(StaticRecorder))
				as  StaticRecorder[];
			
			timer = FindObjectOfType(typeof(TimeRecorder))
				as TimeRecorder;
		}

		public void StartRecording(){

			recording = new Recording(level, fps);

			isRecording = true;
		}

		public void PauseRecording(){

			isRecording = false;
		}

		public void ContinueRecording(){

			isRecording = true;
		}

		public void EndRecording(){

			isRecording = false;
		}

		void FixedUpdate(){

			// TODO: deal with FPS and pausing and stuff

			if(isRecording){

				Recording.AddFrame(timer, dynamics, statics);
	
			}
		}

		public Recording GetRecording () {

			return recording;
		}
	}
}