﻿/*
 * Is responsible for taking text input from the user and sending it to the update manager
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class ChatController : MonoBehaviour {
		
		// the update manager which should be told about any updates
		public UpdateManager updateManager;

		// A record of every message sent
		private ChatHistory chatHistory = new ChatHistory ();

		// The class which looks after rendering the chat
		private ChatGUI chatGUI;

		// The OS keyboard
		private TouchScreenKeyboard keyboard;

		/// Use this for initialization
		void Start() {
			chatGUI = new ChatGUI(chatHistory);
		}

		/// This is called when the chat icon is pressed on screen. 
		/// It initialises the device's onscreen keyboard.
		public void GetChatInput() {
			keyboard = TouchScreenKeyboard.Open("");
		}

		/// Update is called once per frame
		void Update() {
			// If the user is done typing a message, add it to the chat history and send
			if (keyboard != null && keyboard.done) {

				SendMessage(keyboard.text);
				keyboard = null;
			}
		}
		
		/// Method to send a message.
		new public void SendMessage(string message) {
			try {
				ChatMessage m = new ChatMessage(message, MultiPlayerController.Instance.ourName);
				chatHistory.AddMessage(m);
			}
			catch (ArgumentNullException e) {
				Debug.Log (e.Message);
			}

			if (updateManager != null) {
				List<byte> messageList;

				messageList = Serialize (message);
				updateManager.SendTextChat (messageList);
			}
		}

		/// Renders the chat onto the player's screen
		void OnGUI () {
			chatGUI.RenderChatGUI ();
		}

		/// Takes a serialized message and adds it to the chat history
		public void ReceiveMessage (List<byte> message) {

			string strMessage = Deserialize (message);

			try {
				ChatMessage m = new ChatMessage(strMessage, MultiPlayerController.Instance.theirName);
				chatHistory.AddMessage(m);
			}
			catch (ArgumentNullException e) {
				Debug.Log (e.Message);
			}

		}

		/// Takes a string of bytes and converts it to the appropriate string representations
		public string Deserialize(List<byte> message) {
			return Encoding.UTF8.GetString (message.ToArray ());
		}

		/// Takes a string and converts it to a list of bytes
		public List<byte> Serialize(String message) {

			List<byte> ret = new List<byte> ();
			
			// Turn the string into an array of bytes and that array into a list
			ret.AddRange (Encoding.UTF8.GetBytes (message));

			return ret;
		}

		/// Returns the chat history
		public ChatHistory GetChatHistory() {
			return this.chatHistory;
		}
	}
}