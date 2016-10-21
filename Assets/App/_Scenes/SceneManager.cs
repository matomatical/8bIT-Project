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

			// if we're in a networked game, we should leave
			// the room
			MultiPlayerController.Instance.LeaveGame ();

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

		public static void ExitGameFinish(Recording recording = null, bool disconnected = false){
			ExitType exit = disconnected ? ExitType.DISCONNECT : ExitType.FINISH;
			SceneManager.outs = new GameOuts (opts, exit, recording);
			Load (Magic.Scenes.POSTGAME);
		}

		public static void ExitGameDisconnect(){
			SceneManager.outs = new GameOuts (opts, ExitType.DISCONNECT);
			Load (Magic.Scenes.POSTGAME);
		}

		public static void ExitGameQuit(bool disconnected){
			ExitType exit = disconnected ? ExitType.DISCONNECT : ExitType.QUIT;
			SceneManager.outs = new GameOuts (opts, exit);

			// if we're in a networked game, we should leave
			// the room
			MultiPlayerController.Instance.LeaveGame ();

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
		public readonly GameOpts opts;
		public readonly ExitType exit;
		public readonly Recording recording;
		public readonly float time;
		public readonly string name, partner;
		public GameOuts(GameOpts opts, ExitType type, Recording recording = null, float time = -1){
			this.opts = opts;
			this.exit = type;
			this.name = MultiPlayerController.Instance.ourName;
			this.partner = MultiPlayerController.Instance.theirName;
			this.time = time;
			this.recording = recording;
		}
	}
}
