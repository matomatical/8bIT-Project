/*
 * ChatGUI is responsible for displaying the most recent chat messages
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
    public class ChatGUI {

        // font size for the chat messages for when they show up on screen.
        private int FONT_SIZE = 40;

        // Details of where to render chat
        private readonly float MSG_X = 0.3f;
        private readonly float MSG_Y = 0.03f;

        // Display the n most recent messages 
        public readonly int TOPN = 3;

        // A record of all the messages sent
        private ChatHistory chatHistory;

        public ChatGUI (ChatHistory chatHistory) {
            this.chatHistory = chatHistory;
        }

        /// displays the n most recent messages on screen
        public void RenderChatGUI() {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = FONT_SIZE;
            
            // The x and y positions at which to display the messages
            float ypos = (Screen.width * MSG_Y)-FONT_SIZE;

            // get the most recent messages
            List<ChatMessage> recentMessages = chatHistory.MostRecent(TOPN);

            for (int i = 0; i< recentMessages.Count; i++) {
                ChatMessage m = recentMessages[i];

                float xpos = Screen.height * MSG_X;

                // generate a message with the tag (which player sent it) appended to it
				string taggedMsg = assignTag(m.message, m.localPlayerMsg);
                
                // display it on screen
                GUI.Label(new Rect(xpos, ypos+= FONT_SIZE, Screen.width, Screen.height),
                    taggedMsg, myStyle);
            }

            
        }

        /// Take a string, check which player sent it and assign a tag accordingly
        private string assignTag(string m, bool isLocalPlayer) {
            if (isLocalPlayer) {
                m = "      me: " + m;
            } else {
                m = "partner: " + m;
            }
            return m;
        }
    }
}