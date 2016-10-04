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

			dynamics = FindObjectsOfType<DynamicRecorder> ();
			
			statics = FindObjectsOfType<StaticRecorder> ();
			
			timer = FindObjectOfType<TimeRecorder> ();

			StartRecording ();
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

				recording.AddFrame(timer, dynamics, statics);
	
			}
		}

		public string GetRecording () {

			return JsonUtility.ToJson (recording);
		}
	}
}