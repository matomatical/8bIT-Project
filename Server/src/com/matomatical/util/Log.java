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
	private static String path = "logfile.txt";

	/** Overwrite existing logfile? */
	private static boolean overwrite = false;

	/** Log to stdout as well? */
	private static boolean logToStdout = true;

	/** Format for displaying the current time in log messages */
	private static SimpleDateFormat date = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss:SSS");

	// LOGFILE

	/** For writing to the logfile */
	private static PrintWriter writer;
	private static PrintWriter getWriter() {
		if (writer == null) {
			try {
				writer = new PrintWriter(new FileWriter(path, !overwrite));
				writer.println("*** BEGIN LOG ***");
			} catch (IOException e) {
				throw new AntiException("Error opening logfile: " + e.getMessage());
			}
		}
		return writer;
	}

	// PUBLIC METHODS

	/** Allow changing location of logfile */
	public static synchronized void setPath(String path) {
		setPath(path, false);
	}
	public static synchronized void setPath(String path, boolean overwrite) {
		Log.path = path;
		Log.overwrite = overwrite;
		if (writer != null) {
			writer.close();
		}
	}

	/** Allow toggling stdout logging */
	public static synchronized void setStdoutLogging(boolean value) {
		Log.logToStdout = value;
	}

	/** Print a message to the logfile */
	public static synchronized void log(String msg){
		msg = String.format("(%s) thread %s: %s",
			date.format(new Date()), Thread.currentThread().getId(), msg);

		if (logToStdout) {
			System.out.println(msg);
		}

		writer = getWriter();
		writer.println(msg);
		writer.flush();
	}

}
