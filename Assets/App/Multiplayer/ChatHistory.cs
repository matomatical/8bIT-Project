/*
 * Contains all the messages sent by players.
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer {
    public class ChatHistory : MonoBehaviour {
        // Display the n most recent messages 
        public int TOPN = 3;

        // A record of every single message sent
        private List<ChatMessage> history;

        // Use this for initialization
        public ChatHistory() {
            this.history = new List<ChatMessage>();
        }

        // Add a new message to the chat history
        public void AddMessage(string message) {
            ChatMessage m = new ChatMessage(message);
            history.Add(m);
        }

        // Returns the most n recent messages
        // NOTE : Assumes that the list is sorted in ascending order. This may not always be the case
        // when sending messages between devices
        public List<ChatMessage> MostRecent() {
            List<ChatMessage> mostRecent = new List<ChatMessage>();

            for (int i = 0; i < history.Count; i++) {
                if (i >= history.Count - TOPN) {
                    mostRecent.Add(history[i]);
                }
            }

            return mostRecent;
        }


    }
}
