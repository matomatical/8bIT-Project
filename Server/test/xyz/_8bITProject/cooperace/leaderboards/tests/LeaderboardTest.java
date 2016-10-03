package xyz._8bITProject.cooperace.leaderboards.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.Score;

/** LeaderboardTest tests the Leaderboard unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class LeaderboardTest {
	
	@Test
	public void leadersShouldHaveNumLeadersElements(){
		
		// new default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score[] leaders = leaderboard.getLeaders();
		
		// test lengths
		
		int leadersLength = leaders.length;
		int numLeaders = leaderboard.numLeaders();
		
		// should be equal
		
		Assert.assertEquals(numLeaders, leadersLength);
	}
	
	@Test
	public void leadersShouldStartWithDefaultScores(){
		
		// new default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		
		// get leaders
		
		Score[] leaders = leaderboard.getLeaders();
		
		// should be array of defaults 
		
		Score[] scores = new Score[leaderboard.numLeaders()];
		for(int i = 0; i < scores.length; i++){
			scores[i] = Score.newDefaultScore();
		}
		
		Assert.assertArrayEquals(scores, leaders);
	}
	
	@Test
	public void rankOfLowScoreShouldReturnZero(){
		
		// new default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		
		// rank new lower score
		
		int n = leaderboard.numLeaders();
		Score lastLeader = leaderboard.getLeaders()[n-1];
		Score nonLeader = new Score(lastLeader.time+1, "NEW", "SCR");
		int position = leaderboard.rank(nonLeader);
		
		// should return 0 (indicating wouldn't rank)
		
		Assert.assertEquals(0, position);
	}
	
	@Test
	public void rankOfTopScoreShouldReturnOne(){
		
		// new default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		
		// rank new high score
		
		Score firstLeader = leaderboard.getLeaders()[0];
		Score newLeader = new Score(firstLeader.time-1, "NEW", "SCR");
		int position = leaderboard.rank(newLeader);
		
		// should rank 1st
		
		Assert.assertEquals(1, position);
	}
	
	@Test
	public void rankOfMediumScoreShouldReturnTwo(){
		
		// new partly-custom leaderboard
		
		Score[] scores = {
				new Score(1, "abc", "123"),
				new Score(3, "abc", "123")
			};
		Leaderboard leaderboard = new Leaderboard("test", scores); 

		// rank second-beter score
		
		Score sndLeader = new Score(2, "2nd", "top");
		int position = leaderboard.rank(sndLeader);

		// should rank second
		
		Assert.assertEquals(2, position);
	}
	
	@Test
	public void recordOfLowScoreShouldReturnZero(){
		
		// default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		
		// record new score that is slower than slowest score
		
		int n = leaderboard.numLeaders();
		Score lastLeader = leaderboard.getLeaders()[n-1];
		Score nonLeader = new Score(lastLeader.time+1, "NEW", "SCR");
		int position = leaderboard.record(nonLeader);
		
		// should return 0 (not placed)
		
		Assert.assertEquals(0, position);
	}
	
	@Test
	public void recordOfTopScoreShouldReturnOne(){
		
		// default leaderboard
		
		Leaderboard leaderboard = new Leaderboard("test");
		
		// record new leading score
		
		Score firstLeader = leaderboard.getLeaders()[0];
		Score newLeader = new Score(firstLeader.time-1, "NEW", "SCR");
		int position = leaderboard.record(newLeader);
		
		// should end up in position 1
		
		Assert.assertEquals(1, position);
	}
	
	@Test
	public void recordOfMediumScoreShouldReturnTwo(){
	
		// set up new leaderbaord
		
		Score[] scores = {
				new Score(1, "abc", "123"),
				new Score(3, "abc", "123")
			};		
		Leaderboard leaderboard = new Leaderboard("test", scores); 

		// record a new leader
		
		Score sndLeader = new Score(2, "2nd", "top");
		int position = leaderboard.record(sndLeader);
		
		// should go in position 2
		
		Assert.assertEquals(2, position);
	}
	

	@Test
	public void recordOfLowScoreShouldNotChangeLeaders(){
		
		// default leaderboard and initial leaders
		
		Leaderboard leaderboard = new Leaderboard("test");
		Score[] leadersBefore = leaderboard.getLeaders();
		
		// record new score that is slower than slowest score
		
		int n = leaderboard.numLeaders();
		Score lastLeader = leaderboard.getLeaders()[n-1];
		Score nonLeader = new Score(lastLeader.time+1, "NEW", "SCR");
		leaderboard.record(nonLeader);
		Score[] leadersAfter = leaderboard.getLeaders();
		
		// arrays before and after should be same
		
		Assert.assertArrayEquals(leadersBefore, leadersAfter);
	}
	
	@Test
	public void recordOfTopScoreShouldPushLeaderOnTop(){
		
		// a leaderboard, and get initial leaders
		Score[] scores = {
				new Score(2, "old", "1st"),
				new Score(3, "old", "2nd"),
				new Score(4, "old", "3rd"),
				new Score(5, "old", "4th"),
				new Score(6, "old", "5th"),
				new Score(7, "old", "6th"),
				new Score(8, "old", "7th"),
				new Score(9, "old", "8th"),
				new Score(10, "old", "9th"),
				new Score(11, "old", "0th"),
			};
		
		Leaderboard leaderboard = new Leaderboard("test", scores);
		Score[] leadersBefore = leaderboard.getLeaders();
		
		// record new leading score
		
		Score newLeader = new Score(1, "new", "1st");
		leaderboard.record(newLeader);
		Score[] leadersAfter = leaderboard.getLeaders();
		
		// should end up in position 1, with all the others shifted
		// down by one
		
		Assert.assertEquals(newLeader, leadersAfter[0]);
		
		for(int i = 2; i < leaderboard.numLeaders(); i++){
			Assert.assertEquals(leadersBefore[i-1], leadersAfter[i]);
		}
	}
	
	@Test
	public void recordOfMediumScoreShouldPushScoreInMiddle(){
	
		// a leaderboard, and get initial leaders
		Score[] scores = {
				new Score(1, "old", "1st"),
				new Score(2, "old", "2nd"),
				new Score(3, "old", "3rd"),
				new Score(5, "old", "4th"),
				new Score(6, "old", "5th"),
				new Score(7, "old", "6th"),
				new Score(8, "old", "7th"),
				new Score(9, "old", "8th"),
				new Score(10, "old", "9th"),
				new Score(11, "old", "0th"),
			};
		
		Leaderboard leaderboard = new Leaderboard("test", scores);
		Score[] leadersBefore = leaderboard.getLeaders();
		
		// record new leading score
		
		Score newLeader = new Score(4, "new", "4th");
		leaderboard.record(newLeader);
		Score[] leadersAfter = leaderboard.getLeaders();
		
		// should end up in position 4, with all the others shifted
		// down by one
		
		for(int i = 0; i < 3; i++){
			Assert.assertEquals(leadersBefore[i], leadersAfter[i]);
		}
		
		Assert.assertEquals(newLeader, leadersAfter[3]);
		
		for(int i = 5; i < leaderboard.numLeaders(); i++){
			Assert.assertEquals(leadersBefore[i-1], leadersAfter[i]);
		}
	}
	
	public void createWithNullArrayShouldRecover(){
		
		// new leaderboard with null
		
		Leaderboard leaderboard = new Leaderboard("test", null);
		
		// get leaders
		
		Score[] leaders = leaderboard.getLeaders();
		
		// should produce default scores
		
		Score[] scores = new Score[leaderboard.numLeaders()];
		for(int i = 0; i < scores.length; i++){
			scores[i] = Score.newDefaultScore();
		}
		Assert.assertArrayEquals(scores, leaders);
	}
	
	@Test
	public void createWithShortArrayShouldPad(){
	
		// a new partly-custom leaderboard
		Score[] scores = {
				new Score(1, "scr", "1st"),
				new Score(2, "scr", "2nd"),
				new Score(3, "scr", "3rd"),
			};
		Leaderboard leaderboard = new Leaderboard("test", scores);
		
		// get resulting leaders
		
		Score[] leaders = leaderboard.getLeaders();
		
		// should be same at start then padded with default scores
		
		for(int i = 0; i < scores.length; i++){
			Assert.assertEquals(scores[i], leaders[i]);
		}
		
		for(int i = scores.length; i < leaders.length; i++){
			Assert.assertEquals(Score.newDefaultScore(), leaders[i]);
		}
	}
	
	@Test
	public void createWithLongArrayShouldTruncate(){
	
		// a new custom leaderboard
		
		Score[] scores = {
				new Score(1, "scr", "1st"),
				new Score(2, "scr", "2nd"),
				new Score(3, "scr", "3rd"),
				new Score(4, "scr", "4th"),
				new Score(5, "scr", "5th"),
				new Score(6, "scr", "6th"),
				new Score(7, "scr", "7th"),
				new Score(8, "scr", "8th"),
				new Score(9, "scr", "9th"),
				new Score(10, "scr", "10h"),
				new Score(11, "scr", "11t"),
				new Score(12, "scr", "12h"),
				new Score(13, "scr", "13h"),
			};
		Leaderboard leaderboard = new Leaderboard("test", scores);
		
		// get resulting leaders
		
		Score[] leaders = leaderboard.getLeaders();
		
		// first leaders.length leaders should match
		
		for(int i = 0; i < leaders.length; i++){
			Assert.assertEquals(scores[i], leaders[i]);
		}
	}
}
