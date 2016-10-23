package xyz._8bITProject.cooperace.leaderboards.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;

/** LeaderboardManagerTest tests the LeaderboardManager unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class LeaderboardsManagerTest {
	
	
	/** 
	 * Testing the singleton pattern: calling 'instance' twice should
	 * give the same instance
	 */
	@Test
	public void singletonInstanceShouldGiveSameInstance(){
		
		// grab instance twice
		
		LeaderboardsManager one = LeaderboardsManager.instance();
		LeaderboardsManager two = LeaderboardsManager.instance();
		
		// should be same reference
		
		Assert.assertTrue(one == two);
	}
	
	
	/** 
	 * Testing the singleton pattern: calling 'instance' should never
	 * give a null instance
	 */
	@Test
	public void singletonInstanceShouldNotGiveNullInstance(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// should not be null reference
		
		Assert.assertNotEquals(null, manager);
	}
	
	/** 
	 * Testing the lazy initialisation of leaderboards: calling getLeaderboard
	 * with a new level name should never return null
	 */
	@Test
	public void getArbitraryLeaderboardShouldNotGiveNullReference(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// grab a new leaderboard
		
		Leaderboard leaderboard = manager.getLeaderboard("arbitrary");
		
		// should be valid reference (never null)
		
		Assert.assertNotEquals(null, leaderboard);
		
	}
	
	/** 
	 * Getting the leaderboard for a level twice by the same name should
	 * always return the exact same leaderboard
	 */
	@Test
	public void getLeaderboardSameNameShouldReturnSameLeaderboard(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// grab leaderboard twice
		
		Leaderboard one = manager.getLeaderboard("test");
		Leaderboard two = manager.getLeaderboard("test");
		
		// should be same reference
		
		Assert.assertTrue(one == two);
	}
	
	/** 
	 * Getting leaderboards of a different name should return two different
	 * leaderboards, not the same one!
	 */
	@Test
	public void getLeaderboardDifferentNameShouldReturnDifferentLeaderboard(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// grab two leaderboards
		
		Leaderboard one = manager.getLeaderboard("testing 1");
		Leaderboard two = manager.getLeaderboard("testing 2");
		
		// should be distinct references
		
		Assert.assertTrue(one != two);
	}
}
