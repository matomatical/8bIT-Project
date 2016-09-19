using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
/*
 * Is responsible for taking text input from the user, sending it the update manager,
 * and displaying the top n mos recent messages on screen.
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */
namespace _8bITProject.cooperace.multiplayer {
    public class ChatController : MonoBehaviour {
        
        // the update manager which should be told about any updates
        public IUpdateManager updateManager;

        // font size for the chat messages for when they show up on screen.
        public int FONT_SIZE = 80;

        private float MSG_X = 0.3f;
        private float MSG_Y = 0.03f;
        private int MSG_Y_OFFSET = 30;

        // A record of every message sent
        public ChatHistory chatHistory;

        // The OS keyboard
        private TouchScreenKeyboard keyboard;

        // Use this for initialization
        void Start() {
            chatHistory = new ChatHistory();
        }

        // This is called when the chat icon is pressed on screen. It initialises the device's onscreen keyboard.
        public void SendChatMessage() {
            keyboard = TouchScreenKeyboard.Open("Enter message here");
        }

        // Update is called once per frame
        void Update() {

            // If the user is done typing a message, add it to the chat history
            if (keyboard != null && keyboard.done) {
                string currMessage = keyboard.text;
                chatHistory.AddMessage(currMessage);
                keyboard = null;
            }
        }
        
        // displays the n most recent messages on screen
        void OnGUI() {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = FONT_SIZE;
            
            // The x and y positions at which to display the messages
            float ypos = (Screen.width * MSG_Y)-MSG_Y_OFFSET;
            float xpos = Screen.height * MSG_X;

            List<ChatMessage> recentMessages = chatHistory.MostRecent();

            for (int i = 0; i< recentMessages.Count; i++) {
                GUI.Label(new Rect(xpos, ypos+=MSG_Y_OFFSET, Screen.width, Screen.height), recentMessages[i].getMessage(), myStyle);
            }
        }

        

        public void Deserialize(List<byte> message) {
            throw new NotImplementedException();
        }

        public void Notify(List<byte> message) {
            throw new NotImplementedException();
        }

        public List<byte> Serialize(String meesage) {
            throw new NotImplementedException();
        }

        public void Subscribe(IListener<List<byte>> o) {
            throw new NotImplementedException();
        }

    }
}
