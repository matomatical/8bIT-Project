/*
 * ChatGUI is responsible for displaying the history of chat messages
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class ChatGUI {

		// font size for the chat messages for when they show up on screen.
		private int FONT_SIZE = 20;

		// Details of where to render chat
		private readonly float MSG_X = 0.3f;
		private readonly float MSG_Y = 0.03f;
		private readonly int MSG_Y_OFFSET = 30;

		// Display the n most recent messages 
		private readonly int TOPN = 3;

		private ChatHistory chatHistory;

		public ChatGUI (ChatHistory chatHistory) {
			this.chatHistory = chatHistory;
		}

		// displays the n most recent messages on screen
		public void RenderChatGUI() {
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = FONT_SIZE;

			// The x and y positions at which to display the messages
			float ypos = (Screen.width * MSG_Y)-MSG_Y_OFFSET;
			float xpos = Screen.height * MSG_X;

			List<ChatMessage> recentMessages = chatHistory.MostRecent(TOPN);

			for (int i = 0; i< recentMessages.Count; i++) {
				GUI.Label(new Rect(xpos, ypos+=MSG_Y_OFFSET, Screen.width, Screen.height),
					recentMessages[i].getMessage(), myStyle);
			}
		}
	}
}