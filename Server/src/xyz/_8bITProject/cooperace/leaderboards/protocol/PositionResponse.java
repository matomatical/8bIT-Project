package xyz._8bITProject.cooperace.leaderboards.protocol;

/** Simple data structure containing information requered by client
 *  in response to a score submission. Ready to be converted straight
 *  into JSON!
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class PositionResponse implements Response {

	/** The final (one-based) position of the player on the scoreboard,
	 *  or 0 if no place */
	public final int position;
	
	/** Create a new response wrapping this position information
	 * @param position The position to store */
	public PositionResponse(int position) {
		this.position = position;
	}
	
	@Override
	public boolean equals(Object that){
		if(this == that){
			return true;
		} else if(that != null && that.getClass() == this.getClass()) {
			return this.equals((PositionResponse)that);
		}
		return false;
	}
	
	/** Helper equals method, just for PositionResponses */
	private boolean equals(PositionResponse that){
		return that!=null && this.position == that.position;
	}
	
	@Override
	public String toString(){
		return "position: " + position;
	}
}