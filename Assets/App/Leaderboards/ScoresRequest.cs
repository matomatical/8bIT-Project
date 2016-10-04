/**
 * A leaderboard scores request object.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {

	[System.Serializable]
	public struct ScoresRequest {

		[System.Serializable]
		public struct Request {
			public string level;
			public Request(string level) {
				this.level = level;
			}
		}

		public string type;
		public Request request;

		public ScoresRequest(string level) {
			type = "request";
			request = new Request(level);
		}

		public static string ToJson(string level) {
			ScoresRequest r = new ScoresRequest(level);
			return JsonUtility.ToJson(r, false);
		}
		
	}

}