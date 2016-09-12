package xyz._8bitproject.cooperace.leaderboards.protocol;

import xyz._8bitproject.cooperace.leaderboards.Score;

/** Simple data structure containing information requered by client
 *  in response to a score request. Ready to be converted straight
 *  into JSON!
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class LeadersResponse implements Response {

	/** The list of scores to send */
	public final Score[] leaders;
	
	/** Create a new response wrapping this array of leading scores */
	public LeadersResponse(Score[] leaders) {
		this.leaders = leaders;
	}
}