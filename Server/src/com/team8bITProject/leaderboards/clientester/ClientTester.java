package com.team8bITProject.leaderboards.clientester;

import com.matomatical.net.Peer;

public class ClientTester {

	public static void main(String[] args) {
		
		// connect to server at specified address and port
		
		Peer server = new Peer("localhost", (short)1024);
		
		// send some info
		
		String message = "hello, server!";
		System.out.println("sending: " + message);
		server.send(message);
		
		
		// recv some info
		
		String reply = server.recv();
		System.out.println("reply from server: " + reply);
		
		// disconnect

		server.close();
	}

}
