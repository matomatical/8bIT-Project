/**
 * A leaderboard scores response object.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {

	[System.Serializable]
	public struct ScoresResponse {

		public Score[] leaders;

		public static ScoresResponse FromJson(string json) {
			return JsonUtility.FromJson<ScoresResponse>(json);
		}
		
	}

}