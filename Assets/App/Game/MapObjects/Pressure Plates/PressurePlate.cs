using UnityEngine;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour {

	[HideInInspector]
	public string address;
	public List<PressurePlateBlock> linked;

	int isColliding;

	void UpdateBlocksStatus() {
		foreach (PressurePlateBlock block in linked) {
			block.UpdateStatus();
		}
	}

	void OnTriggerEnter2D() {
		++isColliding;
		UpdateBlocksStatus();
	}

	void OnTriggerExit2D() {
		--isColliding;
		UpdateBlocksStatus();
	}

	public bool IsPressed() {
		return isColliding > 0;
	}

}
