/**
 * A submission request object.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {

	[System.Serializable]
	public struct SubmissionRequest {

		[System.Serializable]
		public struct Submission {
			public string level;
			public Score score;
			public Submission(string level, Score score) {
				this.level = level;
				this.score = score;
			}
		}

		public string type;
		public Submission submission;

		public SubmissionRequest(string level, Score score) {
			type = "submission";
			submission = new Submission(level, score);
		}

		public static string ToJson(string level, Score score) {
			SubmissionRequest s = new SubmissionRequest(level, score);
			return JsonUtility.ToJson(s, false);
		}
		
	}

}