/*
 * Contains all the messages sent by players.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
    public class ChatHistory {
        // A record of every single message sent
        private List<ChatMessage> history;

        /// Use this for initialization
        public ChatHistory() {
            this.history = new List<ChatMessage>();
        }

        /// Add a new message to the chat history
        public void AddMessage(string message, bool localPlayer) {
            try {
                ChatMessage m = new ChatMessage(message, localPlayer);
                history.Add(m);
            } catch (System.ArgumentNullException e) {
                Debug.Log(e);
                throw e;
            }
            
        }

        /// Returns the most n recent messages
        public List<ChatMessage> MostRecent(int n) {
            List<ChatMessage> mostRecent = new List<ChatMessage>();

            for (int i = 0; i < history.Count; i++) {
                if (i >= history.Count - n) {
                    mostRecent.Add(history[i]);
                }
            }

            return mostRecent;
        }

        // These methods below are used for testing purposes

        /// Checks to see if the chat history contains a particular message
        public bool ContainsMessage(String m) {
            for (int i=0; i<this.history.Count; i++) {
                if (history[i].getMessage() == m) {
                    return true;
                }
            }
            return false;
        }

        /// Set the history
        public void SetHistory(List<ChatMessage> history) {
            this.history = history;
        }

        /// get the chat history
        public List<ChatMessage> GetHistory() {
            return this.history;
        }

    }
}