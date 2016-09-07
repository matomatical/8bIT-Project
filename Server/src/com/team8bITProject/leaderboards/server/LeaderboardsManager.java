package com.team8bITProject.leaderboards.server;

import java.util.HashMap;

/** Singleton collector of leaderboards, providing access by level-name 
 * @author Matt Farrugia
 */
public class LeaderboardsManager {
	
	/** Singleton instance */
	private static LeaderboardsManager instance = null;
	
	/** Access the singleton instance (uses lazy initialisation) */
	public static LeaderboardsManager instance(){
		if(instance == null){
			instance = new LeaderboardsManager();
		}
		return instance;
	}
	
	/** Private singleton constructor */
	private LeaderboardsManager(){
		leaderboards = new HashMap<String, Leaderboard>();
	}
	
	/** collection of leaderboards indexed by level */
	private HashMap<String, Leaderboard> leaderboards;
	
	/** Get the leaderboard corresponding to a particular level name
	 *  This will create the leaderboard if no leaderboard exists for that name */
	public Leaderboard getLeaderboard(String level){
		
		if( ! leaderboards.containsKey(level)){
			leaderboards.put(level, new Leaderboard(level));
		}
		
		return leaderboards.get(level);
	}
	
}
