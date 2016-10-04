using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class ReplayingController : MonoBehaviour {

		// public int fps = 25; // for now, let's ignore this
		// public string level = "test";

		DynamicReplayer[] dynamics;
		StaticReplayer[] statics;
		TimeReplayer timer;

		Recording recording;
		int i = 0; // the frame we are up to
		bool isReplaying = true;

		void Start(){

			// get all replayables in this level

			// have to TRUST that they are in the same structure as 
			// for the recorder

			dynamics = FindObjectsOfType<DynamicReplayer> ();
			
			statics = FindObjectsOfType<StaticReplayer> ();
			
			timer = FindObjectOfType<TimeReplayer> ();

			StartReplaying (Recording.json);
		}

		public void StartReplaying(string recording){

			this.recording =
				JsonUtility.FromJson<Recording>(recording);

			isReplaying = true;
			i = 0;
		}

		public void PauseReplaying(){

			isReplaying = false;
		}

		public void ContinueReplaying(){

			isReplaying = true;
		}

		public void EndReplaying(){

			isReplaying = false;
		}

		void FixedUpdate(){

			// TODO: deal with FPS and pausing and stuff

			if(isReplaying){

				recording.ApplyFrame(i, timer, dynamics, statics);

				i++;	
			}
		}
	}
}