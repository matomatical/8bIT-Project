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

		private Vector2 levelCenter; // position of the target level's center
		private Vector2 scrollCenter; // position of this level's center

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

			levelCenter = new Vector2(corner.x + levelx/2, corner.y - levely/2);

			Vector2 scrollingCorner = scrollingLevel.transform.position;
			scrollCenter = new Vector2(scrollingCorner.x + scrollx/2, scrollingCorner.y - scrolly/2);

			scalex = (float)((levelx - scrollx) / (levelx - camerax));

			scaley = (float)((levely - scrolly) / (levely - cameray));
		}

		void LateUpdate () {
			transform.position = (Vector3)scrollCenter +
				new Vector3 (
					levelCenter.x + (cam.transform.position.x - levelCenter.x) * scalex,
					levelCenter.y + (cam.transform.position.y - levelCenter.y) * scaley,
					transform.position.z
				);
		}
	}
}
