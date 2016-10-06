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
		public string level;
		
		public ScoresResponse(string level) {
			leaders = null;
			this.level = level;
		}

		public static ScoresResponse FromJson(string json, string level) {
			ScoresResponse sr = JsonUtility.FromJson<ScoresResponse>(json);
			sr.level = level;
			return sr;
		}
		
	}

}