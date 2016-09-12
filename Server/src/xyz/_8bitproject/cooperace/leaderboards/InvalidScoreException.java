package xyz._8bitproject.cooperace.leaderboards;

import com.matomatical.util.AntiException;

/** An unchecked anti-exception for use when something goes wrong
 *  reading a score submitted in a message
 * @author Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */
public class InvalidScoreException extends AntiException {

	/** Create a new InvalidScoreException with a message
	 * @param message Exception's message
	 */
	public InvalidScoreException(String message) {
		super(message);
	}

}
