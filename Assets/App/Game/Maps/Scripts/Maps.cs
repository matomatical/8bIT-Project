/*
 * A static class for managing tiled2unity map prefabs.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace _8bITProject.cooperace {
	public class Maps {

		// list of all levels
		public static string[] maps = new string[] {
			"Test_Level_1",
			"Test_Level_2",
			"Test_Level_3"
		};

		public static GameObject Load(string name) {
			return Resources.Load("Maps/" + name) as GameObject;
		}

	}
}
