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
			if (message == "") {
				throw new ArgumentException ("message is empty");
			} else if (message == null) {
				throw new ArgumentNullException ("message is null");
			} else {
				ChatMessage m = new ChatMessage (message, localPlayer);
				history.Add (m);
			}
        }

        /// Returns the n most recent messages
		/// The most recent will be at the end of the list
		public List<ChatMessage> MostRecent(int n) {
			int length = history.Count;

			if (length < n)
				n = length;
			
			return history.GetRange (length - n, n);
		}

        /// get a copy of the chat history
        public List<ChatMessage> GetHistory() {
			return new List<ChatMessage> (this.history);
        }

    }
}