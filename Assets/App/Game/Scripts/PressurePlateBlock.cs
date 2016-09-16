using UnityEngine;
using System.Collections.Generic;

public class PressurePlateBlock : MonoBehaviour {

	public string address;

	List<PressurePlate> linked;

	void Start () {
		PressurePlate[] allBlocks = transform.parent.GetComponentsInChildren<PressurePlate>();
		linked = new List<PressurePlate>(allBlocks.Length);
		foreach (PressurePlate plate in allBlocks) {
			if (plate.address == address) {
				linked.Add(plate);
			}
		}
	}

	public void UpdateStatus() {
		bool isOpen = false;

		// open if any pressure plate is pressed
		foreach (PressurePlate plate in linked) {
			isOpen = isOpen || plate.IsPressed();
		}

		gameObject.SetActive(!isOpen);
	}

}
