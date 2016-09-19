using UnityEngine;
using System.Collections;
using System;

/*
 * Represents a message sent between users for text chat
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

namespace _8bITProject.cooperace.multiplayer {

    public class ChatMessage : MonoBehaviour {
        // Text to be sent
        String message;

        public ChatMessage(string message) {
            this.message = message;
        }

        public string getMessage() {
            return this.message;
        }
    }
}
