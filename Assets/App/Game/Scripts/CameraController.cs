using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform follow;

	// Guaranteed to run after all Update processing
	void LateUpdate () {

		// follow the target object
		transform.position = new Vector3 (
			follow.position.x,
			follow.position.y,
			transform.position.z
		);
	}
}
