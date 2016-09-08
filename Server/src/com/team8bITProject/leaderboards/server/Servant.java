package com.team8bITProject.leaderboards.server;

import org.boon.Boon;
import org.boon.json.JsonException;

import com.matomatical.net.DisconnectException;
import com.matomatical.net.Peer;

public class Servant implements Runnable {

	Peer client;

	public Servant(Peer client){
		this.client = client;
	}
	
	@Override
	public void run() {
		
		try {
			
			// where the magic happens:
			while(client.isOpen()) {
				
				Message message = Boon.fromJson(client.recv(), Message.class);
				
				if(message.isOver()){
					client.close();
					// TODO: necessary to have this if we know we'll get a disconnect exception
					// when the other side closes the connection?
					
				} else {
					Message.Response response = message.response();
					client.send(Boon.toJson(response));
				}
			}

		} catch (DisconnectException e) {
			// it's possible that our connection to the client will terminate without a
			// message to tell us. in that case, we should gracefully finish the thread
			client.close();
			e.printMessage();
		
		} catch (MessageException | InvalidScoreException e){
			// we'll assume that legitimate clients don't send invalid messages,
			// and terminate connections upon receiving one.
			// for debugging purposes, we'll print these anomalies TODO and also log them 
			
			client.close();
			e.printMessage();
		} catch (Exception  e) {
			// this boon.jar isn't providing very good exception documentation,
			// but boon is the only remaining place that errors could come from.
			// So, if there's another exception, it must be from an invalid JSON
			// message or something else that went wrong during parsing.
			
			client.close();
			System.err.println(e.getMessage());
		}
	}
}
