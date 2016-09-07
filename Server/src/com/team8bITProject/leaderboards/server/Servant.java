package com.team8bITProject.leaderboards.server;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.boon.json.JsonFactory;
import org.boon.json.ObjectMapper;
import com.matomatical.net.Peer;

public class Servant implements Runnable {

	Peer client;
	
	 // (TODO: abstract this?)
	ObjectMapper mapper = JsonFactory.create(); // for reading/generating JSON
	
	public Servant(Peer client){
		this.client = client;
	}
	
	@Override
	public void run() {
		
		try {
			while(client.isOpen()) {
			
				Map map = mapper.readValue(client.recv(), Map.class);
				
				String type = (String) map.get("type");
				
				switch(type){
				case "submit":
					// handle the score submission
					submit(map);
					break;
					
				case "request":
					// handle the leaders request
					request(map);
					break;
					
				case "finished":
					// handle the finish request
					finished(map);
					break;
					
				default:
					throw new MessageException("missing or unknown message type");
				}
			}
		} catch (MessageException | InvalidScoreException e){
			
			// we'll assume that legitimate clients don't send invalid messages,
			// and terminate connections upon receiving one.
			client.close();
			
			// but for debugging purposes, we'd better print the message here
			e.printMessage();
			
			// TODO: put this all in the log, too
		}
	}

	private void finished(Map map) {
		client.close();
	}

	private void request(Map map) {
		
		// get level leaderboard from request
		
		String level = (String) map.get("level");
		if(level == null){throw new MessageException("missing level name in request message");}
		
		Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
		
		
		// formulate reply message
		
		HashMap<String, Object> reply = new HashMap<>();
		
		List<Map<String, Object>> leaders = new ArrayList<>(lb.numLeaders());
		
		for(Score score : lb.getLeaders()){
			
			Map<String, Object> scoreMap = new HashMap<>();
			
			scoreMap.put("time", score.time);
			scoreMap.put("player1", score.player1);
			scoreMap.put("player2", score.player2);
			
			leaders.add(scoreMap);
		}
		
		reply.put("leaders", Arrays.asList(leaders));
		
		// generate reply json and send to client
		
		client.send(mapper.writeValueAsString(reply));
	}

	private void submit(Map map) {
		
		Map scoreMap = (Map) map.get("score");
		if(scoreMap == null){throw new MessageException("missing score in submit message");}
		
		String level = (String) scoreMap.get("level");
		if(level == null){throw new MessageException("missing level name in score in submit message");}
		
		Leaderboard lb = LeaderboardsManager.instance().getLeaderboard(level);
		
		Integer time = (Integer) scoreMap.get("time");
		String player1 = (String) scoreMap.get("player1");
		String player2 = (String) scoreMap.get("player2");
		
		if (time == null){
			throw new MessageException("incomplete score: time field missing");
		} else if (player1 == null){
			throw new MessageException("incomplete score: player1 field missing");
		} else if (player2 == null) {
			throw new MessageException("incomplete score: player2 field missing");
		}
		
		Score score = new Score(time, player1, player2);
		
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
		Map<String, Object> reply = new HashMap<String, Object>();
		reply.put("position", position);

		// generate json and send to client
		client.send(mapper.writeValueAsString(reply));	
	}
}
