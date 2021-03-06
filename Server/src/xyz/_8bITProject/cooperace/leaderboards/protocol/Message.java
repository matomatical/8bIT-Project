package xyz._8bITProject.cooperace.leaderboards.protocol;

/** A Message is a command dispatcher populated with data from
 *  incoming JSON messages.
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class Message {
	
	/** string describing incoming message type (used for dispatching) */
	private String type;
	
	/** Data needed to responding to score request message */
	private RequestBody request;
	
	/** Data needed for responding to score submission message */
	private SubmissionBody submission;
	
	/** Create a new Message with
	 * @param type Type of message
	 * @param submission Submission body of message
	 * @param request Request body of message
	 */
	public Message(String type, SubmissionBody submission, RequestBody request){
		this.type = type;
		this.submission = submission;
		this.request = request;
	}
	
	/** Carry out the request contained in the message, return response
	 *  to request.
	 * @return Response object ready to be converted back into JSON
	 */
	public Response response(){
		
		if(type == null){
			throw new MessageException("missing message type");
		}
		
		switch(type){
		case "submission":
			if(submission != null){
				// we're looking at a valid score submission, let's respond! 
				return submission.response();
			} else {
				throw new MessageException("submission message without submission");
			}
			
		case "request":
			if(request != null){
				// we're looking at a valid response message, let's respond!
				return request.response();
			} else {
				throw new MessageException("request message without request");
			}
			
		default:
			// this type of message is unknown to us
			throw new MessageException("unknown message type");
		}
	}
	
	@Override
	public String toString(){
		return type + " message: request is [" + request + "] and submission is [" + submission + "]"; 
	}
}
