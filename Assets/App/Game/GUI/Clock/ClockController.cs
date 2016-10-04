/*
 * Controller for the time text ui object.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class ClockController : MonoBehaviour {

		float time = 0;
		bool isTiming = true;

		Text label; // the text object we're attached to

		void Start () {
			label = GetComponent<Text>();
		}

		void Update () {
			if (isTiming) {
				// count
				time += Time.deltaTime;

				// convert to clock time string
				int mins = (int) (time / 60);
				int secs = ((int) time) % 60;
				int msec = (int)(10*(time - mins * 60 - secs));
				string text = string.Format("{0:00}:{1:00}.{2:0}", mins, secs, msec);

				// lazy refresh text
				if (label.text != text) {
					label.text = text;
				}
			}
		}

		public void StopTiming() {
			isTiming = false;
		}

		public void StartTiming() {
			isTiming = true;
		}

		public float GetTime(){
			return time;
		}

		public void SetTime(float time){
			this.time = time;
		}
	}
}
