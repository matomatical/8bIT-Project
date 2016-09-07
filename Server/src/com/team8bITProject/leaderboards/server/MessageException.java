package com.team8bITProject.leaderboards.server;

import com.matomatical.util.AntiException;

public class MessageException extends AntiException {

	public MessageException(String message) {
		super("Message Exception: " + message);
	}

}
