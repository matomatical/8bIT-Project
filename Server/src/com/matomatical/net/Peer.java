package com.matomatical.net;

import java.net.Socket;
import java.net.UnknownHostException;

import java.net.ConnectException;

import java.io.IOException;

public class Peer {

	Socket remote;
	LineWriter o;
	LineReader i;

	// set up a peer connection based on a hostname and port
	// could DRY this by using the other constructor but not in java!
	public Peer(String host, short port) throws ConnectionException {
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
	public Peer(Socket remote) {
		try {
			this.remote = remote;
			
			this.i = new LineReader(remote.getInputStream());
			this.o = new LineWriter(remote.getOutputStream());
			
		} catch (IOException e) {
			throw new ConnectionException(e.getMessage());
		}
	}

	public void close() throws DisconnectException{
		// clean up resources
		try {
			i.close();
			o.close();
			remote.close();
			
		} catch (IOException e) {
			throw new DisconnectException(e.getMessage());
		}
	}

	public void send(String message) {
		o.writeln(message);
	}

	public String recv() throws DisconnectException {
		return i.readln();
	}

	public boolean isOpen() {
		return !remote.isClosed();
	}

}
