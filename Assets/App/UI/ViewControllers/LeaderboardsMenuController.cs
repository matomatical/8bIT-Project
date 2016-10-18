/*
 * Logic for the leaderboards menu.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using xyz._8bITProject.cooperace.leaderboard;

namespace xyz._8bITProject.cooperace.ui {
	public class LeaderboardsMenuController : MonoBehaviour {

		// ui elements
		public Text levelNameText;
		public GameObject scoresList;
		public GameObject messageText;
		LeaderboardItem[] items;

		// api
		Leaderboards lb = new Leaderboards();

		// values to change ui to on update
		Score[] scoresToDisplay;
		string messageToDisplay;

		// the currently displayed level
		int currentLevelIndex_ = 0;
		int currentLevelIndex {
			get {
				return currentLevelIndex_;
			}
			set {
				// wraps around the list of maps
				if (value >= Maps.maps.Length) {
					value = 0;
				}
				if (value < 0) {
					value = Maps.maps.Length - 1;
				}
				currentLevelIndex_ = value;
				currentLevelName = Maps.maps[currentLevelIndex];
			}
		}
		protected string currentLevelName;
		
		void OnEnable() {
			items = scoresList.GetComponentsInChildren<LeaderboardItem>();

			// make sure something is loaded when visible
			LoadLevelStats();
		}

		void Update() {
			if (scoresToDisplay != null) {
				// update scores if necessary
				DisplayNames(scoresToDisplay);
				scoresToDisplay = null;

			} else if (messageToDisplay != null) {
				// update message if necessary
				DisplayMessage(messageToDisplay);
				messageToDisplay = null;
			}
		}

		// fetches the actual leaderboard scores
		protected void LoadLevelStats() {
			DisplayMessage("Loading scores for " + currentLevelName);
			lb.RequestScoresAsync(currentLevelName,
				new Action<ScoresResponse, ServerException>(OnServerResponse));
		}
		// callback for when server responds
		void OnServerResponse(ScoresResponse scores, ServerException error) {
			// make sure this response isn't out of date
			if (scores.level == currentLevelName) {
				if (error != null) {
					QueueMessage("Unable to contact server, please check your connection.");
				} else if (scores.leaders != null) {
					QueueNames(scores.leaders);
				}
			}
		}

		// methods to queue the ui to change
		// necessary as unity doesn't allow game objects to change from threads
		void QueueMessage(string message) {
			messageToDisplay = message;
		}
		void QueueNames(Score[] scores) {
			scoresToDisplay = scores;
		}

		// methods to actually change the ui
		protected void DisplayMessage(string message) {
			// hide level name
			levelNameText.text = "";

			messageText.GetComponent<Text>().text = message;

			// make sure message is currently displayed
			scoresList.SetActive(false);
			messageText.SetActive(true);
		}
		void DisplayNames(Score[] scores) {
			// update level name as well
			levelNameText.text = currentLevelName;

			for (int i = 0; i < items.Length; i++) {
				LeaderboardItem item = items[i];
				item.player1 = scores[i].player1;
				item.player2 = scores[i].player2;
				item.score = scores[i].time.ToString();
			}

			// make sure score list is currently displayed
			scoresList.SetActive(true);
			messageText.SetActive(false);
		}

		// public methods to switch the currently displayed score
		public void SwitchToNextLevel() {
			currentLevelIndex += 1;
			LoadLevelStats();
		}
		public void SwitchToPrevLevel() {
			currentLevelIndex -= 1;
			LoadLevelStats();
		}
		
		// public method to handle back button behaviour
		public void BackButtonHandler() {
			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}

	}
}
