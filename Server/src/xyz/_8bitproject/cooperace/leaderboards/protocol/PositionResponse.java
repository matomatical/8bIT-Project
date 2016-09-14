package xyz._8bITProject.cooperace.leaderboards.protocol;

/** Simple data structure containing information requered by client
 *  in response to a score submission. Ready to be converted straight
 *  into JSON!
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
class PositionResponse implements Response {

	/** The final (one-based) position of the player on the scoreboard,
	 *  or 0 if no place */
	public final int position;
	
	/** Create a new response wrapping this position information */
	public PositionResponse(int position) {
		this.position = position;
	}
}