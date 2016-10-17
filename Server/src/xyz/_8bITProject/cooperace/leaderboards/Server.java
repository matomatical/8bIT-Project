package xyz._8bITProject.cooperace.leaderboards;

import com.matomatical.util.Log;
import static com.matomatical.util.Log.log;
import com.matomatical.net.Port;
import com.matomatical.net.Peer;

/** Simple thread-spawning server driver, listens on a port specified
 *  in args[0] (or on a default port) for incoming connections
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class Server {
	
	/** Default port to listen on (if none specified on command line) */
	public static final short DEFAULT_PORT = 2693;
	
	/** Listens on a port specified in args[0] (or on a default port)
	 *  for incoming connections, spawning new threads to handle incoming
	 *  connections
	 */
	public static void main(String[] args) {
		Log.setPath("./server.log");
		
		log("starting server!");
		
		// set up server
		
		Options opts = opts(args);
		
		Port arrivals = new Port(opts.port);
		log("server listening on port " + opts.port);
		
		// connection loop
		
		while(arrivals.isOpen()){
			Peer client = arrivals.meet();
			log("server connected a new client from " + client.getAddress());
			
			Servant servant = new Servant(client);
			Thread thread = new Thread(servant);
			thread.start();
		}
		
		// if port closes for some reason
		
		arrivals.close();
		log("server has stopped listening for new connections");
		
		log("server closing");
	}

	/** helper method to convert args to options */
	private static Options opts(String[] args) {
		
		if(args.length > 0){
			try{
				short port = Short.parseShort(args[0]);
				return new Options(port);
				
			} catch (NumberFormatException e) {
				// continue to default case...
			}
		}
		
		// missing or invalid port option: use default
		return new Options(DEFAULT_PORT);
	}
	
	/** helper data structure to store options */
	private static class Options{
		public final short port;
		public Options(short port){this.port = port;}
	}
}
