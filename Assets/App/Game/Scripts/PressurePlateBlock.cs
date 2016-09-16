using UnityEngine;
using System.Collections.Generic;

public class PressurePlateBlock : MonoBehaviour {

	public string address;
	public List<PressurePlate> linked;

	public void UpdateStatus() {
		// open if any pressure plate is pressed
		bool isOpen = false;
		foreach (PressurePlate plate in linked) {
			if (plate.IsPressed()) {
				isOpen = true;
				break;
			}
		}

		gameObject.SetActive(!isOpen);
	}

}
