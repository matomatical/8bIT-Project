/*
 * Represents a message sent between users for text chat
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System;

namespace xyz._8bITProject.cooperace.multiplayer {

    public class ChatMessage : MonoBehaviour {
        // Text to be sent
        String message;

        // Use this for initialization
        public ChatMessage(string message) {
            this.message = message;
        }

        // return the message
        public string getMessage() {
            return this.message;
        }
    }
}
