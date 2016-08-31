using UnityEngine;
using Tiled2Unity;
using System.Collections.Generic;

[CustomTiledImporter]
class CustomImporter : ICustomTiledImporter
{
	GameObject keyPrefab = (GameObject)Resources.Load("Key");
	GameObject keyBlockPrefab = (GameObject)Resources.Load("KeyBlock");
	GameObject pressurePlatePrefab = (GameObject)Resources.Load("PressurePlate");
	GameObject pressurePlateBlockPrefab = (GameObject)Resources.Load("PressurePlateBlock");

    public void HandleCustomProperties(GameObject marker,
        IDictionary<string, string> props)
	{
        if (props.ContainsKey("Key")) {
			replaceMarker(marker, keyPrefab);
		} else if (props.ContainsKey("KeyBlock")) {
			replaceMarker(marker, keyBlockPrefab);
		} else if (props.ContainsKey("PressurePlate")) {
			replaceMarker(marker, pressurePlatePrefab).GetComponent<PressurePlate>().address = props["PressurePlate"];
		} else if (props.ContainsKey("PressurePlateBlock")) {
			replaceMarker(marker, pressurePlateBlockPrefab).GetComponent<PressurePlateBlock>().address = props["PressurePlateBlock"];
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
    	// do nothing
    }

	GameObject replaceMarker(GameObject marker, GameObject prefab) {
		GameObject newObject = GameObject.Instantiate(prefab);
		newObject.transform.parent = marker.transform.parent.transform;
		newObject.transform.position = marker.transform.position;

		GameObject.DestroyImmediate(marker.GetComponent<BoxCollider2D>());
		marker.name = "__ignored__";
		marker.transform.position = Vector3.zero;

		return newObject;
    }
}
