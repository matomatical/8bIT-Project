/*
 * Tests for UpdateManager
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
    public class UpdateManagerTest : MonoBehaviour {

        // what if the protool version is different
        [Test]
        public void InvalidProtocolVersionShouldThrowHeaderException() {
            UpdateManager um = new UpdateManager();
            List<byte> update = CreateInvalidProtocolUpdate();

            try {
                um.HandleUpdate(update, "mariam");
				Assert.Fail ("HandleUpdate should have thrown a HeaderException");
            } catch(Exception e) {
				Assert.Pass(e.Message);
            }
        }
        
        // what if the update identifier is invalid?
        [Test]
        public void InvalidUpdateIdentifierShouldThrowHeaderException () {
            UpdateManager um = new UpdateManager();

            // Correct protocol version and invalid update identifier
            List<byte> update = CreateInvalidUpdateTypeUpdate();
			
            try {
                um.HandleUpdate(update, "mariam");
				Assert.Fail ("HandleUpdate should have thrown a HeaderException");
            }
            catch (HeaderException e) {
				Assert.Pass(e.Message);
            }
        }

        [Test]
        public void SendObstacleUpdate() {

        }

        [Test]
        public void SendPlayerUpdates() {

        }

        [Test]
        public void SendTextChat() {

        }

        [Test]
        public void Subscribe() {
        }

        // create a meaningless update
        private List<byte> CreateInvalidUpdateTypeUpdate() {
            List<byte> update = new List<byte>();

			update.Add(UpdateManager.PROTOCOL_VERSION);
            update.Add((Byte)'m');
			update.AddRange(BitConverter.GetBytes(27.147));
            return update;
        }

		// create a meaningless update
		private List<byte> CreateInvalidProtocolUpdate() {
			List<byte> update = new List<byte>();

			update.Add((byte)(UpdateManager.PROTOCOL_VERSION+1));
			update.Add(UpdateManager.OBSTACLE);
			update.AddRange(BitConverter.GetBytes(27.147));
			return update;
		}
    }
}
