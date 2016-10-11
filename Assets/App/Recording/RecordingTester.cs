using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.recording{
	
	public class RecordingTester : MonoBehaviour {

		void Start(){
			FindObjectOfType<RecordingController> ().StartRecording ("test level");
		}

		void OnTriggerEnter2D(Collider2D other){

			if (enabled) { // only trigger if this component is on
				
				if (other.gameObject.CompareTag ("Player")) {

					RecordingController controller = FindObjectOfType<RecordingController> ();

					controller.EndRecording ();
					Recording.jsonRecordingString = controller.GetRecording ();

					// Debug.Log (Recording.jsonRecordingString);

					SceneManager.Load ("Static Playagainst Level Game Scene");
				}
			}
		}
	}
}