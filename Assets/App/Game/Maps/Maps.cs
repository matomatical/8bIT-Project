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
			"Tutorious",
		    "Twoality",
			"Crossy Level"
		};

		// Loads and returns a tiled map prefab object,
		// Levels are identified by name (a string), but passing null will
		// return a random level instead.
		// If no level is found, null is returned instead.
		public static GameObject Load(string name=null) {
			if (name == null) {
				name = maps[Random.Range(0, maps.Length)];
			}
			return Resources.Load("Maps/" + name) as GameObject;
		}

	}
}
