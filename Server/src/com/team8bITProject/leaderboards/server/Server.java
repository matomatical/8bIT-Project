package com.team8bITProject.leaderboards.server;

import com.matomatical.net.Port;
import com.matomatical.net.Peer;

public class Server {
	
	public static final short PORT = 1024;
	
	public static void main(String[] args) {
		
		Port arrivals = new Port(PORT);
		
		while(arrivals.isOpen()){
			Peer client = arrivals.meet();
			
			Servant servant = new Servant(client);
			servant.run();
		}
		
		arrivals.close();
	}
}