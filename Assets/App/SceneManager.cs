/*
 * Scene manager, responsible for loading new scenes as well as communication
 * between them.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	
	public static class SceneManager {

		#if UNITY_EDITOR
		public static GameType gameType = GameType.SINGLE;
		#else
		public static GameType gameType = GameType.MULTI;
		#endif

		public static string levelToLoad;
		public static Recording recording, newRecording;
		public static bool playingAgainstGhosts = false;

		public static void Load(string name) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		}

	}

	public enum GameType { SINGLE, MULTI, REWATCH }
}