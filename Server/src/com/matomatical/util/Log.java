package com.matomatical.util;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

import java.text.SimpleDateFormat;
import java.util.Date;

/** Minimal single-level static logger
 * @author Matt Farrugia
 */
public abstract class Log {

	/** Path to logfile */
	private static final String path = "logfile.txt";
	
	/** Overwrite existing logfile? */
	private static final boolean overwrite = false;
	
	/** for formatting the current time for log messages */
	private static SimpleDateFormat date = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss:SSS");
	
	/** for writing to the logfile */
	private static PrintWriter logfile = logfile();
	private static PrintWriter logfile() {
		try{
			PrintWriter logfile = new PrintWriter(new FileWriter(path, !overwrite));
			logfile.println("*** BEGIN LOG ***");
			return logfile;
		} catch (IOException e) {
			throw new AntiException("Error opening logfile: " + e.getMessage());
		}
	}
	
	/** Print a message to the logfile */
	public static synchronized void log(String msg){
		long t = Thread.currentThread().getId();
		String time = date.format(new Date());
		logfile.println("("+time+") thread " + t + ": " + msg);
	}
}
