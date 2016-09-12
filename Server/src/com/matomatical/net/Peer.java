package com.matomatical.net;

import java.net.Socket;
import java.net.UnknownHostException;

import java.net.ConnectException;

import java.io.IOException;

/** A Peer acts as a unix-sockets-style TCP socket, encapsulating
 *  access to its reader and writer in C-style send() and recv() calls,
 *  and converting annoying checked IO Exceptions to sensible, unchecked
 *  network anti-exceptions
 * @author Matt Farrugia
 */
public class Peer {

	/** Java socket representing remote host */
	Socket remote;
	/** The socket's output stream */
	LineWriter o;
	/** The socket's input stream */
	LineReader i;

	/** Set up a Peer connection based on a hostname and port
	 * @param host String domain name or IP address, or null for loopback
	 * @param port The port number to connect to
	 * @throws ConnectionException when <ul>
	 * 	<li> the hostname cannot be mapped to an IP address (Hostname unknown), </li>
	 * 	<li> the host is unreachable, e.g. if no process is listening at the remote port, </li>
	 * 	<li> the port number is invalid (outside of 0 to 65535, inclusive), or </li>
	 * 	<li> some IO error occurs opening the socket or its streaming i/o objects </li>
	 * </ul>
	 */
	public Peer(String host, short port) throws ConnectionException {
		// NOTE: could DRY this by using the other constructor,
		// but not in java!
		try {
			this.remote = new Socket(host, port);
			
			this.i = new LineReader(remote.getInputStream());
			this.o = new LineWriter(remote.getOutputStream());
		
		} catch (ConnectException e) {
			throw new ConnectionException("Host unreachable (" + e.getMessage() + ")");
		
		} catch (UnknownHostException e) {
			throw new ConnectionException("Hostname unknown (" + e.getMessage() + ")");
		
		} catch (IllegalArgumentException e) {
			throw new ConnectionException("Invalid port (" + e.getMessage() + ")");
		
		} catch (IOException e) {
			throw new ConnectionException(e.getMessage());
		}
	}
	
	
	// create a peer directly from a new socket
	/** Create a new Peer directly from an existing Java Socket
	 * @param remote The existing Java Socket
	 * @throws ConnectionException if some IO error occurs opening
	 * the socket's streaming i/o objects
	 */
	public Peer(Socket remote) throws ConnectionException {
		try {
			this.remote = remote;
			
			this.i = new LineReader(remote.getInputStream());
			this.o = new LineWriter(remote.getOutputStream());
			
		} catch (IOException e) {
			throw new ConnectionException(e.getMessage());
		}
	}

	/** Close and clean up socket resources. <br>
	 * (NOTE: Any subsequent reads/writes will fail) 
	 * @throws DisconnectException if some IO error is encountered
	 * while closing the socket or its streaming i/o objects
	 */
	public void close() throws DisconnectException{
		try {
			// clean up resources
			i.close();
			o.close();
			remote.close();
			
		} catch (IOException e) {
			// something went wrong closing the resources
			throw new DisconnectException(e.getMessage());
		}
	}

	/** Query the state of the socket
	 * @return true iff socket has not been closed, false otherwise
	 */
	public boolean isOpen() {
		return !remote.isClosed();
	}
	
	/** Send a line to the Peer through the socket.
	 * @param message String message to send to the peer
	 */
	public void send(String message) {
		o.writeln(message);
	}

	/** Receive the next line from the Peer connection
	 * @return the line received from the socket (blocks until '\n' or EOF)
	 * @throws DisconnectException if the connection is closed and there is
	 * no more to read
	 */
	public String recv() throws DisconnectException {
		return i.readln();
	}


}
