﻿/*
 * Tiled map importer logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using Tiled2Unity;
using System.Collections.Generic;

namespace _8bITProject.cooperace {
	[CustomTiledImporter]
	class CustomImporter : ICustomTiledImporter {

		// offset to adjust tiled object position for unity objects
		// specifically top-right to center
		Vector3 offset = new Vector3(0.5f, -0.5f, 0);

		// prefabs for initializing game objects
		GameObject playerPrefab = (GameObject)Resources.Load("Player");
		GameObject keyPrefab = (GameObject)Resources.Load("Key");
		GameObject keyBlockPrefab = (GameObject)Resources.Load("KeyBlock");
		GameObject pressurePlatePrefab = (GameObject)Resources.Load("PressurePlate");
		GameObject pressurePlateBlockPrefab = (GameObject)Resources.Load("PressurePlateBlock");
		GameObject pushBlockPrefab = (GameObject)Resources.Load("PushBlock");
		GameObject finishLinePrefab = (GameObject)Resources.Load("FinishLine");
		GameObject exitPrefab = (GameObject)Resources.Load("Exit");

		GameObject replaceMarker(GameObject marker, GameObject prefab) {
			// create a new instance of the prefab and add it to the tiled map
			// object
			GameObject newObject = GameObject.Instantiate(prefab);
			newObject.transform.parent = marker.transform.parent.transform;

			newObject.transform.position = offset + marker.transform.position;

			// mark the object created by tiled2unity for deletion
			marker.name = "__to_be_destroyed__";

			return newObject;
		}

		public void HandleCustomProperties(GameObject marker,
				IDictionary<string, string> props) {
			// the tiled map should have objects with specific keys
			// which are then replaced by instances of the prefab
			if (props.ContainsKey("Player")) {
				replaceMarker(marker, playerPrefab);
			} else if (props.ContainsKey("Key")) {
				replaceMarker(marker, keyPrefab);
			} else if (props.ContainsKey("KeyBlock")) {
				replaceMarker(marker, keyBlockPrefab);
			} else if (props.ContainsKey("PressurePlate")) {
				GameObject plate = replaceMarker(marker, pressurePlatePrefab);
				plate.GetComponent<PressurePlate>().address = props["PressurePlate"];
			} else if (props.ContainsKey("PressurePlateBlock")) {
				GameObject block = replaceMarker(marker, pressurePlateBlockPrefab);
				block.GetComponent<PressurePlateBlock>().address = props["PressurePlateBlock"];
			} else if (props.ContainsKey("PushBlock")) {
				replaceMarker(marker, pushBlockPrefab);
			} else if (props.ContainsKey("FinishLine")) {
				replaceMarker(marker, finishLinePrefab);
			} else if (props.ContainsKey("Exit")) {
				replaceMarker(marker, exitPrefab);
			}
		}

		public void CustomizePrefab(GameObject prefab) {
			// delete all objects marked for deletion
			foreach (Transform transform in prefab.GetComponentsInChildren<Transform>()) {
				if (transform.name == "__to_be_destroyed__") {
					GameObject.DestroyImmediate(transform.gameObject);	
				}
			}

			// setup links between pressure plates and blocks
			// first group all plates and blocks by address
			// (what I wouldn't do for haskell right now...)
			PressurePlate[] allPlates =
				prefab.GetComponentsInChildren<PressurePlate>();
			Dictionary<string, List<PressurePlate>> groupedPlates =
				new Dictionary<string, List<PressurePlate>>();
			foreach (PressurePlate plate in allPlates) {
				List<PressurePlate> list;
				if (groupedPlates.ContainsKey(plate.address)) {
					list = groupedPlates[plate.address];
				} else {
					list = new List<PressurePlate>(allPlates.Length);
					groupedPlates.Add(plate.address, list);
				}
				list.Add(plate);
			}

			PressurePlateBlock[] allBlocks =
				prefab.GetComponentsInChildren<PressurePlateBlock>();
			Dictionary<string, List<PressurePlateBlock>> groupedBlocks =
				new Dictionary<string, List<PressurePlateBlock>>();
			foreach (PressurePlateBlock block in allBlocks) {
				List<PressurePlateBlock> list;
				if (groupedBlocks.ContainsKey(block.address)) {
					list = groupedBlocks[block.address];
				} else {
					list = new List<PressurePlateBlock>(allBlocks.Length);
					groupedBlocks.Add(block.address, list);
				}
				list.Add(block);
			}

			// give the appropriate list to each plate and block
			foreach (PressurePlate plate in allPlates) {
				plate.linked = groupedBlocks[plate.address];
			}
			foreach (PressurePlateBlock block in allBlocks) {
				block.linked = groupedPlates[block.address];
			}
		}
	}
}
