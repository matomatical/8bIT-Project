/*
 * Scrolling logic for background image.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using Tiled2Unity;
using UnityEngine;

namespace xyz._8bITProject.cooperace {
	public class BackgroundScroller : MonoBehaviour {

		public TiledMap level;
		public Camera cam;

		private Vector2 center; // position of the level's center

		float scalex, scaley;

		void Start () {

			// first of all, lets find the size of the background image
			// we are scrolling

			SpriteRenderer background = GetComponent<SpriteRenderer> ();
			float bgx = background.bounds.size.x;
			float bgy = background.bounds.size.y;

			// and the size of the camera box

			float camerax = cam.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = cam.orthographicSize * 2;

			// and the size of the level we are locking our scrolling to

			float levelx = level.GetMapWidthInPixelsScaled ();
			float levely = level.GetMapHeightInPixelsScaled ();

			// so the levels have their origin at the top left corner,
			// we need the center for our calculations

			Vector2 corner = level.transform.position;

			center = new Vector2(corner.x + levelx/2, corner.y - levely/2);

			// finally, what's the scaling factor between camera size
			// and background image size? we need this to calculate where
			// we should place the background image based on the camera's 
			// position in the main level

			scalex = (float)((levelx - bgx) / (levelx - camerax));

			scaley = (float)((levely - bgy) / (levely - cameray));
		}

		void LateUpdate () {
			// now we have to move this background image to the right place
			// according to the camera's position within the main level

			// we'll just be scaling the difference vector of the camera from
			// the main level's center,
			// and that will become the difference vector for the background
			// image from the main level's center! awesome.

			transform.position = new Vector3 (
					center.x + (cam.transform.position.x - center.x) * scalex,
					center.y + (cam.transform.position.y - center.y) * scaley,
					transform.position.z
				);
		}
	}
}
