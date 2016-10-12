/*
 * Dynamic level loading logic.
 * Takes a tiled2unity map prefab by name and loads it,
 * then sets up all of the objects inside the level :)
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.Collections;

using UnityEngine;
using Tiled2Unity;

using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;


namespace xyz._8bITProject.cooperace {
	
	public class InitializeLevel : MonoBehaviour {

		public CameraController cam;
		public BackgroundScroller background;
		public GameObject gui;

		private TiledMap level;

		/// Runs before Start()
		/// 
		/// Loads the map prefab for the given name into the scene,
		/// Links the surrounding objects (camera, etc) to the level,
		/// and properly sets up the level's children's components,
		/// depending on what type of level was entered

		void Awake() {

			// load the level!

			GameObject prefab = Maps.Load(SceneManager.levelToLoad);

			if (prefab) {
				level = GameObject.Instantiate<GameObject> (prefab).GetComponent<TiledMap> ();
				cam.level = level;
				background.level = level;
			}

			// what type of game have we entered?

			GameType type = SceneManager.gameType;

			if (type == GameType.SINGLE) {

				SinglePlayerAwake (level);

				// prepare the level's objects for recording

				RecordingAwake (level);


			} else if (type == GameType.MULTI) {
				MultiPlayerAwake (level);

				// prepare the level's objects for recording

				RecordingAwake (level);


			} else if (type == GameType.REWATCH) {

				// prepare the level for 

				RewatchRecordingAwake (level);

			}

			// is there also a level in the background?

			if (SceneManager.playingAgainstGhosts) {

				GameObject ghostPrefab = Maps.Load(SceneManager.levelToLoad);
				TiledMap ghostLevel = null;
				if (prefab) {
					ghostLevel = GameObject.Instantiate<GameObject>(ghostPrefab).GetComponent<TiledMap>();
				}

				// TODO: shrink and position level between background and real level

				// TODO: shade level

				// TODO: make level scroll

				// ghostLevel.gameObject.AddComponent<BackgroundScroller> ();
				// // ...

				// set up level to track a recording

				RewatchRecordingAwake (ghostLevel);
			}
		}

		void SinglePlayerAwake(TiledMap level){

			// player should be controllable by inputs, turn on
			// LocalPlayerController for first available player!

			LocalPlayerController lpc = level.GetComponentInChildren<LocalPlayerController> ();
			lpc.enabled = true;

			// All other players can get orphaned remote physics controllers
			// just so they obey physics / can be recorded

			foreach (LocalPlayerController lpc2 in
				level.GetComponentsInChildren<LocalPlayerController>()){

				if(!lpc2.enabled){
					lpc2.gameObject.GetComponent<RemotePhysicsController>().enabled = true;
				}
			}

			// push blocks should also respond to collisions rather than
			// remote updates

			foreach (PushBlockController pbc in
				level.GetComponentsInChildren<PushBlockController>()){

				pbc.enabled = true;
			}


			// keys, key blocks and pressure plates should respond to
			// collisions, not remote updates!

			foreach (KeyController key in
					level.GetComponentsInChildren<KeyController>()) {

				key.enabled = true;
			}

			foreach (PressurePlateController plate in
					level.GetComponentsInChildren<PressurePlateController>()) {

				plate.enabled = true;
			}

			foreach (KeyBlockController block in
					level.GetComponentsInChildren<KeyBlockController>()) {

				block.enabled = true;
			}


			// camera should have a reference to the player to-be-followed

			cam.target = (ArcadePhysicsController)lpc;

		}


	
		void MultiPlayerAwake(TiledMap level) {
			MultiplayerInit.Init (level.gameObject);
		}

		void RewatchRecordingAwake (TiledMap level) {

			// enable remote physics controllers on dynamic objects,
			// and enable dynamic recorders

			foreach (DynamicReplayer rep in
					level.GetComponentsInChildren<DynamicReplayer>()) {

				// enable remote control

				RemotePhysicsController rpc
					= rep.GetComponent<RemotePhysicsController> ();
				rpc.enabled = true;

				// enable replaying

				rep.enabled = true;
			}

			// enable static replayers, and disable box collider
			// triggering to prevent triggering on collisions

			foreach (StaticReplayer rep in
					level.GetComponentsInChildren<StaticReplayer>()) {

				// enable replaying

				rep.enabled = true;

				// disable collision triggering

				BoxCollider2D box = rep.GetComponent<BoxCollider2D> ();
				box.isTrigger = false;
			}

			// enable the replayer and start the replaying

			ReplayingController rc = FindObjectOfType<ReplayingController> ();
			rc.enabled = true;
			rc.level = level;
			rc.StartReplaying (SceneManager.recording);

		}

		void RecordingAwake (TiledMap level){

			// enable static recorders

			foreach (StaticRecorder rec in
					level.GetComponentsInChildren<StaticRecorder>()) {
				rec.enabled = true;
			}

			// enable dynamic recorders

			foreach (DynamicRecorder rec in
					level.GetComponentsInChildren<DynamicRecorder>()) {
				rec.enabled = true;
			}

			// enable the recorder and start the recording

			RecordingController rc = FindObjectOfType<RecordingController> ();
			rc.enabled = true;
			rc.level = level;
			rc.StartRecording (SceneManager.levelToLoad);
		}

	}
}
