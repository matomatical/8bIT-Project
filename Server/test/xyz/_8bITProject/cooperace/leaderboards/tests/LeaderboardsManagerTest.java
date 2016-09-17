package xyz._8bITProject.cooperace.leaderboards.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;

public class LeaderboardsManagerTest {
	
	@Test
	public void singletonInstanceShouldGiveSameInstance(){
		
		// grab instance twice
		
		LeaderboardsManager one = LeaderboardsManager.instance();
		LeaderboardsManager two = LeaderboardsManager.instance();
		
		// should be same reference
		
		Assert.assertTrue(one == two);
	}
	
	@Test
	public void singletonInstanceShouldNotGiveNullInstance(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// should not be null reference
		
		Assert.assertNotEquals(null, manager);
	}
	
	// Testing lazy initialisation of leaderboards
	@Test
	public void getArbitraryLeaderboardShouldNotGiveNullReference(){
		
		// grab instance
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		
		// grab a new leaderboard
		
		Leaderboard leaderboard = manager.getLeaderboard("arbitrary");
		
		// should be valid reference (never null)
		
		Assert.assertNotEquals(null, leaderboard);
		
	}
	
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
