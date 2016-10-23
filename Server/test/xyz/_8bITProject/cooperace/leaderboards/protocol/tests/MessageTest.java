package xyz._8bITProject.cooperace.leaderboards.protocol.tests;

import org.junit.Assert;
import org.junit.Test;

import xyz._8bITProject.cooperace.leaderboards.Score;
import xyz._8bITProject.cooperace.leaderboards.protocol.LeadersResponse;
import xyz._8bITProject.cooperace.leaderboards.protocol.Message;
import xyz._8bITProject.cooperace.leaderboards.protocol.MessageException;
import xyz._8bITProject.cooperace.leaderboards.protocol.PositionResponse;
import xyz._8bITProject.cooperace.leaderboards.protocol.RequestBody;
import xyz._8bITProject.cooperace.leaderboards.protocol.Response;
import xyz._8bITProject.cooperace.leaderboards.protocol.SubmissionBody;

/** MessageTes tests the Message unit
 * @author Matt <farrugiam@student.unimelb.edu.au>
 */
public class MessageTest {

	/**
	 * If we try to respond to a message without a message type, a
	 * message exception should be thrown
	 */
	@Test
	public void responseToMissingTypeShouldFail(){
		try{
			// create incomplete message
			Message message = new Message(null,
					new SubmissionBody("test", new Score(3, "two", "one")),
					new RequestBody("test"));
			
			// attempt to respond
			message.response();
			
			// must throw an exception dispatching
			Assert.fail("Missing exception: MessageException");
			
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	/**
	 * If we try to respond to a message with an unrecognised message type, a
	 * message exception should be thrown
	 */
	@Test
	public void responseToUnknownTypeShouldFail(){
		try{
			// create incomplete message
			Message message = new Message("made up message type",
					new SubmissionBody("test", new Score(3, "two", "one")),
					new RequestBody("test"));
			
			// attempt to respond
			message.response();
			
			// must throw an exception dispatching
			Assert.fail("Missing exception: MessageException");
			
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	/**
	 * If we try to respond to a submission message without a submission body,
	 * a message exception should be thrown
	 */
	@Test
	public void responseToMissingSubmissionBodyShouldFail(){
		try{
			// create incomplete message
			Message message = new Message("submission", null, new RequestBody("test"));
			
			// attempt to respond
			message.response();
			
			// must throw an exception dispatching
			Assert.fail("Missing exception: MessageException");
			
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	/**
	 * If we try to respond to a request message without a request body,
	 * a message exception should be thrown
	 */
	@Test
	public void responseToMissingRequestBodyShouldFail(){
		try{
			// create incomplete message
			Message message = new Message("request",
					new SubmissionBody("test", new Score(3, "two", "one")), null);
			
			// attempt to respond
			message.response();
			
			// must throw an exception dispatching
			Assert.fail("Missing exception: MessageException");
			
		} catch (MessageException e) {
			// passed the test!
		}
	}
	
	/**
	 * If we try to respond to a submission message WITH a submission body,
	 * a message exception should NOT be thrown.
	 */
	@Test
	public void responseToSubmissionMessageShouldNotFail(){
		try{
			// create complete message
			Message message = new Message("submission",
					new SubmissionBody("test", new Score(3, "two", "one")), null);
			
			// attempt to respond
			message.response();
			
			// passed the test!
			
		} catch (MessageException e) {
			
			// failed the test, unexpected exception
			Assert.fail("Unexpected Exception");
		}
	}
	
	/**
	 * If we try to respond to a request message WITH a request body,
	 * a message exception should NOT be thrown.
	 */
	@Test
	public void responseToRequestMessageBodyShouldNotFail(){
		try{
			// create complete message
			Message message = new Message("request", null, new RequestBody("test"));
			
			
			// attempt to respond
			message.response();
			
			// passed the test!
			
		} catch (MessageException e) {
			
			// failed the test, unexpected exception
			Assert.fail("Unexpected Exception");
		}
	}
	
	
	/**
	 * The response to a submission message should be a valid position response
	 */
	@Test
	public void responseToSubmissionShouldBePositionResponse(){
		
		// create submission message
		
		Message message = new Message("submission",
				new SubmissionBody("test", new Score(3, "two", "one")), null);
		
		// get response
		
		Response response = message.response();
		
		// response should be a PositionResponse
		
		Assert.assertTrue(response instanceof PositionResponse);
	}
	
	/**
	 * The response to a request message should be a valid leaders response
	 */
	@Test
	public void responseToRequestShouldBeLeadersMessage(){
		
		// create request message
		
		Message message = new Message("request", null, new RequestBody("test"));
		
		// get response
		
		Response response = message.response();
		
		// response should be a PositionResponse
		
		Assert.assertTrue(response instanceof LeadersResponse);
	}
}
