/*
 * A wrapper exception for all exceptions when communicating with the
 * leaderboards server.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

namespace xyz._8bITProject.cooperace.leaderboard {
	public class ServerException : System.Exception {
		
		public ServerException(string message) : base(message) {}
		public ServerException(System.Exception e) : base(e.Message) {}

	}

}