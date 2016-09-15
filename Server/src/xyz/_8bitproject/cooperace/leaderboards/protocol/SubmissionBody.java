package xyz._8bITProject.cooperace.leaderboards.protocol;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;
import xyz._8bITProject.cooperace.leaderboards.Score;

/** This command object represents the body of a score submit message,
 *  comprised of a level-name and a score to be submitted to that level.
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
class SubmissionBody {
	
	/** The name of the level for submission */
	public String level;
	/** The score submission to make */
	public Score score;
	
	/** If this request is well-formed, execute it and return our response
	 *  object for the client */
	public Response response() {
		
		// validation
		
		if(level == null){
			throw new MessageException("missing level in submission message");
		
		} else if(score == null){
			throw new MessageException("missing score in submission message");
		
		} else if (score.time <= 0){ // TODO NOTE: actual scores of 0 will be considered invalid 
			throw new MessageException("incomplete score: time field missing or zero");
			
		} else if (score.player1 == null){ // TODO: This checking should reall happen within score?
			throw new MessageException("incomplete score: player1 field missing");
			
		} else if (score.player2 == null) {
			throw new MessageException("incomplete score: player2 field missing");
		}
		
		// build response
		
		Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
		
		int position = lb.rank(score);
		
		if(position > 0){
			// we placed!
			
			// NOTE that this will update the position as well:
			// it's possible that someone got in between us ranking and
			// then recording our score on the leaderboard it's even
			// possible that we now didn't get recorded and then position
			// will be zero from now on (and zero will be sent to the client)
			position = lb.record(score);
		}

		// either way, make a reply
		
		return new PositionResponse(position);
	}
	
	@Override
	public String toString(){
		return this.score + " on level " + this.level;
	}
}