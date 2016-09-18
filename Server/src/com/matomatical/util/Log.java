package com.matomatical.util;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

import java.text.SimpleDateFormat;
import java.util.Date;

/** Minimal single-level static logger
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public abstract class Log {

	// OPTIONS
	
	/** Path to logfile */
	private static final String path = "logfile.txt";
	
	/** Overwrite existing logfile? */
	private static final boolean overwrite = false;
	
	/** Format for displaying the current time in log messages */
	private static SimpleDateFormat date = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss:SSS");
	
	// LOGFILE
	
	/** For writing to the logfile */
	private static PrintWriter logfile = logfile();
	private static PrintWriter logfile() {
		try{
			PrintWriter logfile = new PrintWriter(new FileWriter(path, !overwrite), true);
			logfile.println("*** BEGIN LOG ***");
			return logfile;
		} catch (IOException e) {
			throw new AntiException("Error opening logfile: " + e.getMessage());
		}
	}
	
	// STATIC LOG MESSAGE
	
	/** Print a message to the logfile */
	public static synchronized void log(String msg){
		long t = Thread.currentThread().getId();
		String time = date.format(new Date());
		logfile.println("("+time+") thread " + t + ": " + msg);
	}
}
