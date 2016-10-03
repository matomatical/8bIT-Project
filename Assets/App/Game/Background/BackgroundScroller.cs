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

			SpriteRenderer background = GetComponent<SpriteRenderer> ();
			float bgx = background.bounds.size.x;
			float bgy = background.bounds.size.y;

			float camerax = cam.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = cam.orthographicSize * 2;

			float levelx = level.GetMapWidthInPixelsScaled ();
			float levely = level.GetMapHeightInPixelsScaled ();

			Vector2 corner = level.transform.position;

			center = new Vector2(corner.x + levelx/2, corner.y - levely/2);

			scalex = (float)((levelx - bgx) / (levelx - camerax));

			scaley = (float)((levely - bgy) / (levely - cameray));
		}

		void LateUpdate () {
			transform.position = new Vector3 (
					center.x + (cam.transform.position.x - center.x) * scalex,
					center.y + (cam.transform.position.y - center.y) * scaley,
					transform.position.z
				);
		}
	}
}
