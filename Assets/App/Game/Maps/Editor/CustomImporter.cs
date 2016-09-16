using UnityEngine;
using Tiled2Unity;
using System.Collections.Generic;

[CustomTiledImporter]
class CustomImporter : ICustomTiledImporter {

	Vector3 offset = new Vector3(0.5f, -0.5f, 0);

	GameObject playerPrefab = (GameObject)Resources.Load("Player");
	GameObject keyPrefab = (GameObject)Resources.Load("Key");
	GameObject keyBlockPrefab = (GameObject)Resources.Load("KeyBlock");
	GameObject pressurePlatePrefab = (GameObject)Resources.Load("PressurePlate");
	GameObject pressurePlateBlockPrefab = (GameObject)Resources.Load("PressurePlateBlock");
	GameObject pushBlockPrefab = (GameObject)Resources.Load("PushBlock");
	GameObject finishLinePrefab = (GameObject)Resources.Load("FinishLine");
	GameObject exitPrefab = (GameObject)Resources.Load("Exit");

    public void HandleCustomProperties(GameObject marker,
			IDictionary<string, string> props) {
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
    	// do nothing
    }

	GameObject replaceMarker(GameObject marker, GameObject prefab) {
		GameObject newObject = GameObject.Instantiate(prefab);
		newObject.transform.parent = marker.transform.parent.transform;

		newObject.transform.position = offset + marker.transform.position;

		GameObject.DestroyImmediate(marker.GetComponent<BoxCollider2D>());
		marker.name = "__ignored__";
		marker.transform.position = Vector3.zero;

		return newObject;
    }
}