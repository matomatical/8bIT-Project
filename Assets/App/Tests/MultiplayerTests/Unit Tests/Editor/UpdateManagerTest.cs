/*
 * Tests for UpdateManager
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
*/

using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
    
	[TestFixture]
    public class UpdateManagerTest {

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
            catch (MessageHeaderException e) {
				Assert.Pass(e.Message);
            }
        }

        [Test]
        public void ObstacleTypeUpdateShouldNotifyAllObstacleSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.OBSTACLE);
			um.Subscribe (listener2, UpdateManager.Channel.OBSTACLE);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.OBSTACLE), "matt");

			// all listeners should have been notified

			Assert.IsTrue (listener1.notified && listener2.notified);
        }

		[Test]
		public void PlayerTypeUpdateShouldNotifyAllPlayerSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.PLAYER);
			um.Subscribe (listener2, UpdateManager.Channel.PLAYER);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.PLAYER), "matt");

			// all listeners should have been notified

			Assert.IsTrue (listener1.notified && listener2.notified);
		}

		[Test]
		public void PushBlockTypeUpdateShouldNotifyAllPushBlockSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.PUSHBLOCK);
			um.Subscribe (listener2, UpdateManager.Channel.PUSHBLOCK);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.PUSHBLOCK), "matt");

			// all listeners should have been notified

			Assert.IsTrue (listener1.notified && listener2.notified);
		}

		[Test]
		public void ObstacleTypeUpdateShouldNotNotifyNonObstacleSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.PLAYER);
			um.Subscribe (listener2, UpdateManager.Channel.PUSHBLOCK);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.OBSTACLE), "matt");

			// all non-listeners should not have been notified

			Assert.IsFalse (listener1.notified && listener2.notified);
		}

		[Test]
		public void PlayerTypeUpdateShouldNotNotifyNonPlayerSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.OBSTACLE);
			um.Subscribe (listener2, UpdateManager.Channel.PUSHBLOCK);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.PLAYER), "matt");

			// all non-listeners should not have been notified

			Assert.IsFalse (listener1.notified && listener2.notified);
		}

		[Test]
		public void PushBlockTypeUpdateShouldNotNotifyNonPushBlockSubscribers() {
			UpdateManager um = new UpdateManager();

			MockListener listener1 = new MockListener ();
			MockListener listener2 = new MockListener ();

			um.Subscribe (listener1, UpdateManager.Channel.PLAYER);
			um.Subscribe (listener2, UpdateManager.Channel.OBSTACLE);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.PUSHBLOCK), "matt");

			// all non-listeners should not have been notified

			Assert.IsFalse (listener1.notified && listener2.notified);
		}

		[Test]
		public void NotificationsShouldBeSentOutWithTheCorrectData(){
			UpdateManager um = new UpdateManager();

			MockListener listener = new MockListener ();

			um.Subscribe (listener, UpdateManager.Channel.PLAYER);

			um.HandleUpdate (CreateValidUpdate (UpdateManager.PLAYER), "matt");

			// listener should have been notified with the sample data

			CollectionAssert.AreEqual(Encoding.UTF8.GetBytes ("Hello, World!"), listener.data);
		}



		// test helper methods



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

		/// create meaninful update with header and test data 'Hello, World!'
		private List<byte> CreateValidUpdate(byte type) {
			List<byte> update = new List<byte> ();

			update.Add(UpdateManager.PROTOCOL_VERSION);
			update.Add(type);
			update.AddRange(Encoding.UTF8.GetBytes ("Hello, World!"));

			return update;
		}
    }





	/// This class behaves as a minimal listener which can be used to test
	/// the observer pattern methods in update manager
	/// The notified field is false until someone calls the Notify method,
	/// which will set it to true (and ignore the notification data)
	class MockListener : IListener<List<byte>> {
		public bool notified = false;
		public List<byte> data;
		public void Notify(List<byte> data){
			notified = true;
			this.data = data;
		}
	}
}
