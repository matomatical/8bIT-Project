/*
 * ChatGUI is responsible for displaying the history of chat messages
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
    public class ChatGUI {

        // font size for the chat messages for when they show up on screen.
        private int FONT_SIZE = 35;

        // Details of where to render chat
        private readonly float MSG_X = 0.3f;
        private readonly float MSG_Y = 0.03f;
        private readonly int MSG_Y_OFFSET = 35;
        private readonly int MSG_X_OFFSET = 70;

        // Display the n most recent messages 
        public readonly int TOPN = 3;

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
            float xposLocal = Screen.height * MSG_X + MSG_X_OFFSET;
            float xposRemote = Screen.height * MSG_X;

            List<ChatMessage> recentMessages = chatHistory.MostRecent(TOPN);

            for (int i = 0; i< recentMessages.Count; i++) {
                //createRectangle(xpos, ypos += MSG_Y_OFFSET, recentMessages[i].getMessage(), 1, 0, 0);
                ChatMessage m = recentMessages[i];
                float xpos = xposRemote;

                // If the message was sent by the local player, indent it appropriately
                if (m.getLocalMsg()) {
                    xpos = xposLocal;
                }

                GUI.Label(new Rect(xpos, ypos+= MSG_Y_OFFSET, Screen.width, Screen.height),
                    m.getMessage(), myStyle);
            }

            
        }

        private void createRectangle(float x, float y, string m, int r, int g, int b) {
            int w = 15 * FONT_SIZE;
            int h = FONT_SIZE + 5;

            Texture2D rgb_texture = new Texture2D(w, h);
            Color rgb_color = new Color(r, g, b);
            int i, j;
            for (i = 0; i < w; i++) {
                for (j = 0; j < h; j++) {
                    rgb_texture.SetPixel(i, j, rgb_color);
                }
            }
            rgb_texture.Apply();
            GUIStyle generic_style = new GUIStyle();
            GUI.skin.box = generic_style;
            GUI.Box(new Rect(x, y, w, h), rgb_texture);
        }
    }
}