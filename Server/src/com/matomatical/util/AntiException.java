package com.matomatical.util;

import java.io.PrintStream;

/** A default unchecked (runtime) exception with some additional,
 *  commonly used helpful wrapper methods provided
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class AntiException extends RuntimeException {

	/** Generated default serial version unique identifier */
	protected static final long serialVersionUID = 6246280879205431706L;

	/** Create a new AntiException with a message
	 * @param message Exception's message
	 */
	public AntiException(String message){
		super(message);
	}
	
	/** Prints this.getMessage() to System.err */
	public void printMessage(){
		System.err.println(this.getMessage());
	}
	
	/** Prints this.getMessage() to PrintStream o */
	public void printMessage(PrintStream o){
		o.println(this.getMessage());
	}
}
