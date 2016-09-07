package com.matomatical.util;

import java.io.PrintStream;

public class AntiException extends RuntimeException {

	protected static final long serialVersionUID = 6246280879205431706L;

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
