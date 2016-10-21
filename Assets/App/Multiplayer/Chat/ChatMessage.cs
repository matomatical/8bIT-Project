/*
 * Represents a message sent between users for text chat
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System;
using xyz._8bITProject.cooperace.persistence;


#pragma warning disable 0659
namespace xyz._8bITProject.cooperace.multiplayer {

	public class ChatMessage {
		// Text to be sent
		public string message { get; private set; }
		public string gamerTag { get; private set; }

		/// Use this for initialization
		public ChatMessage(string message, string gamerTag) {
			this.message = message;
			this.gamerTag = gamerTag;
		}

		/// Check if two objects are equal
		public override bool Equals(System.Object obj) {
			// If parameter is null return false.
			if (obj == null) {
				return false;
			}

			// If parameter cannot be cast to ChatMessage return false.
			ChatMessage chat = obj as ChatMessage;
			if ((System.Object)chat == null) {
				return false;
			}

			// Return true if the fields match:
			return Equals(chat);
		}

		/// Check if two objects of type ChatMessage are equal
		public bool Equals(ChatMessage chat) {
			// Return true if the fields match:
			return (chat != null) && (chat.message == this.message);
		}
	}
}
