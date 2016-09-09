package com.team8bITProject.leaderboards.server;

public class Message {
	
	private String type;
	
	private Request request;
	private Submission submission;
	
	public Response response(){
		
		switch(type){
		case "submission":
			if(submission != null){
				return submission.response();
			} else {
				throw new MessageException("submission message without submission");
			}
			
		case "request":
			if(request != null){
				return request.response();
			} else {
				throw new MessageException("request message without request");
			}
			
		default:
			throw new MessageException("missing or unknown message type");
		}
	}
	
	
	private class Request {
		public String level;

		public Response response() {
			
			// validation
			
			if(level == null){
				throw new MessageException("missing level in request message");
			}
			
			// build response object
			
			Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
			
			return new LeadersResponse(lb.getLeaders());
		}
		
		private class LeadersResponse extends Response {

			public final Score[] leaders;
			
			public LeadersResponse(Score[] leaders) {
				this.leaders = leaders;
			}
		}
	}
	
	private class Submission {
		public String level;
		public Score score;
		
		public Response response() {
			
			// validation
			
			if(level == null){
				throw new MessageException("missing level in submission message");
			
			} else if(score == null){
				throw new MessageException("missing score in submission message");
			
			} else if (score.time <= 0){ // TODO NOTE: actual scores of 0 will be considered invalid
				throw new MessageException("incomplete score: time field missing or zero");
				
			} else if (score.player1 == null){
				throw new MessageException("incomplete score: player1 field missing");
				
			} else if (score.player2 == null) {
				throw new MessageException("incomplete score: player2 field missing");
			}
			
			// build response
			
			Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
			
			int position = lb.rank(score);
			
			if(position > 0){
				// we placed!
				
				// NOTE that this will update the position as well:
				// it's possible that someone got in between us ranking and
				// then recording our score on the leaderboard it's even
				// possible that we now didn't get recorded and then position
				// will be zero from now on (and zero will be sent to the client)
				position = lb.record(score);
			}

			// either way, make a reply
			
			return new PositionResponse(position);
		}
		
		private class PositionResponse extends Response {

			public final int position;
			
			public PositionResponse(int position) {
				this.position = position;
			}
		}
	}
	
	public abstract class Response {

	}
}
