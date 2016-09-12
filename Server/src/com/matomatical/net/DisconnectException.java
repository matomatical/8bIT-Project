package com.matomatical.net;

import com.matomatical.util.AntiException;

/** An unchecked anti-exception for use when something
 *  interrupts a network connection
 *  @author Matt Farrugia
 */
public class DisconnectException extends AntiException {

	/** Create a new DisconnectException with a message
	 * @param message Exception's message
	 */
	public DisconnectException(String message){
		super(message);
	}
}
