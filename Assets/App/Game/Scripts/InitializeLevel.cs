using UnityEngine;
using System.Collections;

public class InitializeLevel : MonoBehaviour {

	new public GameObject camera;
	public GameObject gui;
	public GameObject level;

	void Start () {
		GameObject objects = GameObject.Find("Objects");

		// finish line should have a reference to the clock
		FinishLine finishLine = objects.GetComponentInChildren<FinishLine>();
		finishLine.clock = gui.GetComponentInChildren<ClockController>();

		// player should have a reference to the joystick
		PlayerController player = objects.GetComponentInChildren<PlayerController>();
		player.joystick = gui.GetComponentInChildren<Joystick>();

		// camera should have a reference to the player
		CameraController cameraController = camera.GetComponent<CameraController>();
		cameraController.follow = player.gameObject.transform;
	}

}
