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
        public void InvalidProtocolVersion() {
            UpdateManager um = new UpdateManager();
            List<byte> update = CreateInvalidUpdate();

            try {
                um.HandleUpdate(update, "mariam");
            } catch(Exception e) {
                Debug.Log(e);
                Assert.Pass();
            }

        }
        
        // what if the update identifier is invalid?
        [Test]
        public void InvalidUpdateIdentifier() {
            UpdateManager um = new UpdateManager();

            // Incorrect protocol version and invalid update identifier
            List<byte> update1 = CreateInvalidUpdate();

            // Correct protocol version but invalid update identifier
            List<byte> update2 = new List<byte>();
            update2 = (CreateInvalidUpdate());
            update2[0] = (UpdateManager.PROTOCOL_VERSION);

            try {
                um.HandleUpdate(update1, "mariam");
                um.HandleUpdate(update2, "mariam");
            }
            catch (Exception e) {
                Debug.Log(e);
                Assert.Pass();
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
        private List<byte> CreateInvalidUpdate() {
            List<byte> update = new List<byte>();
            update.Add((Byte)20);
            update.Add((Byte)'m');
            update.Add((Byte)27.147);
            return update;
        }

    }
}
