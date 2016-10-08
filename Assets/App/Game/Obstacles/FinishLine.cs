/*
 * Finish line logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	public class FinishLine : MonoBehaviour {

		ClockController clock;

		void Start () {
			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			// the timer is stopped when the player touches the finish line
			if (other.gameObject.CompareTag("Player")) {

				clock.StopTiming();

				RecordingController controller = FindObjectOfType<RecordingController>();

				controller.EndRecording ();
				Recording.json = controller.GetRecording ();

				// TODO: pass properly, not with global variable!

				SceneManager.Load("Static Replay Level Game Scene");
			}
		}

	}
}
