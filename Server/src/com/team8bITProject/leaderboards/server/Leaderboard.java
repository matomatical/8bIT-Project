package com.team8bITProject.leaderboards.server;

import java.util.concurrent.locks.ReentrantReadWriteLock;

/** Stores top scores for a particular level, providing
 *  methods for thread-safe reading and writing of scores
 *  (readers and writers problem)
 * @author Matt Farrugia
 */
public class Leaderboard {

	static final int NUMSCORES = 10;
	
	String levelName;
	Score[] leaders;
	
	final ReentrantReadWriteLock lock = new ReentrantReadWriteLock();
	
	public Leaderboard(String levelName) {
		this.levelName = levelName;
		
		leaders = new Score[NUMSCORES];
		
		for(int i = 0; i < NUMSCORES; i ++){
			leaders[i] = Score.newDefaultScore();
		} 
	}
	
	public int rank(Score score) {
		
		lock.readLock().lock();
		
		// look for a place for this score in the list
		for(int i = 0; i < leaders.length; i++){
			if(score.compareTo(leaders[i]) < 0){
				// found a spot! the score at index i > new score
				
				lock.readLock();
				return i+1; // the player ended up in position i+1 (1-based positions)
			}
		}

		// player's score did not make the leaderboard
		lock.readLock().unlock();
		return 0;
	}
	
	public int record(Score score) {
		
		// possible that someone else inserted beforehand,
		// either way we need exclusive access for this step
		lock.writeLock().lock();
		
		// look for a place for this score in the list
		for(int i = 0; i < leaders.length; i++){
			if(score.compareTo(leaders[i]) < 0){
				// this is the spot!
				
				// move the remaining scores down and insert 
				for(int j = leaders.length-1; j > i; j--){
					leaders[j] = leaders[j-1];
				}
				leaders[i] = score;
				
				// done!
				lock.writeLock().unlock();
				return i+1;
			}
		}
		
		// we didn't find a place! someone else must have
		// gotten in before us and took the place. Sorry!
		lock.writeLock().unlock();
		return 0;
	}
	
	public Score[] getLeaders() {
		
		lock.readLock().lock();
		
		Score[] leaders = new Score[NUMSCORES];
		
		for(int i = 0; i < leaders.length; i++){
			leaders[i] = this.leaders[i];
		}
		
		lock.readLock().unlock();
		return leaders;
	}

	public int numLeaders() {
		return leaders.length;
	}
}
