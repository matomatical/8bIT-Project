using UnityEngine;

public class MockPlayerInput : MonoBehaviour {

	public PlayerController player;
	public bool inputLeftDown = false;
	public bool inputRightDown = false;

	void Update () {
		if (inputLeftDown) {
			player.SetInputLeft();
		} else {
			player.UnsetInputLeft();
		}

		if (inputRightDown) {
			player.SetInputRight();
		} else {
			player.UnsetInputRight();
		}
	}

}
