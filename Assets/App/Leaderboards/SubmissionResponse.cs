/**
 * A submission response object.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {

	[System.Serializable]
	public struct SubmissionResponse {

		public int position;

		public static SubmissionResponse FromJson(string json) {
			return JsonUtility.FromJson<SubmissionResponse>(json);
		}
		
	}

}