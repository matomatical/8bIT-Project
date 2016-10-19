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

namespace xyz._8bITProject.cooperace.ui {
	public class RecordingsMenuController : MonoBehaviour {

		// ui elements
		public Text levelNameText;
		public Text messageText;
		public GameObject confirmationPanel;
		public RawImage preview;


		// list of recordings
		string[] recordings;

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
			recordings = ListRecordings();

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
			currentRecordingIndex += 1;
		}
		public void SwitchToPrevRecording() {
			currentRecordingIndex -= 1;
		}

		// public method to handle watch button behaviour
		public void WatchButtonHandler() {
			TryLoadRecording (false);
		}

		// public method to handle play vs ghost button behaviour
		public void GhostButtonHandler() {
			TryLoadRecording (true);
		}

		// helper method to load recording and go to scene
		void TryLoadRecording(bool playagainst){
			if (recordingsExist) {
				SceneManager.recording = TryReadRecording(recordings [currentRecordingIndex]);
				if (SceneManager.recording == null) {
					return;
				}

				SceneManager.gameType = (playagainst ? GameType.MULTI : GameType.REWATCH);
				SceneManager.levelToLoad = SceneManager.recording.level;
				SceneManager.playingAgainstGhosts = playagainst;

				if (playagainst) {
					#if UNITY_EDITOR
					SceneManager.Load ("Game Scene");
					#else
					DisplayMessage("Starting Game...");
					MultiPlayerController.Instance.roomListener = this;
					MultiPlayerController.Instance.StartMPGame(currentLevelIndex_);
					#endif
				} else {
					SceneManager.Load ("Game Scene");
				}
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
				DeleteRecording(recordings [currentRecordingIndex]);
			} catch (PersistentStorageException) {
				DisplayMessage ("Problem reading recording file.\n\nTry another.");
			}

			confirmationPanel.SetActive (false);

			// refresh list, also make sure recordings still exists
			recordings = ListRecordings();
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

		// helper methods to access files
		// (dealing extensions and subfolders)

		string[] ListRecordings(){
			
			string[] ls = PersistentStorage.ListFiles ("Recordings/", false, "*.crr");
			for (int i = 0; i < ls.Length; i++) {
				//TODO: strip extensions
			}

			if (ls.Length < 1) {
				recordingsExist = false;
				DisplayMessage ("No recordings found.\n\nPlay a game and\nsave a recording.");
			}
			return ls;
		}

		Recording TryReadRecording(string recordingName){
			try {
				//TODO: strip filename earlier, add it back here...
				string recordingString = PersistentStorage.Read("Recordings/" + recordingName);
				return Recording.FromString(recordingString);
			} catch (RecordingFormatException) {
				// something went wrong reading the recording
				DisplayMessage ("Problem reading recording file.");
			} catch (PersistentStorageException) {
				DisplayMessage ("Problem reading recording file.");
			}
			return null;
		}

		void DeleteRecording(string recordingName){

			//TODO: strip filename earlier, add it back here...
			PersistentStorage.Delete ("Recordings/" + recordingName);
		}
	}
}
