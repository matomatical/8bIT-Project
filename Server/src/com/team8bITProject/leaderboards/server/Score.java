package com.team8bITProject.leaderboards.server;

/** A data structure to represent a single high-score entry
 *  Implements the comparable interface so that scores can
 *  be easily sorted by time (and then by creation order)
 *  Also manages input validation
 * @author Matt Farrugia
 */
public class Score implements Comparable<Score> {
	
	public final int time;
	public final String player1, player2;
	
	public Score(int time, String player1, String player2){
		
		this.time = parseTime(time); 
		
		this.player1 = parseName(player1);
		this.player2 = parseName(player2);
	}
	
	private int parseTime(int time) {
		if(time > 0){
			return time;
		} else {
			throw new InvalidScoreException("Invalid time: " + time);
		}
			
	}
	
	private String parseName(String player) {
		if(player.length() == 3) {
			return player;
		} else {
			throw new InvalidScoreException("Invalid player name length: " + player.length());
		}
	}

	@Override
	public int compareTo(Score that) {
		return this.time - that.time;
	}
	
	@Override
	public String toString() {
		return player1 + " " + player2 + " - " + (time/10) + "." + (time%10);
	}

	public static Score newDefaultScore() {
		return new Score(60000, "---", "---");
	}
}