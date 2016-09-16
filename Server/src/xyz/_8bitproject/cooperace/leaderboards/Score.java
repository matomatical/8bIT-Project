package xyz._8bITProject.cooperace.leaderboards;

/** A data structure to represent a single high-score entry
 *  Implements the comparable interface so that scores can
 *  be easily sorted by time (and then by creation order)
 *  Also manages input validation
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class Score implements Comparable<Score> {
	
	/** The time-component of scores, stored in thenths-of-a-second */
	public final int time;
	/** One of the player names associated with this score */
	public final String player1, player2;
	
	/** Create a new score */
	public Score(int time, String p1, String p2){
		this.time = time;
		this.player1 = p1;
		this.player2 = p2;
	}
	
	/** Validate this score
	 * @throws InvalidScoreException if a part of the score is
	 * not valid
	 */
	public void validate() throws InvalidScoreException {
		validateTime(time);
		validateName(player1);
		validateName(player2);
	}
	
	/** Helper function to validate time inputs 
	 * @param time The time to validate
	 * @throws InvalidScoreException if the time is invalid (<= 0)
	 */
	private void validateTime(int time) throws InvalidScoreException {
		if(time <= 0){
			 // TODO: Add more validation rules (e.g. lower bounds on time)
			throw new InvalidScoreException("Invalid time: " + time);
		}
	}
	
	/** Helper function to validate name inputs 
	 * @param name The name to validate
	 * @return The validated name
	 * @throws InvalidScoreException if the name is invalid
	 * (longer than 3 chars)
	 */
	private void validateName(String name) {
		if(name == null || name.length() != 3) {
			throw new InvalidScoreException("Invalid player name: " + name);
		}
	}

	@Override
	public int compareTo(Score that) {
		// compare based on time
		return this.time - that.time;
	}
	
	@Override
	public boolean equals(Object that){
		if(this == that){
			return true;
		} else if(that != null && that.getClass() == this.getClass()) {
			return this.equals((Score)that);
		}
		return false;
	}
	
	/** Helper equals method, just for Scores */
	private boolean equals(Score that){
		return that!=null && this.time == that.time
				&& this.player1.equals(that.player1)
				&& this.player2.equals(that.player2);
	}
	
	@Override
	public String toString() {
		return player1 + " " + player2 + " - " + (time/10) + "." + (time%10);
	}

	/** Method providing sample default scores, for use in empty leaderboards,
	 * for example
	 * @return A new default score object
	 */
	public static Score newDefaultScore() {
		return new Score(60000, "---", "---");
	}
}