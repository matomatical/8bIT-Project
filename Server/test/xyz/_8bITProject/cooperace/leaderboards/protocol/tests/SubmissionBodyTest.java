package xyz._8bITProject.cooperace.leaderboards.protocol.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.InvalidScoreException;
import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;
import xyz._8bITProject.cooperace.leaderboards.Score;
import xyz._8bITProject.cooperace.leaderboards.protocol.MessageException;
import xyz._8bITProject.cooperace.leaderboards.protocol.PositionResponse;
import xyz._8bITProject.cooperace.leaderboards.protocol.SubmissionBody;

/** SubmissionBodyTest tests the SubmissionBody unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class SubmissionBodyTest {
	
	@Test
	public void validationOfMissingLevelShouldFail(){
		try{
			// create incomplete submission body
			SubmissionBody body = new SubmissionBody(null,
					new Score(1, "abc", "def"));
			
			// attempt to respond
			body.response();
			
			// must throw an exception validating
			Assert.fail("Missing exception: MessageException");
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	@Test
	public void validationOfMissingScoreShouldFail(){
		try{
			// create incomplete submission body
			SubmissionBody body = new SubmissionBody("test level name", null);
			
			// attempt to respond
			body.response();
			
			// must throw an exception validating
			Assert.fail("Missing exception: MessageException");
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	@Test
	public void validationOfInvalidScoreShouldFail(){
		try{
			// create incomplete submission body
			SubmissionBody body = new SubmissionBody("test level",
					new Score(-1, "abc", "def"));
			
			// attempt to respond
			body.response();
			
			// must throw an exception validating
			Assert.fail("Missing exception: InvalidScoreException");
		} catch (InvalidScoreException e) {
			// passed the test!
		}
	}
	
	@Test
	public void validationOfCompleteBodyShouldPass(){
		try{
			// create incomplete submission body
			SubmissionBody body = new SubmissionBody("test level", new Score(1, "abc", "def"));
			
			// attempt to respond
			body.response();
			
			// passed the test!
			
		} catch (Exception e) {
			// must NOT throw an exception validating
			Assert.fail("No Exception Expected");
		}
	}
	
	@Test
	public void submissionOfRankingScoreShouldReturnPosition(){
		
		// set up leaderboard to test submission on
		
		String level = "submissionOfRankingScoreShouldReturnPosition test level";
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		Leaderboard board = manager.getLeaderboard(level);
		
		// submit score that should rank 1st
		
		int time = board.getLeaders()[0].time;
		Score score = new Score(time-1, "pl1", "pl2");
		
		SubmissionBody body = new SubmissionBody(level, score);
		
		PositionResponse response = (PositionResponse) body.response();
		
		// should give position 1
		
		Assert.assertEquals(new PositionResponse(1), response);
	}
	
	@Test
	public void submissionOfNonRankingScoreShouldReturnPositionZero(){
		
		// set up leaderboard to test submission on
		
		String level = "submissionOfNonRankingScoreShouldReturnPositionZero test level";
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		Leaderboard board = manager.getLeaderboard(level);
		
		// submit score that should not rank
		
		int time = board.getLeaders()[board.numLeaders()-1].time;
		Score score = new Score(time+1, "pl1", "pl2");
		
		SubmissionBody body = new SubmissionBody(level, score);
		
		PositionResponse response = body.response();
		
		// should give position 0
		
		Assert.assertEquals(new PositionResponse(0), response);
	}
}
