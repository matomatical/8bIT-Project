package com.matomatical.net;

import java.io.OutputStream;
import java.io.PrintWriter;

/** A wrapper for java.io's PrintWriter, serving to
 *  drastically simplify creation and use
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class LineWriter {
	
	/** The wrapped print writer object */
	private PrintWriter pw;

	/** Creates a new, auto-flushing LineWriter from an existing OutputStream
	 * @param out An output stream object
	 */
	public LineWriter(OutputStream out){
		this.pw = new PrintWriter(out, true);
	}
	
	/** Wraps {@link java.io.PrintWriter#println()}. LineWriters are created with
	 * autoFlush enabled, meaning this method will also flush the output buffer
	 * @param s String to write to the output stream (using println, meaning a
	 * newline will be appended)
	 */
	public void writeln(String s){
		pw.println(s);
		
		// TODO: investigate: will println() here ever throw an exception?
		// doesn't look like it.
	}

	/** Wraps {@link java.io.PrintWriter#close()} */
	public void close() {
		pw.close();		
	}
}
