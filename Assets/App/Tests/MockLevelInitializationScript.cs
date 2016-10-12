using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.tests {
	
	public class MockLevelInitializationScript : MonoBehaviour {

		// Use this to toggle all components in scene
		void OnEnable () {
		
			// initialise all of the players local controllers

			LocalPlayerController[] players = FindObjectsOfType<LocalPlayerController> ();

			foreach (LocalPlayerController player in players) {
				player.enabled = true;
			}

			// initialise all of the keys, key blocks, pushblocks and pressure
			// lates to respond to regular inputs

			KeyController[] keys = FindObjectsOfType<KeyController> ();

			foreach (KeyController key in keys) {
				key.enabled = true;
			}


			KeyBlockController[] keyBlocks = FindObjectsOfType<KeyBlockController> ();

			foreach (KeyBlockController block in keyBlocks) {
				block.enabled = true;
			}


			PushBlockController[] pushBlocks = FindObjectsOfType<PushBlockController> ();

			foreach (PushBlockController block in pushBlocks) {
				block.enabled = true;
			}
		

			PressurePlateController[] plates = FindObjectsOfType<PressurePlateController> ();

			foreach (PressurePlateController plate in plates) {
				plate.enabled = true;
			}
		}
	}
}