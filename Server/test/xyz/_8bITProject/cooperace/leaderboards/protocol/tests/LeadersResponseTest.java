package xyz._8bITProject.cooperace.leaderboards.protocol.tests;

import org.boon.Boon;
import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Score;
import xyz._8bITProject.cooperace.leaderboards.protocol.LeadersResponse;

public class LeadersResponseTest {
	
	@Test
	public void boonToJsonShouldMatchProtocol(){
		
		// create leaders response
		
		Score[] leaders = {
				new Score(1, "sco", "res"),
				new Score(2, "in ", "an "),
				new Score(3, "arr", "ay!"),
				new Score(4, "yay", "woo")
			};
		LeadersResponse response = new LeadersResponse(leaders);
		
		// convert to JSON
		
		String json = Boon.toJson(response);
		
		// should match expected output by protocol
		
		Assert.assertEquals("{\"leaders\":["
				+"{\"time\":1,\"player1\":\"sco\",\"player2\":\"res\"},"
				+"{\"time\":2,\"player1\":\"in \",\"player2\":\"an \"},"
				+"{\"time\":3,\"player1\":\"arr\",\"player2\":\"ay!\"},"
				+"{\"time\":4,\"player1\":\"yay\",\"player2\":\"woo\"}]}", json);
	}
	
}
