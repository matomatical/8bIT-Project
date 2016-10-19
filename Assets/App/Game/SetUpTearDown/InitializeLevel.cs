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
using xyz._8bITProject.cooperace.ui;

namespace xyz._8bITProject.cooperace {

    public class InitializeLevel : MonoBehaviour {

        public CameraController cam;
        public BackgroundScroller background;
        public GameObject gui;

        private TiledMap level;
        private string mapPicked;

        /// Runs before Start()
        /// 
        /// Loads the map prefab for the given name into the scene,
        /// Links the surrounding objects (camera, etc) to the level,
        /// and properly sets up the level's children's components,
        /// depending on what type of level was entered

        void Awake() {
			// clone and load the level!

//            mapPicked = Maps.maps[LevelSelectMenuController.currentLevelIndex_];
//            SceneManager.levelToLoad = mapPicked;
            GameObject prefab = Maps.Load(SceneManager.levelToLoad);

			level = GameObject.Instantiate<GameObject> (prefab).GetComponent<TiledMap> ();
			cam.level = level;
			background.level = level;

			// is there a game in the background?

			GhostLevelAwake (level, prefab);

            // what type of game have we entered?

            GameType type = SceneManager.gameType;

            if (type == GameType.SINGLE) {

                SinglePlayerAwake(level);

                // prepare the level's objects for recording

                RecordingAwake(level);


            }
            else if (type == GameType.MULTI) {
                MultiPlayerAwake(level);

                // prepare the level's objects for recording

                RecordingAwake(level);


            }
            else if (type == GameType.REWATCH) {

                // prepare the level for 

                RewatchRecordingAwake(level);

            }
        }

		void GhostLevelAwake(TiledMap level, GameObject prefab) {

			// is there also a level in the background?

			if (SceneManager.playingAgainstGhosts) {

				// create replay level

				TiledMap ghostLevel = GameObject.Instantiate<GameObject>(prefab).GetComponent<TiledMap>();

				// shrink level behind real level

				float scale = 0.8f;

				ghostLevel.transform.localScale = new Vector3 (
					ghostLevel.transform.localScale.x * scale,
					ghostLevel.transform.localScale.y * scale,
					ghostLevel.transform.localScale.z
				);

				// shade and order level

				float color = 0.6f;

				Renderer[] renderers = ghostLevel.GetComponentsInChildren<Renderer> ();
				foreach (Renderer renderer in renderers) {
					renderer.sortingLayerName = "Before Background";
					renderer.material.color = new Color (color, color, color);
				}

				// make ghost level scroll along with camera and real level

				BackgroundLevelScroller scroller = ghostLevel.gameObject.AddComponent<BackgroundLevelScroller> ();

				scroller.cam = FindObjectOfType<Camera>();
				scroller.level = level;

				// arrange objects by z depth

				level.transform.position 	+= 2*Vector3.forward;
				ghostLevel.transform.position += Vector3.forward;


				// set up level to track a recording

				RewatchRecordingAwakeGhost(ghostLevel);
			}
		}

		void RewatchRecordingAwakeGhost(TiledMap level) {

			// enable remote physics controllers on dynamic objects,
			// and enable dynamic recorders

			foreach (DynamicReplayer rep in
				level.GetComponentsInChildren<DynamicReplayer>()) {

				// enable remote control

				LerpingPhysicsController lpc
				= rep.GetComponent<LerpingPhysicsController>();
				lpc.enabled = true;

				// enable replaying

				rep.enabled = true;
			}

			// enable static replayers, and disable box collider
			// triggering to prevent triggering on collisions

			foreach (StaticReplayer rep in
				level.GetComponentsInChildren<StaticReplayer>()) {

				// enable replaying

				rep.enabled = true;

			}

			// enable the replayer and start the replaying

			ReplayingController rc = FindObjectOfType<ReplayingController>();
			rc.enabled = true;
			rc.level = level;
			rc.StartReplaying(SceneManager.recording);


			// disable the finish lines and stuff

			foreach (FinishLine finish in level.GetComponentsInChildren<FinishLine>()) {
				finish.enabled = false;
			}

			foreach (LevelBoundary exit in level.GetComponentsInChildren<LevelBoundary>()) {
				exit.enabled = false;
			}
		}

        void SinglePlayerAwake(TiledMap level) {

            // player should be controllable by inputs, turn on
            // LocalPlayerController for first available player!

            LocalPlayerController lpc = level.GetComponentInChildren<LocalPlayerController>();
            lpc.enabled = true;

            // All other players can get orphaned remote physics controllers
            // just so they obey physics / can be recorded

            foreach (LocalPlayerController lpc2 in
                level.GetComponentsInChildren<LocalPlayerController>()) {

                if (!lpc2.enabled) {
                    lpc2.gameObject.GetComponent<RemotePhysicsController>().enabled = true;
                }
            }

            // push blocks should also respond to collisions rather than
            // remote updates

            foreach (PushBlockController pbc in
                level.GetComponentsInChildren<PushBlockController>()) {

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
            MultiplayerInit.Init(level.gameObject);
        }

        void RewatchRecordingAwake(TiledMap level) {

            // enable remote physics controllers on dynamic objects,
            // and enable dynamic recorders

            foreach (DynamicReplayer rep in
                    level.GetComponentsInChildren<DynamicReplayer>()) {

                // enable remote control

				LerpingPhysicsController lpc
                    = rep.GetComponent<LerpingPhysicsController>();
                lpc.enabled = true;

                // enable replaying

                rep.enabled = true;

				// camera should follow one of these guys
				cam.target = (ArcadePhysicsController)lpc;
            }

            // enable static replayers, and disable box collider
            // triggering to prevent triggering on collisions

            foreach (StaticReplayer rep in
                    level.GetComponentsInChildren<StaticReplayer>()) {

                // enable replaying

                rep.enabled = true;

            }

            // enable the replayer and start the replaying

            ReplayingController rc = FindObjectOfType<ReplayingController>();
            rc.enabled = true;
            rc.level = level;
            rc.StartReplaying(SceneManager.recording);

        }

        void RecordingAwake(TiledMap level) {

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

            RecordingController rc = FindObjectOfType<RecordingController>();
            rc.enabled = true;
            rc.level = level;
            rc.StartRecording(SceneManager.levelToLoad = mapPicked);


			// make the finish line finish the recording

			foreach (RecordingFinalizer rf in
				level.GetComponentsInChildren<RecordingFinalizer>()) {
				rf.enabled = true;
			}
        }


    }
}
