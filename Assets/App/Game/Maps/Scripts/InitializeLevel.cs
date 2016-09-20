/*
 * Dynamic level loading logic.
 * Takes a tiled2unity map prefab by name and loads it.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using Tiled2Unity;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class InitializeLevel : MonoBehaviour {

		new public CameraController camera;
		public Parallax background;
		public GameObject gui;
		public GameObject level;

		// Runs before Start()
		// Loads the map prefab for the given name into the scene.
		// The map is passed via the SceneManager class.
		void Awake() {
			GameObject prefab = Maps.Load(SceneManager.levelToLoad);
			if (prefab) {
				level = GameObject.Instantiate<GameObject>(prefab);
				camera.level = level.GetComponent<TiledMap>();
				background.level = level.GetComponent<TiledMap>();
			}
		}

		// Run after Awake()
		// Responsible for setting up all the new connections between
		// pre-existing objects in the scene and the new objects in the level
		// prefab.
		void Start () {
			// finish line should have a reference to the clock
			FinishLine finishLine = level.GetComponentInChildren<FinishLine>();
			finishLine.clock = gui.GetComponentInChildren<ClockController>();

			// player should have a reference to the joystick
			InputManager inputManager = level.GetComponentInChildren<InputManager> ();
			inputManager.joystick = gui.GetComponentInChildren<Joystick> ();
			inputManager.jumpButton = gui.GetComponentInChildren<Button> ();

			// camera should have a reference to the player
			PlayerController player = level.GetComponentInChildren<PlayerController> ();
			camera.follow = player.gameObject.transform;
		}

	}
}
