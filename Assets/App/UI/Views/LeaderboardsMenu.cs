using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {
	public class LeaderboardsMenu : MonoBehaviour {

		public GameObject scoresList;

		Leaderboards lb;
		int currentLevel;

		void Start () {
			lb = new Leaderboards();
			currentLevel = 0;
			LoadLevelStats();
		}

		void LoadLevelStats() {
			string mapName = Maps.maps[currentLevel];
			Score[] scores = lb.RequestScores(mapName);
			LeaderboardItem[] items = scoresList.GetComponentsInChildren<LeaderboardItem>();

			for (int i = 0; i < items.Length; i++) {
				LeaderboardItem item = items[i];
				item.player1 = scores[i].player1;
				item.player2 = scores[i].player2;
				item.score = scores[i].time.ToString();
			}
		}

		public void SwitchToNextLevel() {
			currentLevel += 1;
			if (currentLevel >= Maps.maps.Length) {
				currentLevel = 0;
			}
			LoadLevelStats();
		}

		public void SwitchToPrevLevel() {
			currentLevel += 1;
			if (currentLevel < 0) {
				currentLevel = Maps.maps.Length - 1;
			}
			LoadLevelStats();
		}

	}
}