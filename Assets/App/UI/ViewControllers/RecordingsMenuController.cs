/*
 * Recordings menu logic.
 *
 * Athir Saleem    <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class RecordingsMenuController : MonoBehaviour {

		// ui elements
		public Text levelNameText;
		public Text messageText;
		public GameObject confirmationPanel;
		public RawImage preview;

		// list of recordings
		string[] recordings;

		// true if list of recordings is non-empty
		bool recordingsExist {
			get {
				return recordings != null && recordings.Length > 0;
			}
		}

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
			recordings = RecordingFileManager.ListRecordings();

			// load in the first recording
			UpdateRecordingDetails();
		}
		
		// Load in the details of the current recording
		void UpdateRecordingDetails() {
			if (recordingsExist) {
				HideMessage ();
				string levelName = recordings [currentRecordingIndex];
				levelNameText.text = levelName;
				LevelPreview.LoadPreview (levelName, preview);
			} else {
				DisplayMessage ("No recordings found.\n\nPlay a game and\nsave a recording.");
			}
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
			
			if (recordingsExist) {

				Recording recording = null;

				try{
					recording = RecordingFileManager.TryReadRecording(recordings [currentRecordingIndex]);
				} catch (RecordingFormatException) {
					// something went wrong reading the recording
					DisplayMessage ("Problem reading recording file.");
				} catch (PersistentStorageException) {
					DisplayMessage ("Problem reading recording file.");
				}

				if (recording == null) {
					return;
				}

				SceneManager.StartReplayGame (recording.level, recording);
			}
		}

		// public method to handle play vs ghost button behaviour
		public void GhostButtonHandler() {
			
			if (recordingsExist) {

				Recording recording = null;
				try{
					recording = RecordingFileManager.TryReadRecording(recordings [currentRecordingIndex]);
				} catch (RecordingFormatException) {
					// something went wrong reading the recording
					DisplayMessage ("Problem reading recording file.");
				} catch (PersistentStorageException) {
					DisplayMessage ("Problem reading recording file.");
				}

				if (recording == null) {
					return;
				}

				// set up matchmaking for playagainst game,
				// and start matchmaking!

				MatchmakingMenuController.options =
					new MatchmakingMenuController.Options (
						UIState.RecordingsMenu,
						GameType.GHOST,
						recording.level,
						recording
					);
				
				UIStateMachine.instance.GoTo(UIState.Matchmaking);
			}
		}

		// public method to handle delete recording button behaviour
		public void DeleteButtonHandler() {
			if (recordingsExist) {
				HideMessage ();
				confirmationPanel.SetActive (true);
			}
		}

		// public method to handle confirm-deletion button behaviour
		public void ConfirmButtonHandler() {

			try {
				RecordingFileManager.DeleteRecording(recordings [currentRecordingIndex]);
			} catch (PersistentStorageException) {
				DisplayMessage ("Problem reading recording file.\n\nTry another.");
			}

			confirmationPanel.SetActive (false);

			// refresh list, also make sure recordings still exists
			recordings = RecordingFileManager.ListRecordings();
			currentRecordingIndex = currentRecordingIndex; // ensures validity of index
			UpdateRecordingDetails();
		}

		// public method to handle cancel-deletion button behaviour
		public void CancelButtonHandler() {
			confirmationPanel.SetActive (false);
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}

		// methods to actually change the ui
		protected void DisplayMessage(string message) {
			// hide level name
			levelNameText.text = "";
			
			messageText.text = message;

			// make sure message is currently displayed
			messageText.gameObject.SetActive(true);
		}
		protected void HideMessage(){
			messageText.text = "";

			// make sure message is no longer displayed
			messageText.gameObject.SetActive(false);
		}
	}
}
