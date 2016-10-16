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

		public static readonly int MAX_MESSAGES_STORED = 25;
        // A record of every single message sent
        private LinkedList<ChatMessage> history;

        /// Use this for initialization
        public ChatHistory() {
            this.history = new LinkedList<ChatMessage>();
        }

        /// Add a new message to the chat history
        public void AddMessage(string message, bool localPlayer) {
			if (message == "") {
				throw new ArgumentException ("message is empty");
			} else if (message == null) {
				throw new ArgumentNullException ("message is null");
			} else {
				ChatMessage m = new ChatMessage (message, localPlayer);
				history.AddFirst (m);

				if (history.Count >= MAX_MESSAGES_STORED) {
					history.RemoveLast ();
				}
			}
        }

        /// Returns the n most recent messages
		/// The most recent will be at the end of the list
		public List<ChatMessage> MostRecent(int n) {
			int i = 0;
			List<ChatMessage> recent = new List<ChatMessage> ();

			foreach (ChatMessage m in history) {
				if (++i > n)
					break;
				Debug.Log ("i: " + i);
				Debug.Log (m.message);
				recent.Insert(0, m);
			}

			return recent;
		}

        /// get a copy of the chat history
        public List<ChatMessage> GetHistory() {
			return new List<ChatMessage> (this.history);
        }

    }
}