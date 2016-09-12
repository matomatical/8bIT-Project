package com.matomatical.net;

import com.matomatical.util.AntiException;

/** An unchecked anti-exception for use when something goes wrong
 *  establishing a network connection
 *  @author Matt Farrugia
 */
public class ConnectionException extends AntiException {

	/** Create a new ConnectionException with a message
	 * @param message Exception's message
	 */
	public ConnectionException(String message) {
		super(message);
	}
}
