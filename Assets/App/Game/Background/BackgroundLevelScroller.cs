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

		public float shift = 0;

		private Vector2 center; // position of the target level's center
		private Vector2 offset; // position of this level's center

		float scalex, scaley;

		void Start () {

			// first of all, lets find the size of the level we are scrolling

			TiledMap scrollingLevel = GetComponent<TiledMap> ();
			float scrollx = scrollingLevel.GetMapWidthInPixelsScaled ();
			float scrolly = scrollingLevel.GetMapHeightInPixelsScaled ();

			// and the size of the camera box

			float camerax = cam.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = cam.orthographicSize * 2;

			// and the size of the level we are locking our scrolling to

			float levelx = level.GetMapWidthInPixelsScaled ();
			float levely = level.GetMapHeightInPixelsScaled ();

			// so the levels have their origin at the top left corner,
			// we need the center for our calculations

			offset = new Vector2(- scrollx/2, scrolly/2);

			// we also need the main level's center

			Vector2 corner = level.transform.position;
			center = new Vector2(corner.x + levelx/2, corner.y - levely/2);

			// finally, what's the scaling factor between camera size
			// and scroll-level size? we need this to calculate where
			// we should place the scroll level based on the camera's
			// position in the main level

			scalex = (float)((levelx - scrollx) / (levelx - camerax));

			scaley = (float)((levely - scrolly) / (levely - cameray));


		}

		void LateUpdate () {

			// now we have to move this scroll level to the right place
			// according to the camera's position within the main level

			// we'll just be scaling the difference vector of the camera from
			// the main level's center,
			// and that will become the difference vector for the scrolling
			// level's center, from the main level's center! awesome.

			transform.position = new Vector3 (
					center.x + (cam.transform.position.x - center.x) * scalex + offset.x,
					center.y + (cam.transform.position.y - center.y) * scaley + offset.y + shift,
					transform.position.z
				);
		}
	}
}
