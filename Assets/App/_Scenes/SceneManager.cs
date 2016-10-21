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

		/// This struct contains the information required between
		/// scenes for properly setting up a game scene for the
		/// game type we are entering
		public static GameOpts opts { get; private set; }

		/// After a level (in the postgame menu), this struct has
		/// all the information about that level including the
		/// opts that were used to initialize it.
		public static GameOuts outs { get; private set; }

		/// Used internally by SceneManager to transition between scenes
		static void Load(string name) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		}

		/// Get out of the game or postgame menu and back to the main menu
		public static void LoadMainMenu(){

			// if we're in a networked game, we should leave
			// the room
			MultiPlayerController.Instance.LeaveGame ();

			Load (Magic.Scenes.MAIN_MENU);
		}

		/// Start a new replaying of a certain recording
		public static void StartReplayGame(string level, Recording recording) {
			SceneManager.opts = new GameOpts (GameType.REWATCH, level, recording);
			Load (Magic.Scenes.GAME_SCENE);
		}

		/// After matchmaking has succeeded, start a new multiplayer level
		public static void StartMultiplayerGame(string level){
			SceneManager.opts = new GameOpts (GameType.MULTI, level);
			Load (Magic.Scenes.GAME_SCENE);
		}

		/// After matchmaking has succeeded, start a new playagainst level
		public static void StartPlayagainstGame(string level, Recording recording){
			SceneManager.opts = new GameOpts (GameType.GHOST, level, recording);
			Load (Magic.Scenes.GAME_SCENE);
		}

		/// Exit the game after finishing a level (after crossing the
		/// finish line)
		/// Takes a 'disconnected' boolean, which should be true if we
		/// came from the disconnection dialog after losing connection
		/// with another player.
		public static void ExitGameOnFinish(Recording recording = null, bool disconnected = false){
			ExitType exit = disconnected ? ExitType.DISCONNECT : ExitType.FINISH;
			SceneManager.outs = new GameOuts (opts, exit, recording);
			Load (Magic.Scenes.POSTGAME);
		}

		/// Exit the game by quitting (e.g. pressing 'exit' in the pause
		/// menu or disconnect dialog)
		/// Takes a 'disconnected' boolean, which should be true if we
		/// came from the disconnection dialog after losing connection
		/// with another player.
		public static void ExitGameOnQuit(bool disconnected){
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
		public GameOuts(GameOpts opts, ExitType type, Recording recording = null){
			this.opts = opts;
			this.exit = type;
			this.name = MultiPlayerController.Instance.ourName;
			this.partner = MultiPlayerController.Instance.theirName;
			this.time = FinalizeLevel.time;
			this.recording = recording;
		}
	}
}
