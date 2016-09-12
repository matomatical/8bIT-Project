package com.matomatical.net;

import java.io.IOException;
import java.net.ServerSocket;

/** A Port acts as a unix-sockets-style welcoming socket.
 * Binds to a port and listens for connections.
 * Transforms all exceptions into sensible (unchecked)
 * Connection and Disconnect exceptions. 
 * @author Matt Farrugia
 */
public class Port {

	ServerSocket welcomingSocket;

	/** Open a new Port
	 * @param port port to bind to
	 * @throws ConnectionException when port is unavailable or invalid,
	 * or when an I/O error occurs
	 */
	public Port(short port) throws ConnectionException {
		try {
			welcomingSocket = new ServerSocket(port);
		} catch (IllegalArgumentException e) {
			throw new ConnectionException("Invalid port (" + e.getMessage() + ")");
			
		} catch (IOException e) {
			throw new ConnectionException(e.getMessage());
		}
	}

	/** Is the Port's welcoming socket bound? */
	public boolean isOpen() {
		return this.welcomingSocket.isBound();
	}

	/** create and return a new Peer on an incoming connection
	 * @throws ConnectionException if some I/O error occurs
	 * while waiting for a connection to come in
	 * @throws + other unchecked exceptions. See {@link ServerSocket#accept()}
	 */
	public Peer meet() throws ConnectionException {
		try {
			return new Peer(welcomingSocket.accept());
		
		} catch (IOException e) {
			throw new ConnectionException(e.getMessage());
		}
	}
	
	/** Clean up and close a Port when no new connections are required.
	 * Any thread currently waiting to {@link #meet()} will throw a {@link java.net.SocketException}.
	 * @throws DisconnectException if some I/O error occurs while closing
	 * the welcoming socket
	 */
	public void close() throws DisconnectException {
		try {
			welcomingSocket.close();
		} catch (IOException e) {
			throw new DisconnectException(e.getMessage());
		}
	}
}