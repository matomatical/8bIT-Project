package xyz._8bITProject.cooperace.leaderboards.protocol.tests;

import org.boon.Boon;
import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.protocol.PositionResponse;

/** PositionResponseTest tests the PositionResponse unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class PositionResponseTest {
	
	@Test
	public void boonToJsonOfRankingPositionShouldMatchProtocol(){
		
		// create a position response to convert
		
		PositionResponse response = new PositionResponse(1);

		// convert to JSON
		
		String json = Boon.toJson(response);

		// should match expected output by protocol
		
		Assert.assertEquals("{\"position\":1}", json);
	}

	@Test
	public void boonToJsonOfZeroPositionShouldMatchProtocol(){
		
		// create a position response to convert
		
		PositionResponse response = new PositionResponse(0);

		// convert to JSON
		
		String json = Boon.toJson(response);

		// should match expected output by protocol
		
		Assert.assertEquals("{}", json);
	}

	
	@Test
	public void equalsShouldRejectDifferentPositions(){
		
		// create two different positions
		
		PositionResponse one = new PositionResponse(1);
		PositionResponse two = new PositionResponse(2);
		
		// test equality
		
		boolean result = one.equals(two);
		
		// should be false
		
		Assert.assertEquals(false, result);
	}

	@Test
	public void equalsShouldAcceptSamePosition(){

		// create one position
		
		PositionResponse response = new PositionResponse(1);
		
		// test equality
		
		boolean result = response.equals(response);
		
		// should be true
		
		Assert.assertEquals(true, result);
	}
	
	@Test
	public void equalsShouldAcceptIdenticalPosition(){

		// create two identical positions
		
		PositionResponse one = new PositionResponse(1);
		PositionResponse two = new PositionResponse(1);
		
		// test equality
		
		boolean result = one.equals(two);
		
		// should be true
		
		Assert.assertEquals(true, result);
	}
	
	
	@Test
	public void equalsShouldRejectNullPositions(){

		// create one position
		
		PositionResponse response = new PositionResponse(1);
		
		// test equality with null
		
		boolean result = response.equals(null);
		
		// should be false
		
		Assert.assertEquals(false, result);
	}
	
	@Test
	public void equalsShouldRejectNonPositions(){

		// create one position and one non-position
		
		PositionResponse response = new PositionResponse(1);
		Object other = new Object();
		
		// test equality
		
		boolean result = response.equals(other);
		
		// should be false
		
		Assert.assertEquals(false, result);
	}
	
}
