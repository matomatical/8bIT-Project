/*
 * Tests for ChatHistory
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

    [TestFixture]
    public class ChatHistoryTest {
        string message = "Hello World!";
        

        // Test that when you try to add a message to the chathistory, you are able to
        [Test]
        public void AddedMessagesShouldBeInHistory () {
            ChatHistory history = new ChatHistory();

            history.AddMessage(message);

			bool containsMessage = ContainsMessage(history, new ChatMessage(message));

			Assert.IsTrue (containsMessage);
        }

        
        // Make sure that mostRecent returns the sfirst n number of messages
        [Test]
        public void MostRecentThreeShouldReturnThreeMostRecentMessages () {
            ChatHistory history = new ChatHistory();

            history.AddMessage("0"); // first
            history.AddMessage("1"); // second
            history.AddMessage("2"); // third
            history.AddMessage("3"); // fourth

            List<ChatMessage> recentMessages = history.MostRecent(3);

			Assert.That (recentMessages[0].Equals(new ChatMessage("1")));
			Assert.That (recentMessages[1].Equals(new ChatMessage("2")));
			Assert.That (recentMessages[2].Equals(new ChatMessage("3")));
        }

		[Test]
		public void MostRecentThreeShouldReturnThreeMessages () {
			ChatHistory history = new ChatHistory();

			history.AddMessage("0"); // first
			history.AddMessage("1"); // second
			history.AddMessage("2"); // third
			history.AddMessage("3"); // fourth

			List<ChatMessage> recentMessages = history.MostRecent(3);

			Assert.AreEqual (3, recentMessages.Count);
		}

		[Test]
		public void MostRecentShouldNotReturnMoreMessagesThanItHas () {
			ChatHistory history = new ChatHistory();

			history.AddMessage("0"); // first

			List<ChatMessage> recents = history.MostRecent (3);

			Assert.AreEqual (recents.Count, 1);
		}

        // test to ensure you can't add any empty messages to the history
        [Test]
        public void AddEmptyMessageThrowsArgumentException() {
            ChatHistory history = new ChatHistory();

            try {
                history.AddMessage("");
				Assert.Fail ("didn't throw ArgumentException when adding a message with an empty body");
            }
            catch (System.ArgumentException e) {
				Assert.Pass(e.Message);
            }
        }

		[Test]
		public void MostRecentZeroShouldReturnEmptyList () {
			ChatHistory history = new ChatHistory ();

			history.AddMessage (message);

			Assert.That (history.MostRecent (0).Count == 0);
		}

		[Test] public void MostRecentNegativeShouldReturnEmptyList () {
			ChatHistory history = new ChatHistory ();

			history.AddMessage (message);

			Assert.That (history.MostRecent (0).Count == 0);

		}

		[Test] public void AddNullMessageThrowsArgumentNullException () {
			ChatHistory history = new ChatHistory ();

			try {
				history.AddMessage (null);
				Assert.Fail ("didn't throw ArugmentNullException when adding a message with null body");
			}
			catch (ArgumentException e) {
				Assert.Pass (e.Message);
			}
		}

        // generate a list of random messages of random length
        private List<ChatMessage> generateRandomMessages(int num) {
            List<ChatMessage> messages = new List<ChatMessage>();
            System.Random random = new System.Random();

            for (int i=0; i< num; i++) {
                int len = random.Next(1, 140);
                ChatMessage m = new ChatMessage(RandomString(len));
                messages.Add(m);
            }

            return messages;
        }

        // generate a random string of the specified length
        private string RandomString(int length) {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            System.Random random = new System.Random();
            
            for (int i = 0; i < stringChars.Length; i++) {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalString = new System.String(stringChars);

            return finalString;
        }

		public static bool ContainsMessage (ChatHistory history, ChatMessage message)
		{
			bool containsMessage = false;
			foreach (ChatMessage m in history.GetHistory ()) {
				if (m.Equals (message)) {
					containsMessage = true;
					break;
				}
			}
			return containsMessage;
		}
    }
}
