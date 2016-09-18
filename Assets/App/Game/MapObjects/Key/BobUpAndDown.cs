using UnityEngine;
using System.Collections;

public class BobUpAndDown : MonoBehaviour {

	public float bobSpeed = 0.25f;
	public float bobOffset = 0.3f;

	float originY;
	int bobDir;

	void Start () {
		originY = transform.position.y;
		bobDir = 1;
	}
	
	void Update () {
		if (transform.position.y > originY + bobOffset) {
			bobDir = -1;
		}
		if (transform.position.y < originY) {
			bobDir = 1;
		}
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y + bobDir * Time.deltaTime * bobSpeed,
			transform.position.z
		);
	}

}
