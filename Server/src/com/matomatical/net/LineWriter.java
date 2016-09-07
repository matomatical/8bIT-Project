package com.matomatical.net;

import java.io.OutputStream;
import java.io.PrintWriter;

public class LineWriter {
	
	private PrintWriter pw;

	public LineWriter(OutputStream out){
		this.pw = new PrintWriter(out, true);
	}
	
	/** effectively {@link java.io.PrintWriter#println()} */
	public void writeln(String s){
		pw.println(s);
		
		// under what conditions will this throw an exception? I want to be able to throw a disconnect exception if it fails!
	}

	/** effectively {@link java.io.PrintWriter#close()} */
	public void close() {
		pw.close();		
	}
}
