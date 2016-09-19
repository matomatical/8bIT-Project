using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
/*
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */
namespace _8bITProject.cooperace.multiplayer {
    public class ChatController : MonoBehaviour {
        
        // the update manager which should be told about any updates
        public IUpdateManager updateManager;
        public int FONT_SIZE = 80;
        //public float MSG_X = 0.4f;
        //public float MSG_Y = Screen.width;
        //public float MSG_Y_OFFSET = 30;


        public ChatHistory chatHistory;
        private String currMessage;
        private TouchScreenKeyboard keyboard;


        public void SendChatMessage() {
            keyboard = TouchScreenKeyboard.Open("Enter message here");
        }

        void OnGUI() {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = FONT_SIZE;
            int y_offset = 20;
            int ypos = 45-y_offset;

            List<ChatMessage> recentMessages = chatHistory.MostRecent();
            //float ypos = MSG_Y-MSG_Y_OFFSET;

            for (int i = 0; i< recentMessages.Count; i++) {
                GUI.Label(new Rect(Screen.height*0.3f, ypos+=y_offset, Screen.width, Screen.height), recentMessages[i].getMessage(), myStyle);
            }

            //GUI.Label(new Rect(Screen.height*0.3f, 45, Screen.width, Screen.height), "hello", myStyle);
            //GUI.Label(new Rect(Screen.height * 0.3f, 75, Screen.width, Screen.height), "hello", myStyle);
        }

        // Use this for initialization
        void Start() {
            chatHistory = new ChatHistory();
            currMessage = string.Empty;
        }

        // Update is called once per frame
        void Update() {

            if (keyboard!=null && keyboard.done) {

                currMessage = keyboard.text;
                chatHistory.AddMessage(currMessage);
                keyboard = null;
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
