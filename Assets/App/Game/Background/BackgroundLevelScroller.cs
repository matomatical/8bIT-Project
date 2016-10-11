/*
 * Scrolling logic for a level.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using Tiled2Unity;
using UnityEngine;

namespace xyz._8bITProject.cooperace {
	public class BackgroundLevelScroller : MonoBehaviour {

		public TiledMap level;
		public Camera cam;

		public float shift = 2;

		private Vector2 center; // position of the target level's center
		private Vector2 offset; // position of this level's center

		float scalex, scaley;

		void Start () {

			TiledMap scrollingLevel = GetComponent<TiledMap> ();
			float scrollx = scrollingLevel.GetMapWidthInPixelsScaled ();
			float scrolly = scrollingLevel.GetMapHeightInPixelsScaled ();

			float camerax = cam.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = cam.orthographicSize * 2;

			float levelx = level.GetMapWidthInPixelsScaled ();
			float levely = level.GetMapHeightInPixelsScaled ();

			Vector2 corner = level.transform.position;

			center = new Vector2(corner.x + levelx/2, corner.y - levely/2);

			scalex = (float)((levelx - scrollx) / (levelx - camerax));

			scaley = (float)((levely - scrolly) / (levely - cameray));

			offset = new Vector2(- scrollx/2, scrolly/2);
		}

		void LateUpdate () {
			transform.position = new Vector3 (
					center.x + (cam.transform.position.x - center.x) * scalex + offset.x,
					center.y + (cam.transform.position.y - center.y) * scaley + offset.y + shift,
					transform.position.z
				);
		}
	}
}
