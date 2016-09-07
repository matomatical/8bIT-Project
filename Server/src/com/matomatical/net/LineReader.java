package com.matomatical.net;

import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.io.IOException;

public class LineReader {
	
	private BufferedReader br;
	
	public LineReader(InputStream in){
		this.br = new BufferedReader(new InputStreamReader(in));
	}
	
	/** wraps {@link java.io.BufferedReader#readLine()}
	 *  (but converts exceptions and end-of-stream to unchecked ones) */
	public String readln() throws DisconnectException {
		try {
			String message = br.readLine();
			
			if(message == null){
				throw new DisconnectException("Connection ended");
			}
			
			return message;
			
		} catch (IOException e) {
			throw new DisconnectException("Connection problem (" + e.getMessage() + ")");
		}
	}

	/** essentially {@link java.io.BufferedReader#close()} */
	public void close() throws IOException {
		br.close();
	}
}
