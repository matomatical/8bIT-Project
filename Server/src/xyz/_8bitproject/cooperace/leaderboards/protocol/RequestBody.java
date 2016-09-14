package xyz._8bITProject.cooperace.leaderboards.protocol;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;

/** This command object represents the body of a score request message,
 *  which is comprised of a level name (for which the leading scores
 *  are sought).
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
class RequestBody {
	
	/** The name of the level for which scores are sought in this request */
	public String level;

	/** If this request is well-formed, execute it and return our response
	 *  object for the client */
	public Response response() {
		
		// validation
		
		if(level == null){
			throw new MessageException("missing level in request message");
		}
		
		// build response object
		
		Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
		
		return new LeadersResponse(lb.getLeaders());
	}
	
	@Override
	public String toString(){
		return "level " + this.level;
	}
}
