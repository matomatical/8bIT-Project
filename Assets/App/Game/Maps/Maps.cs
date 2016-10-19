/*
 * A static class for managing tiled2unity map prefabs.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace {
	public static class Maps {

		// list of all levels
		public static string[] maps = new string[] {
//			"Random",
			"Tutorious",
			"Twoality",
			"Crossy Level"
		};

		// Loads and returns a tiled map prefab object,
		// Levels are identified by name (a string), but passing null will
		// return a random level instead.
		// If no level is found, null is returned instead.
		public static GameObject Load(string name=null) {
			if (name == null || name.Equals("Random")) {
				name = maps[Random.Range(0, maps.Length-1)];
			}
			return Resources.Load("Maps/" + name) as GameObject;
		}

		public static int GetIndex(string levelName) {
			for (int i = 0; i < maps.Length; i++) {
				if (levelName.Equals(maps[i])) {
					return i;
				}
			}
			return -1;
		}

	}
}
