package xyz._8bITProject.cooperace.leaderboards.protocol.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Leaderboard;
import xyz._8bITProject.cooperace.leaderboards.LeaderboardsManager;
import xyz._8bITProject.cooperace.leaderboards.Score;
import xyz._8bITProject.cooperace.leaderboards.protocol.LeadersResponse;
import xyz._8bITProject.cooperace.leaderboards.protocol.MessageException;
import xyz._8bITProject.cooperace.leaderboards.protocol.RequestBody;

/** RequestBodyTest tests the RequestBody unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class RequestBodyTest {
	
	/**
	 * If a request comes in without a level, an exception should
	 * be thrown
	 */
	@Test
	public void validationOfMissingLevelShouldFail(){
		try{
			// create incomplete submission body
			RequestBody body = new RequestBody(null);
			
			// attempt to respond
			body.response();
			
			// must throw an exception validating
			Assert.fail("Missing exception: MessageException");
			
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	/**
	 * If a request comes in with a level, the level's leaders should
	 * return the leaders of that level
	 */
	@Test
	public void requestForLevelShouldGiveLevelLeaders(){
		
		// set up leaderboard to test request from

		String level = "requestForLevelShouldGiveLevelLeaders test level";
		
		LeaderboardsManager manager = LeaderboardsManager.instance();
		Leaderboard board = manager.getLeaderboard(level);
		
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
			};
		
		for(Score score : scores){
			board.record(score);
		}
		
		scores = board.getLeaders(); // reflect padding, truncation, sorting
		
		
		// request for this level's leaders
		
		RequestBody body = new RequestBody(level);
		
		LeadersResponse response = body.response();
		
		// should be the correct leaders
		
		Assert.assertArrayEquals(scores, response.leaders);
		
	}
}
