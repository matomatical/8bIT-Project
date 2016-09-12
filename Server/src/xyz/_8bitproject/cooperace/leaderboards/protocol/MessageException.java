package xyz._8bitproject.cooperace.leaderboards.protocol;

import com.matomatical.util.AntiException;

/** An unchecked anti-exception for use when something goes wrong
 *  generating or responding to a message
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class MessageException extends AntiException {

	/** Create a new MessageException with a message
	 * @param message Exception's message
	 */
	public MessageException(String message) {
		super("Message Exception: " + message);
	}

}
