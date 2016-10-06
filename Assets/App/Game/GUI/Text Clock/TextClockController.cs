/*
 * Controller for the time text ui object.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof(Text))]
	public class TextClockController : MonoBehaviour {

		/// the text object we're attached to
		Text label;

		/// The game clock

		ClockController clock;

		void Start () {

			// link components

			label = GetComponent<Text>();

			// find the game clock

			clock = FindObjectOfType<ClockController> ();
		}

		void Update () {

			// what time is it?

			float time = clock.GetTime ();

			// convert to text string

			int mins = (int) (time / 60);
			int secs = ((int) time) % 60;
			int msec = (int)(10*(time - mins * 60 - secs));

			string text = string.Format("{0:00}:{1:00}.{2:0}", mins, secs, msec);

			// lazy refresh text in label (only when changed)

			if (label.text != text) {
				label.text = text;
			}
		}
	}
}
