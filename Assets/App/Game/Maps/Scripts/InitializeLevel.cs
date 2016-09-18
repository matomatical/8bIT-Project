﻿/*
 * Dynamic level loading logic.
 * Takes a tiled2unity map prefab by name and loads it.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using Tiled2Unity;
using System.Collections;

namespace _8bITProject.cooperace {
	public class InitializeLevel : MonoBehaviour {

		new public CameraController camera;
		public Parallax background;
		public GameObject gui;
		public GameObject level;

		void Awake() {
			GameObject prefab = Maps.Load(SceneManager.levelToLoad);
			if (prefab) {
				level = GameObject.Instantiate<GameObject>(prefab);
				camera.level = level.GetComponent<TiledMap>();
				background.level = level.GetComponent<TiledMap>();
			}
		}

		void Start () {
			// finish line should have a reference to the clock
			FinishLine finishLine = level.GetComponentInChildren<FinishLine>();
			finishLine.clock = gui.GetComponentInChildren<ClockController>();

			// player should have a reference to the joystick
			PlayerController player = level.GetComponentInChildren<PlayerController>();
			player.joystick = gui.GetComponentInChildren<Joystick>();

			// camera should have a reference to the player
			camera.follow = player.gameObject.transform;
		}

	}
}
