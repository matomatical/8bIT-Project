using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockController : MonoBehaviour {

	float time = 0;
	bool timing = true;

	private Text label; // the text object we're attached to

	void Start () {
		label = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		
		if(timing){

			// count
			time += Time.deltaTime;

			// convert to clock time string
			int mins = (int) (time / 60);
			int secs = ((int) time) % 60;
			string text = string.Format("{0:00}:{1:00}", mins, secs);

			// lazy refresh text
			if(!(label.text == text)){
				label.text = text;
			}
		}
	}

	public void StopTiming(){
		timing = false;
	}

	public void StartTiming(){
		timing = true;
	}
}
