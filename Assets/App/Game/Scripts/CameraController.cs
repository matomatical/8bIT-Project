using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform follow;
	public SpriteRenderer level;

	private float minx, miny, maxx, maxy;

	void Start () {

		Camera camera = GetComponent<Camera> ();

		// helper values

		float camerax = camera.orthographicSize * Screen.width / Screen.height * 2;
		float cameray = camera.orthographicSize * 2;

		float levelx = level.bounds.size.x;
		float levely = level.bounds.size.y;

		float centerx = level.transform.position.x;
		float centery = level.transform.position.x;

		// calculate max/min values

		maxx = centerx + (levelx / 2) - (camerax / 2);
		minx = centerx - (levelx / 2) + (camerax / 2);

		maxy = centery + (levely / 2) - (cameray / 2);
		miny = centery - (levely / 2) + (cameray / 2);
	}

	// Guaranteed to run after all Update processing
	void LateUpdate () {

		// Calculations assume map is position at the origin


		// follow the target object
		transform.position = new Vector3 (
			Mathf.Clamp(follow.position.x, minx, maxx),
			Mathf.Clamp(follow.position.y, miny, maxy),
			transform.position.z
		);
	}
}
