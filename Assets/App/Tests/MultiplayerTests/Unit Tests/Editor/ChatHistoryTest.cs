/*
 * Tests for ChatHistory
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

    [TestFixture]
    public class ChatHistoryTest {
        string message = "Hello World!";
        

        // Test that when you try to add a message to the chathistory, you are able to
        [Test]
        public void AddMessage() {
            ChatHistory history = new ChatHistory();

            history.AddMessage(message,true);

			bool containsMessage = false;

			foreach (ChatMessage m in history.GetHistory ()) {
				if (m.Equals (message)) {
					containsMessage = true;
					break;
				}
			}

			Assert.IsTrue (containsMessage);
        }

        
        // Make sure that mostRecent returns the sfirst n number of messages
        [Test]
        public void MostRecent() {
            ChatHistory history = new ChatHistory();

            history.AddMessage("0", true); // first
            history.AddMessage("1", true); // second
            history.AddMessage("2", true); // third
            history.AddMessage("3", true); // fourth

            List<ChatMessage> recentMessages = history.MostRecent(3);

			Assert.That (recentMessages.Count == 3);
			Assert.That (recentMessages[0].Equals(new ChatMessage("1", true)));
			Assert.That (recentMessages[1].Equals(new ChatMessage("2", true)));
			Assert.That (recentMessages[2].Equals(new ChatMessage("3", true)));
        }

        // Add a message to the chatHistory and make sure true is returned when seeing if the history contains
        // the message added
        [Test]
        public void ContainsMessage() {
            ChatHistory history = new ChatHistory();
            int numMessages = 10;

            // generate 10 random messages and add them to 
            List<ChatMessage> messages = generateRandomMessages(numMessages);
			for (int i = 0; i < messages.Count; i++) {
				history.AddMessage (messages [i].message, messages [i].localPlayerMsg);
			}

			// For every test messages search the chat history to make sure each test message is in there
			foreach (ChatMessage testMessage in messages) {
				bool containsMessage = false;

				foreach (ChatMessage m in history.GetHistory ()) {
					if (m.Equals (testMessage)) {
						containsMessage = true;
						break;
					}
				}

				Assert.IsTrue (containsMessage);
            }
        }

        // test to ensure you can't add any empty messages to the history
        [Test]
        public void AddEmptyMessage() {
            ChatHistory history = new ChatHistory();

            try {
                history.AddMessage("", true);
            }
            catch (System.ArgumentNullException e) {
                Debug.Log(e);
                Assert.Pass();
            }

        }

        // generate a list of random messages of random length
        private List<ChatMessage> generateRandomMessages(int num) {
            List<ChatMessage> messages = new List<ChatMessage>();
            System.Random random = new System.Random();

            for (int i=0; i< num; i++) {
                int len = random.Next(1, 140);
                ChatMessage m = new ChatMessage(RandomString(len), true);
                messages.Add(m);
            }

            return messages;
        }

        // generate a random string of the specified length
        public static string RandomString(int length) {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            System.Random random = new System.Random();
            
            for (int i = 0; i < stringChars.Length; i++) {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalString = new System.String(stringChars);

            return finalString;
        }
    }
}
