using UnityEngine;
using Tiled2Unity;
using System.Collections.Generic;

[CustomTiledImporter]
class CustomImporter : ICustomTiledImporter
{
	GameObject keyPrefab = (GameObject)Resources.Load("Key");
	GameObject keyBlockPrefab = (GameObject)Resources.Load("KeyBlock");

    public void HandleCustomProperties(GameObject marker,
        IDictionary<string, string> keyValuePairs)
	{
        if (keyValuePairs.ContainsKey("Key")) {
			replaceMarker(marker, keyPrefab);
		} else if (keyValuePairs.ContainsKey("KeyBlock")) {
			replaceMarker(marker, keyBlockPrefab);
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
		marker.name = "__to_delete__";

		return newObject;
    }
}
