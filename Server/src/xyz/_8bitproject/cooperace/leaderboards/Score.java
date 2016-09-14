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
	
	/** Create a new score
	 * @param time Time component (in tenths-of-a-second)
	 * @param player1 Name of player 1 (a three-letter string)
	 * @param player2 Name of player 2 (a three-letter string)
	 * @throws InvalidScoreException if a part of the score is
	 * not valid
	 */
	public Score(int time, String player1, String player2)
									throws InvalidScoreException {
		
		this.time = validateTime(time); 
		
		this.player1 = validateName(player1);
		this.player2 = validateName(player2);
	}
	
	/** Helper function to validate time inputs 
	 * @param time The time to validate
	 * @return The validated time
	 * @throws InvalidScoreException if the time is invalid (<= 0)
	 */
	private int validateTime(int time) throws InvalidScoreException {
		if(time > 0){
			return time;
		} else {
			 // TODO: Add more validation rules (e.g. lower bounds on time)
			throw new InvalidScoreException("Invalid time: " + time);
		}
	}
	
	/** Helper function to validate name inputs 
	 * @param player The name to validate
	 * @return The validated name
	 * @throws InvalidScoreException if the name is invalid
	 * (longer than 3 chars)
	 */
	private String validateName(String player) {
		if(player != null && player.length() == 3) {
			return player;
		} else {
			throw new InvalidScoreException("Invalid player name length: "
					+ player.length());
		}
	}

	@Override
	public int compareTo(Score that) {
		// compare based on time
		return this.time - that.time;
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