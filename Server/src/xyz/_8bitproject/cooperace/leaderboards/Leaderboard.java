package xyz._8bITProject.cooperace.leaderboards;

import static com.matomatical.util.Log.log;

import java.util.concurrent.locks.ReentrantReadWriteLock;

/** Stores top scores for a particular level, providing
 *  methods for thread-safe reading and writing of scores
 *  (readers and writers problem)
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class Leaderboard {
	
	/** How many scores does this leaderboard hold at once? */
	private static final int NUMSCORES = 10;
	
	/** The name of the level this leaderboard tracks */
	private String levelName;
	/** The array of leading scores for this level */
	private Score[] leaders;
	
	/** Lock for synchronizing access to leaderboard by multiple submitters
	 *  in a shared-read, exclusive-write manner. See
	 *  {@link java.util.concurrent.locks.ReentrantReadWriteLock}.
	 *  The default ('non-fair') settings are used. */
	private final ReentrantReadWriteLock lock = new ReentrantReadWriteLock();
	
	/** Create a new leaderboard for a level with a given name
	 * @param levelName The name of the level this leaderboard tracks
	 */
	public Leaderboard(String levelName) {
		
		this.levelName = levelName;
		
		// initialise the scoreboard with filler scores
		
		leaders = new Score[NUMSCORES];
		
		for(int i = 0; i < NUMSCORES; i ++){
			leaders[i] = Score.newDefaultScore();
		} 
	}
	
	/** Where would this score rank on the leaderboard?
	 * Requests a read lock on the leaderboard, so may block.
	 * Check this before trying to {@link #record(Score)}, to save a
	 * pointless write lock.
	 * @param score The score to record on the leaderboard
	 * @return the prsopective position of the score on the leaderboard,
	 * or 0 if the score would not place.
	 */
	public int rank(Score score) {
		
		// we want shared access to prevent the scores changing as we read
		lock.readLock().lock();
		
		// look for a place for this score in the list
		for(int i = 0; i < leaders.length; i++){
			if(score.compareTo(leaders[i]) < 0){
				// found a spot! the score at index i > new score
				
				lock.readLock().unlock(); // done reading!
				
				// the player ended up in position i+1 (1-based positions)
				return i+1;
			}
		}

		lock.readLock().unlock(); // done reading!
		
		// player's score did not make the leaderboard
		return 0;
	}
	
	/** Tries to place a score on the leaderboard.
	 * Requests a write lock on the leaderboard, so may block.
	 * Check {@link #rank(Score)} != 0 before trying to this, to save a
	 * pointless write lock.
	 * @param score The score to record on the leaderboard
	 * @return the eventual position of the score on the leaderboard,
	 * or 0 if the score did not place 
	 */
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
				

				lock.writeLock().unlock(); // done writing!
				
				log("a new score has made it onto leaderboard "
						+ levelName + " (" + (i+1) + ": " + score + ")");
				
				return i+1; // positions are 1-based
			}
		}
		
		// we didn't find a place! someone else must have
		// gotten in before us and took the place. Sorry!
		lock.writeLock().unlock(); // done writing!
		return 0;
	}
	
	/** Array of leading scores. Requests a read lock on
	 *  the leaderboard, so may block
	 * @return an array of the scores on the leaderboard
	 */
	public Score[] getLeaders() {
		
		// we don't want the leaderboard to change as we
		// are reading it
		lock.readLock().lock();
		
		// create and return a copy of the score array
		Score[] leaders = new Score[NUMSCORES];
		
		for(int i = 0; i < leaders.length; i++){
			leaders[i] = this.leaders[i];
		}
		
		lock.readLock().unlock(); // done reading!
		return leaders;
	}

	/** How many leaders does this board hold?
	 * @return the number of leaders
	 */
	public int numLeaders() {
		return leaders.length;
	}
}
