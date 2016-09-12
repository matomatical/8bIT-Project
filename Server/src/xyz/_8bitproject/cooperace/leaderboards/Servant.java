package xyz._8bitproject.cooperace.leaderboards;

import static com.matomatical.util.Log.log;

import org.boon.Boon;

import com.matomatical.net.DisconnectException;
import com.matomatical.net.Peer;

import xyz._8bitproject.cooperace.leaderboards.protocol.Message;
import xyz._8bitproject.cooperace.leaderboards.protocol.MessageException;
import xyz._8bitproject.cooperace.leaderboards.protocol.Response;

/** Simple client handler thread driver - dispatches incoming
 *  client messages to the Message class and and sends their
 *  response back. 
 *  Handles conversion of messages from-and-to JSON using Boon
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class Servant implements Runnable {

	/** The remote source of messages and destination for replies */
	Peer client;

	/** A new Servant set up to talk with a particular client
	 * @param client The client connection to talk on
	 */
	public Servant(Peer client){
		this.client = client;
	}
	
	@Override
	public void run() {
		
		log("new client servant starting communication loop");
		
		try {
			
			// communication loop
			
			while(client.isOpen()) {
				
				log("waiting for new message...");
				
				// receive and deserialise message
				
				String msg = client.recv();
				log("new message received!");
				
				Message message = Boon.fromJson(msg, Message.class);
				log("translated message: " + message.toString());
				
				// formulate, serialise and send response
				
				Response response = message.response();
				log("formulated reply: " + response.toString());
				
				client.send(Boon.toJson(response));
				log("sent reply!");
			}

		} catch (DisconnectException e) {
			// recv detected client disconnection, on purpose or not,
			// all we can do is end the thread
			
			client.close();
			e.printMessage();
			log("client disconnected: " + e.getMessage());
			
		} catch (MessageException | InvalidScoreException e){
			// we'll assume that legitimate clients don't send invalid messages,
			// and terminate connections upon receiving one.
			// for debugging purposes, we'll print these anomalies TODO and also log them 
			
			client.close();
			e.printMessage();
			log("invalid message: " + e.getMessage());
			
		} catch (Exception  e) {
			// this boon.jar isn't providing very good exception documentation,
			// but boon is the only remaining place that errors could come from.
			// So, if there's another exception, it must be from an invalid JSON
			// message or something else that went wrong during parsing.
			
			client.close();
			System.err.println(e.getMessage());
			log("JSON message error: " + e.getMessage());
		}
		
		// communication has ended, safe to end the thread
		log("end of communication loop, servant ending");
	}
}
