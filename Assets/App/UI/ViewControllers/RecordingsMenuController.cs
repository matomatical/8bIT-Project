/*
 * Recordings menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace xyz._8bITProject.cooperace.ui {
	public class RecordingsMenuController : MonoBehaviour {

		// ui elements
		public Text levelNameText;
		
		// list of recordings
		string[] recordings;

		// the currently displayed recording
		int currentRecordingIndex_ = 0;
		int currentRecordingIndex {
			get {
				return currentRecordingIndex_;
			}
			set {
				// wraps around the list of maps
				if (value >= recordings.Length) {
					value = 0;
				}
				if (value < 0) {
					value = recordings.Length - 1;
				}
				currentRecordingIndex_ = value;

				UpdateRecordingDetails();
			}
		}
		
		void OnEnable() {
			// TODO: get a list of all the recordings
			// fake list for now
			recordings = new string[Maps.maps.Length * 2];
			int i = 0;
			foreach (string map in Maps.maps) {
				recordings[i++] = "recording of " + map + " 1";
				recordings[i++] = "recording of " + map + " 2";
			}

			// load in the first recording
			UpdateRecordingDetails();
		}
		
		// Load in the details of the current recording
		void UpdateRecordingDetails() {
			levelNameText.text = recordings[currentRecordingIndex];
		}

		// public methods to switch the currently displayed recording
		public void SwitchToNextRecording() {
			currentRecordingIndex += 1;
		}
		public void SwitchToPrevRecording() {
			currentRecordingIndex -= 1;
		}

		// public method to handle watch button behaviour
		public void WatchButtonHandler() {
			Debug.Log("TODO: start playback of recording");
		}

		// public method to handle play vs ghost button behaviour
		public void GhostButtonHandler() {
			Debug.Log("TODO: start playback of recording vs ghosts");
		}

		// public method to handle delete recording button behaviour
		public void DeleteButtonHandler() {
			Debug.Log("TODO: delete recording");
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}

	}
}
