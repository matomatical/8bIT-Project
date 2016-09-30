using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
    [TestFixture]
    public class HeaderManagerTest {

        // Make ssure the header given as an argument to HandleInput is not empty
        [Test]
        public void EmptyHeader() {
            List<byte> emptyHeader = new List<byte>();
            try {
                HeaderManager.StripHeader(emptyHeader);
            }
            catch (System.ArgumentOutOfRangeException e) {
                Assert.Pass();
            }
        }

        // Make sure that the header given as an argument to HandleInput is not null
        [Test]
        public void NullHeader() {
            List<byte> nullHeader = null;
            try {
                HeaderManager.StripHeader(nullHeader);
            }
            catch (System.NullReferenceException e) {
                Assert.Pass();
            }
        }


        // Make sure that the header contains the right protocol version
        [Test]
        public void IncorrectProtocolVersion() {
            List<byte> header = CreateHeader();
            
            // what if the header has an invalid protocol version
            try {
                HeaderManager.StripHeader(header);
            }
            catch (System.IndexOutOfRangeException e) {
                Assert.Pass();
            }

            // test it with the correct protocol version
            //header.Insert();
        }


        // See if Strip header strips the header as it should
        [Test]
        public void StripHeader() {
            List<byte> header = CreateHeader();
            header.Insert(0, UpdateManager.PROTOCOL_VERSION); //insert the valid protocol type
            header.Insert(1, UpdateManager.PLAYER); // insert any update identifier

            List<byte> expectedResult = new List<byte>();
            expectedResult.Add((byte)UpdateManager.PROTOCOL_VERSION);
            expectedResult.Add((byte)UpdateManager.PLAYER);

            List<byte> result = HeaderManager.StripHeader(header);

            
            for (int i = 0; i < result.Count; i++) {
                Debug.Log("r: " + result[i] + " er: " + expectedResult[i]);
                Assert.That(result[i].Equals(expectedResult[i]));
            }
        }
        
        // Create a meaningless header
        private List<byte> CreateHeader() {
            List<byte> header = new List<byte>();
            string message = "Mariam says hello";
            for (int i=0; i<message.Length; i++) {
                header.Add((byte)message[i]);
            }
            return header;
        }
    }
}
