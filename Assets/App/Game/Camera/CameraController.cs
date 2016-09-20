/*
 * Camera controller with smoothing support.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using Tiled2Unity;
using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class CameraController : MonoBehaviour {

		// gameobject to follow around
		public Transform follow;
		// level to lock camera view to
		public TiledMap level;
		// how much smoothing to apply to camera movement
		public float smoothing = 4;

		float minx, miny, maxx, maxy;

		// Initialization code
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

			float nextx = transform.position.x
				+ displacement.x * smoothing * Time.deltaTime;
			float nexty = transform.position.y
				+ displacement.y * smoothing * Time.deltaTime;

			// clamp to screen
			transform.position = new Vector3 (
				Mathf.Clamp(nextx, minx, maxx),
				Mathf.Clamp(nexty, miny, maxy),
				transform.position.z
			);
		}
	}
}
