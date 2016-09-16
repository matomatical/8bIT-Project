package xyz._8bITProject.cooperace.leaderboards.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.Score;

public class LeaderboardTest {
	
	@Test
	public void leadersShouldHaveNumleadersElements(){
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score[] leaders = leaderboard.getLeaders();
		
		int leadersLength = leaders.length;
		int numLeaders = leaderboard.numLeaders();
		
		Assert.assertEquals(numLeaders, leadersLength);
	}
	
	@Test
	public void leadersShouldStartWithDefaultScores(){
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score[] leaders = leaderboard.getLeaders();
		
		Score[] scores = new Score[leaderboard.numLeaders()];
		for(int i = 0; i < scores.length; i++){
			scores[i] = Score.newDefaultScore();
		}
		
		Assert.assertArrayEquals(scores, leaders);
	}
	
	@Test
	public void rankOfLowScoreShouldReturnZero(){
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score lastLeader = leaderboard.getLeaders()[0];
		
		Score nonLeader = new Score(lastLeader.time+1, "NEW", "SCR");
		int position = leaderboard.rank(nonLeader);
		
		Assert.assertEquals(0, position);
	}
	
	@Test
	public void rankOfTopScoreShouldReturnOne(){
		Leaderboard leaderboard = new Leaderboard("test");
		Score firstLeader = leaderboard.getLeaders()[0];
		
		Score newLeader = new Score(firstLeader.time-1, "NEW", "SCR");
		int position = leaderboard.rank(newLeader);
		
		Assert.assertEquals(1, position);
	}
	
	@Test
	public void rankOfMediumScoreShouldReturnTwo(){
		Score[] scores = {
				new Score(1, "abc", "123"),
				new Score(3, "abc", "123")
			};		
		Leaderboard leaderboard = new Leaderboard("test", scores); 

		Score sndLeader = new Score(2, "2nd", "top");
		int position = leaderboard.rank(sndLeader);

		Assert.assertEquals(2, position);
	}
	
	@Test
	public void recordOfLowScoreShouldReturnZero(){
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score lastLeader = leaderboard.getLeaders()[0];
		
		Score nonLeader = new Score(lastLeader.time+1, "NEW", "SCR");
		int position = leaderboard.record(nonLeader);
		
		Assert.assertEquals(0, position);
	}
	
	@Test
	public void recordOfTopScoreShouldReturnOne(){
		Leaderboard leaderboard = new Leaderboard("test");
		Score firstLeader = leaderboard.getLeaders()[0];
		
		Score newLeader = new Score(firstLeader.time-1, "NEW", "SCR");
		int position = leaderboard.record(newLeader);
		
		Assert.assertEquals(1, position);
	}
	
	@Test
	public void recordOfMediumScoreShouldReturnTwo(){
		Score[] scores = {
				new Score(1, "abc", "123"),
				new Score(3, "abc", "123")
			};		
		Leaderboard leaderboard = new Leaderboard("test", scores); 

		Score sndLeader = new Score(2, "2nd", "top");
		int position = leaderboard.record(sndLeader);
		
		Assert.assertEquals(2, position);
	}
	
	@Test
	public void recordOfRankingScoreShouldChangeLeaders(){
		Score[] scores = {
				new Score(1, "abc", "123"),
				new Score(3, "abc", "123")
			};		
		Leaderboard leaderboard = new Leaderboard("test", scores);
		Score[] leadersBefore = leaderboard.getLeaders();

		Score sndLeader = new Score(2, "2nd", "top");
		leaderboard.record(sndLeader);
		Score[] leadersAfter = leaderboard.getLeaders();
		
		// assert arrays are not equal
		boolean equals = true;
		for(int i = 0; i < leaderboard.numLeaders(); i++){
			if( ! leadersBefore[i].equals(leadersAfter[i])){
				equals = false;
			}
		}
		Assert.assertTrue( ! equals);
	}
}
