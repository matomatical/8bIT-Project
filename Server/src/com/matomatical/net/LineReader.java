package com.matomatical.net;

import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.io.IOException;

// TODO: refactor exception hierarchy within the com.matomatical.net package
// and separate these IO classes into a general IO package

/** A wrapper for java.io's BufferedReader, serving to
 *  drastically simplify creation and use, and also to
 *  convert annoying IO exceptions into meaningful,
 *  unchecked anti-exceptions
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class LineReader {
	
	/** The wrapped buffered reader object */
	private BufferedReader br;
	

	/** Creates a new, default-sized-buffer LineReader from an existing InputStream
	 * @param in An input stream object
	 */
	public LineReader(InputStream in){
		this.br = new BufferedReader(new InputStreamReader(in));
	}
	
	/** Wraps {@link java.io.BufferedReader#readLine()}, but converts exceptions
	 * and end-of-stream errors to to unckecked anti-exceptions
	 * @return the next line read from the buffered reader (blocks until '\n')
	 * @throws DisconnectException when the connection has ended, or a problem
	 * has caused the connection to be lost.
	 */
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

	/** Wraps {@link java.io.BufferedReader#close()} */
	public void close() throws IOException {
		// TODO: See comment at top of file! We're inconsistently
		// wrapping IOExceptions during use/creation/closing; unify this!
		br.close();
	}
}
