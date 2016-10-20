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

		public static InitializeLevel instance = null;

		public CameraController cam;
		public BackgroundScroller bg;
		public InGameGUIController gui;

        private TiledMap level;

        /// Runs before Start()
        /// 
        /// Loads the map prefab for the given name into the scene,
        /// Links the surrounding objects (camera, etc) to the level,
        /// and properly sets up the level's children's components,
        /// depending on what type of level was entered

        void Awake() {

			// make this instance statically available

			InitializeLevel.instance = this;

			// clone prefab and load the level into the scene

			GameObject prefab = Maps.Load (SceneManager.opts.level);
			GameObject instance = GameObject.Instantiate<GameObject> (prefab);

			this.level = instance.GetComponent<TiledMap> ();

			// link the scene to the other components in the level

			cam.level = level;
			bg.level = level;


			// what type of game have we entered?

			if (SceneManager.opts.type == GameType.REWATCH) {

				// prepare the level for replaying

				RecordingInit.ReplayingAwake (level.gameObject);

			} else if (SceneManager.opts.type == GameType.MULTI) {
                
				// prepare the level to be recorded

				RecordingInit.RecordingAwake (level.gameObject);

				// also prepare the level for multiplayer play

				MultiplayerInit.MultiplayerAwake (level.gameObject);
            
			} else if (SceneManager.opts.type == GameType.GHOST) {

				// create a new background level and prepare it for ghost replay

				TiledMap ghostLevel = GameObject.Instantiate<GameObject> (prefab).GetComponent<TiledMap> ();

				RecordingInit.PlayagainstAwake (ghostLevel.gameObject, level.gameObject);


				// also prepare the orginal level to be recorded

				RecordingInit.RecordingAwake (level.gameObject);

				// and prepare it for multiplayer play

				MultiplayerInit.MultiplayerAwake (level.gameObject);

			}
		}
    }
}
