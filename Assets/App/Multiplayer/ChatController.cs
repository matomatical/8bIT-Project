using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;
/*
 * Is responsible for taking text input from the user and sending it to the update manager
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */
namespace xyz._8bITProject.cooperace.multiplayer {
    public class ChatController : MonoBehaviour {
        
        // the update manager which should be told about any updates
        public IUpdateManager updateManager;

        // A record of every message sent
        public ChatHistory chatHistory;

        // The class which looks after rendering the chat
        private ChatGUI chatGUI;

        // The OS keyboard
        private TouchScreenKeyboard keyboard;

        // Use this for initialization
        void Start() {
            chatHistory = new ChatHistory();
            chatGUI = new ChatGUI (chatHistory);
        }

        // This is called when the chat icon is pressed on screen. It initialises the device's onscreen keyboard.
        public void SendChatMessage() {
            keyboard = TouchScreenKeyboard.Open("Enter message here");
        }

        // Update is called once per frame
        void Update() {
            List<byte> messageList;

            // If the user is done typing a message, add it to the chat history and send
            if (keyboard != null && keyboard.done) {
                string currMessage = keyboard.text;
                chatHistory.AddMessage(currMessage);
            if (updateManager != null) {
                messageList = Serialize (currMessage);
                updateManager.SendTextChat (messageList);
            }
                keyboard = null;
            }
        }

        // Renders the chat onto the player's screen
        void OnGUI () {
            chatGUI.RenderChatGUI ();
        }

        // Takes a message and adds it to the chat history
        public void GiveMessage(List<byte> message) {
            string strMessage = Deserialize (message);
            chatHistory.AddMessage(strMessage);
        }

        // Takes a string of bytes and converts it to the appropriate string representations
        private string Deserialize(List<byte> message) {
            return Encoding.UTF8.GetString (message.ToArray ());
        }

        // Takes a string and converts it to a list of bytes
        public List<byte> Serialize(String message) {
            List<byte> ret = new List<byte> ();
            // Turn the string into an array of bytes and that array into a list
            ret.AddRange (Encoding.UTF8.GetBytes (message));
            return ret;
        }
    }
}