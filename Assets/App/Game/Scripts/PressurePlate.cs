using UnityEngine;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour {

	public string address;

	List<PressurePlateBlock> linked;

	int isColliding;

	void Start () {
		PressurePlateBlock[] allBlocks = transform.parent.GetComponentsInChildren<PressurePlateBlock>();
		linked = new List<PressurePlateBlock>(allBlocks.Length);
		foreach (PressurePlateBlock block in allBlocks) {
			if (block.address == address) {
				linked.Add(block);
			}
		}
	}

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
