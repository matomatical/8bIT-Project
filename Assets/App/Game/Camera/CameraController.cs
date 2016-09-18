using Tiled2Unity;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform follow;	// gameobject to follow around
	public TiledMap level; 		// level to lock camera view to

	public float smoothing = 4;

	private float minx, miny, maxx, maxy;

	void Start () {

		Camera camera = GetComponent<Camera> ();

		// helper values

		float camerax = camera.orthographicSize * Screen.width / Screen.height * 2;
		float cameray = camera.orthographicSize * 2;

		float levelx = level.GetMapWidthInPixelsScaled();
		float levely = level.GetMapHeightInPixelsScaled();

		float cornerx = level.transform.position.x;
		float cornery = level.transform.position.y;

		// calculate max/min values

		maxx = cornerx + levelx - (camerax / 2);
		minx = cornerx + (camerax / 2);

		maxy = cornery - (cameray / 2);
		miny = cornery - levely + (cameray / 2);
	}


	// Guaranteed to run after all Update processing
	void FixedUpdate () {

		// head towards target object

		Vector3 displacement = follow.position - transform.position;

		float nextx = transform.position.x + displacement.x * smoothing * Time.deltaTime;
		float nexty = transform.position.y + displacement.y * smoothing * Time.deltaTime;

		// clamp to screen

		transform.position = new Vector3 (
			Mathf.Clamp(nextx, minx, maxx),
			Mathf.Clamp(nexty, miny, maxy),
			transform.position.z
		);
	}
}
