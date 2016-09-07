package com.team8bITProject.leaderboards.server;

import com.matomatical.util.AntiException;

public class InvalidScoreException extends AntiException {

	public InvalidScoreException(String message) {
		super(message);
	}

}
