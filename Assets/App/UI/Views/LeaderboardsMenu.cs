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

namespace xyz._8bITProject.cooperace.leaderboard {
	public class LeaderboardsMenu : MonoBehaviour {

		// ui elements
		public GameObject scoresList;
		public GameObject messageText;
		LeaderboardItem[] items;

		Leaderboards lb = new Leaderboards();
		int currentLevel = 0;
		
		Score[] scoresToDisplay;
		string messageToDisplay;
		
		void Start() {
			items = scoresList.GetComponentsInChildren<LeaderboardItem>();
			currentLevel = 0;
			LoadLevelStats();
        }
		
		void Update() {
			if (scoresToDisplay != null) {
				for (int i = 0; i < items.Length; i++) {
					LeaderboardItem item = items[i];
					item.player1 = scoresToDisplay[i].player1;
					item.player2 = scoresToDisplay[i].player2;
					item.score = scoresToDisplay[i].time.ToString();
				}
				scoresToDisplay = null;
				
				scoresList.SetActive(true);
				messageText.SetActive(false);
			}

			if (messageToDisplay != null) {
				messageText.GetComponent<Text>().text = messageToDisplay;
				messageToDisplay = null;
				
				scoresList.SetActive(false);
				messageText.SetActive(true);
			}
		}

		// fetches the actual leaderboard scores
		void LoadLevelStats() {
			string mapName = Maps.maps[currentLevel];
			DisplayMessage("Loading scores for " + mapName);
			lb.RequestScoresAsync(mapName,
				new Action<Score[], ServerException>(OnServerResponse));
		}
		void OnServerResponse(Score[] scores, ServerException error) {
			if (error != null) {
				DisplayMessage("Unable to contact server, please check your connection.");
			} else {
				DisplayNames(scores);
			}
		}
		
		// methods to mark the ui to change
		// necessary as unity doesn't allow game objects to change from threads
		void DisplayMessage(string message) {
			messageToDisplay = message;
		}
		void DisplayNames(Score[] scores) {
			scoresToDisplay = scores;
		}

		// public methods to switch the currently displayed score
		// wraps around the list of maps
		public void SwitchToNextLevel() {
			currentLevel += 1;
			if (currentLevel >= Maps.maps.Length) {
				currentLevel = 0;
			}
			LoadLevelStats();
		}
		public void SwitchToPrevLevel() {
			currentLevel -= 1;
			if (currentLevel < 0) {
				currentLevel = Maps.maps.Length - 1;
			}
			LoadLevelStats();
		}

	}
}