/*
 * Tests for ChatController
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

    [TestFixture]
    public class ChatControllerTest : MonoBehaviour {
        String message1 = "Hello World!";


        [Test]
        public void SerializeDeserialize() {
            ChatController serializer = new ChatController();
            // Serialize some information
            List<Byte> data = serializer.Serialize(message1);

            // Make sure the serializer isn't just spitting out the same thing
            Assert.That(!message1.Equals(data));

            // Deserialize the result
            String resultingMessage = serializer.Deserialize(data);

            // Check the information is the same as the original
            Assert.That(resultingMessage.Equals(message1));
        }

        [Test]
        public void DeserializeSerialize() {
            ChatController serializer = new ChatController();

            String info;
            List<byte> data = data1();
            List<byte> resultingData;

            // Deserialize a List
            info = serializer.Deserialize(data);

            // Make sure we didn't just get the same thing back
            Assert.That(!info.Equals(data));

            // Serialize the info
            resultingData = serializer.Serialize(info);

            // Check to see the new data is the same as the original
            for (int i = 0; i < data.Count; i++) {
                Assert.That(resultingData[i] == data[i]);
            }
            
        }

        // Test chat controller's serialize method which should take a string and convert it to
        // a list of bytes
        [Test]
        public void Serialize() {
            ChatController serializer = new ChatController();

            String info = message1;
            List<byte> expectedData = data1();
            List<byte> data = serializer.Serialize(info);

            for (int i = 0; i < data.Count; i++) {
                Assert.That(data[i] == expectedData[i]);
            }
        }

        // Test chat controllers deserialize method which should take a byte array and convert it
        // to a string
        [Test]
        public void Deserialize() {
            ChatController serializer = new ChatController();

            List<byte> data = data1();
            String info = serializer.Deserialize(data);

            Assert.That(info.Equals(message1));
        }

        // Test chat controller's give message which adds a message to the chat history
        [Test]
        public void GiveMessage() {
            ChatController serializer = new ChatController();
            serializer.Init();

            List<Byte> data = data1();
            serializer.GiveMessage(data);

            ChatHistory history = serializer.GetChatHistory();

            // Make sure that the byte stream that is converted and added to the chatHistory is the 
            // same as the original message
			bool containsMessage = false;

			foreach (ChatMessage m in history.GetHistory ()) {
				if (m.Equals (message1)) {
					containsMessage = true;
					break;
				}
			}

			Assert.IsTrue (containsMessage);

        }

        // Convert message1 into a list of bytes manually to ensure everything's working smoothly
        private List<Byte> data1() {
            List<Byte> data = new List<byte>();
            data.Add((Byte)('H'));
            data.Add((Byte)('e'));
            data.Add((Byte)('l'));
            data.Add((Byte)('l'));
            data.Add((Byte)('o'));
            data.Add((Byte)(' '));
            data.Add((Byte)('W'));
            data.Add((Byte)('o'));
            data.Add((Byte)('r'));
            data.Add((Byte)('l'));
            data.Add((Byte)('d'));
            data.Add((Byte)('!'));
            return data;
        }

    }
}
