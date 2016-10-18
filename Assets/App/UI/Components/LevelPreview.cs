/*
 * Helper static class to change the level preview image.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace xyz._8bITProject.cooperace.ui {
	public static class LevelPreview {

		/// Takes a level name and an Image object and loads in the appropriate preview.
		public static void LoadPreview(string levelName, RawImage image) {
			Texture texture = Resources.Load("Maps/Previews/" + levelName) as Texture;
			Color c = image.color;
			if (texture == null) {
				c.a = 0.5f;
				image.color = c;
				image.texture = null;
			} else {
				c.a = 1f;
				image.color = c;
				image.texture = texture;
			}
		}

	}	
}