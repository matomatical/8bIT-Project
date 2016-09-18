/*
 * Parallax scrolling logic for background image.
 *
 * Matt Farrugiam <farrugiam@student.unimelb.edu.au>
 *
 */

using Tiled2Unity;
using UnityEngine;
using System.Collections;

namespace _8bITProject.cooperace {
	public class Parallax : MonoBehaviour {

		public TiledMap level;
		public Camera camera;

		float scalex, scaley;

		void Start () {
			SpriteRenderer background = GetComponent<SpriteRenderer> ();
			float bgx = background.bounds.size.x;
			float bgy = background.bounds.size.y;

			float levelx = level.GetMapWidthInPixelsScaled();
			float levely = level.GetMapHeightInPixelsScaled();

			float camerax = camera.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = camera.orthographicSize * 2;

			// make this work for centered levels
			if (levelx == camerax) {
				scalex = 1;
			} else {
				scalex = (float)((levelx - bgx) / (levelx - camerax));
			}

			if (levely == cameray) {
				scaley = 1;
			} else {
				scaley = (float)((levely - bgy) / (levely - cameray));
			}
		}

		void LateUpdate () {
			transform.position = new Vector3 (
				camera.transform.position.x * scalex,
				camera.transform.position.y * scaley,
				transform.position.z
			);
		}
	}
}
