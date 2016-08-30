using UnityEngine;
using System.Collections;

public class BasicCameraScript : MonoBehaviour {

	public Transform follow;
	
	void LateUpdate () {
		transform.position = new Vector3(
			follow.position.x, follow.position.y, transform.position.z
		);
	}

}
