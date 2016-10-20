/*
 * Scene manager, responsible for loading scenes
 * as well as communication between them.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace {
	
	public static class SceneManager {

		public static GameOpts opts { get; private set; }
		public static GameOuts outs { get; private set; }

		static void Load(string name) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		}

		public static void LoadMainMenu(){
			Load (Magic.Scenes.MAIN_MENU);
		}

		public static void StartReplayGame(string level, Recording recording) {
			SceneManager.opts = new GameOpts (GameType.REWATCH, level, recording);
			Load (Magic.Scenes.GAME_SCENE);
		}

		public static void StartMultiplayerGame(string level){
			SceneManager.opts = new GameOpts (GameType.MULTI, level);
			Load (Magic.Scenes.GAME_SCENE);
		}

		public static void StartPlayagainstGame(string level, Recording recording){
			SceneManager.opts = new GameOpts (GameType.GHOST, level, recording);
			Load (Magic.Scenes.GAME_SCENE);
		}

		public static void ExitGameFinish(Recording recording = null){
			SceneManager.outs = new GameOuts (opts, ExitType.FINISH, recording);
			Load (Magic.Scenes.POSTGAME);
		}

		public static void ExitGameDisconnect(){
			SceneManager.outs = new GameOuts (opts, ExitType.DISCONNECT);
			Load (Magic.Scenes.POSTGAME);
		}

		public static void ExitGameQuit(){
			SceneManager.outs = new GameOuts (opts, ExitType.QUIT);
			Load (Magic.Scenes.MAIN_MENU);
		}
	}

	public enum GameType { MULTI, REWATCH, GHOST }

	public struct GameOpts {
		public readonly GameType type;
		public readonly string level;
		public readonly Recording recording;
		public GameOpts(GameType type, string level, Recording recording = null){
			this.type = type;
			this.level = level;
			this.recording = recording;
		}
	}

	public enum ExitType { FINISH, QUIT, DISCONNECT }

	public struct GameOuts {
		public readonly GameType type;
		public readonly ExitType exit;
		public readonly string level;
		public readonly Recording recording;
		public GameOuts(GameOpts opts, ExitType type, Recording recording = null){
			this.type = opts.type;
			this.exit = type;
			this.level = opts.level;
			this.recording = recording;
		}
	}
}
