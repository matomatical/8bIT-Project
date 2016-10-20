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
	public class RecordingsMenuController : MonoBehaviour, IRoomListener {

		// ui elements
		public Text levelNameText;
		public Text messageText;
		public GameObject confirmationPanel;
		public RawImage preview;


		// list of recordings
		string[] recordings;

		/// The currently selected recording
		Recording recording;

		// list of recordings is nonempty
		bool recordingsExist = true;

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

			if (recordings.Length < 1) {
				recordingsExist = false;
				DisplayMessage ("No recordings found.\n\nPlay a game and\nsave a recording.");
			}

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
			}
		}

		// public methods to switch the currently displayed recording
		public void SwitchToNextRecording() {
			MultiPlayerController.Instance.StopMatchMaking ();
			currentRecordingIndex += 1;
		}
		public void SwitchToPrevRecording() {
			MultiPlayerController.Instance.StopMatchMaking ();
			currentRecordingIndex -= 1;
		}

		// public method to handle watch button behaviour
		public void WatchButtonHandler() {
			MultiPlayerController.Instance.StopMatchMaking ();
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
			MultiPlayerController.Instance.StopMatchMaking ();
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
					
				this.recording = recording;
				MultiPlayerController.Instance.StartMatchMaking (recording.level, this);
			}
		}

		// public method to handle delete recording button behaviour
		public void DeleteButtonHandler() {
			MultiPlayerController.Instance.StopMatchMaking ();
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
			
			if (recordings.Length < 1) {
				recordingsExist = false;
				DisplayMessage ("No recordings found.\n\nPlay a game and save a recording.");
			}
		}

		// public method to handle cancel-deletion button behaviour
		public void CancelButtonHandler() {
			confirmationPanel.SetActive (false);
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			MultiPlayerController.Instance.StopMatchMaking ();
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

		// IListener methods
		public void SetRoomStatusMessage(string message) {
			DisplayMessage(message);
		}
		public void HideRoom() {
			 HideMessage();
		}
		public void OnConnectionComplete(){
			SceneManager.StartPlayagainstGame(this.recording.level, this.recording);
		}
	}
}
