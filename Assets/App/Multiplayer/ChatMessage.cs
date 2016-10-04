/*
 * Represents a message sent between users for text chat
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System;

namespace xyz._8bITProject.cooperace.multiplayer {

    public class ChatMessage : MonoBehaviour {
        // Text to be sent
        String message;

        // did I send this message?
        bool localPlayerMsg;

        // Use this for initialization
        public ChatMessage(string message, bool localPlayerMsg) {
            this.message = message;
            this.localPlayerMsg = localPlayerMsg;
        }

        // return the message
        public string getMessage() {
            return this.message;
        }

        // See if this message was sent by local player
        public bool getLocalMsg() {
            return this.localPlayerMsg;
        }

        // set the message
        public void setMessage(string message) {
            this.message = message;
        }

        // Mark this message as having been sent by the local player
        public void setLocalPlayerMsg(bool localPlayerMsg) {
            this.localPlayerMsg = localPlayerMsg;
        }

    }
}
