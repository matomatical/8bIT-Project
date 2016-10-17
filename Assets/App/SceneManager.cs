/*
 * Scene manager, responsible for loading new scenes as well as communication
 * between them.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	
	public static class SceneManager {

		public static string levelToLoad;
		public static GameType gameType = GameType.MULTI;
		public static string recording;
		public static bool playingAgainstGhosts;

		public static void Load(string name) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		}

	}

	public enum GameType { SINGLE, MULTI, REWATCH }
}