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
        private int FONTSIZE = 40;

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
            GUIStyle mystyle = new GUIStyle();
            mystyle.fontSize = FONTSIZE;

            // These values have been tested on a resolution of 1024x768.
            // The gui matrix will handle scaling the gui components to suit different resolutions
            float scalex = Screen.width / 1280.0f;
            float scaley = Screen.height / 720.0f;

            // The of the first chat message should appear if the resolution is 1024x768.
            float xpos = 110, ypos = 10;

            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(scalex, scaley, 1));           //And create your elements
            GUI.color = Color.black;

            // The y pos for the next message
            int offset = mystyle.fontSize + 8; // Add a few pixels for breathing room

            // get the most recent messages
            List<ChatMessage> recentMessages = chatHistory.MostRecent(TOPN);
            
            for (int i = 0; i< recentMessages.Count; i++) {
                ChatMessage m = recentMessages[i];

                // generate a message with the tag (which player sent it) appended to it
				string taggedMsg = assignTag(m.message, m.isFromMe);
                
                // display it on screen
                GUI.Label(new Rect(xpos, ypos+= offset, Screen.height,Screen.width), taggedMsg, mystyle);
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