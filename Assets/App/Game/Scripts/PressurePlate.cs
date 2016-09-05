using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour {

	public string address;

	List<PressurePlateBlock> linked;

	void Start () {
		PressurePlateBlock[] allBlocks = transform.parent.GetComponentsInChildren<PressurePlateBlock>();
		linked = new List<PressurePlateBlock>(allBlocks.Length);
		foreach (PressurePlateBlock block in allBlocks) {
			if (block.address == address) {
				linked.Add(block);
			}
		}
	}

	void OnTriggerEnter2D() {
		Debug.Log("enter plate: " + address + ":" + linked.Count);
		foreach (PressurePlateBlock block in linked) {
			Debug.Log("disable:" + block);
			block.gameObject.SetActive(false);
		}
	}

	void OnTriggerExit2D() {
		Debug.Log("exit plate: " + address + ":" + linked.Count);
		foreach (PressurePlateBlock block in linked) {
			Debug.Log("enable:" + block);
			block.gameObject.SetActive(true);
		}
	}

}
