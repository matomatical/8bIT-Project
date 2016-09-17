using UnityEngine;
using Tiled2Unity;
using System.Collections;

public class InitializeLevel : MonoBehaviour {

	new public CameraController camera;
	public GameObject gui;
	public GameObject level;

	void Awake() {
		GameObject prefab = Maps.Load("Test_Level_3");
		if (prefab) {
			level = GameObject.Instantiate<GameObject>(prefab);
			camera.level = level.GetComponent<TiledMap>();
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
