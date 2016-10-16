/*
 * Tests for KeySerializer
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
	public class KeySerializerTest {

		private byte _128 = 128;

		// Test data
		private BoolObstacleInformation BoolObstacleInformation1 = new BoolObstacleInformation (94, false);
		private BoolObstacleInformation BoolObstacleInformation2 = new BoolObstacleInformation (111, true);
		//private BoolObstacleInformation BoolObstacleInformation3 = new BoolObstacleInformation (128, true);

		private List<byte> data1 () {
			List<byte> data = new List<byte> ();
			data.Add ((byte)94);
			return data;
		}

		private List<byte> data2 () {
			List<byte> data = new List<byte> ();
			data.Add ((byte)(111 + _128));
			return data;
		}

		private List<byte> data3 () {
			List<byte> data = new List<byte> ();
			data.Add ((byte)(_128 + _128));
			return data;
		}

		[Test]
		public void SerializeThenDeserializeShouldPreserveOriginal () {
			KeySerializer serializer = new KeySerializer ();

			// Serialize some information
			BoolObstacleInformation info = BoolObstacleInformation1;
			List<byte> data = serializer.Serialize (info);

			// Make sure the serializer isn't just spitting out the same thing
			Assert.That(!info.Equals(data));

			// Deserialize the result
			BoolObstacleInformation resultingInfo = serializer.Deserialize (data);

			// Check the information is the same as the original
			Assert.That(resultingInfo.Equals(info));
		}

		[Test]
		public void DeserializeThenSerializeShouldPreserveOriginal () {
			KeySerializer serializer = new KeySerializer ();

			BoolObstacleInformation info;
			List<byte> data = data2 ();
			List<byte> resultingData;

			// Deserialize a List
			info = serializer.Deserialize(data);

			// Make sure we didn't just get the same thing back
			Assert.That(!info.Equals(data));

			// Serialize the info
			resultingData = serializer.Serialize(info);

			// Check to see the new data is the same as the original
			for (int i=0; i<data.Count; i++) {
				Assert.AreEqual(resultingData [i], data [i]);
			}
		}

		[Test]
		public void SerializeTestCaseShouldReturnExpected () {
			KeySerializer serializer = new KeySerializer ();

			BoolObstacleInformation info = BoolObstacleInformation2;
			List<byte> data = serializer.Serialize (info);
			List<byte> expectedData = data2 ();

			for (int i=0; i<data.Count; i++) {
				Assert.AreEqual(data [i], expectedData [i]);
			}
		}

		[Test]
		public void DeserializeTestCaseShouldReturnExpected () {
			KeySerializer serializer = new KeySerializer ();

			List<byte> data = data1 ();
			BoolObstacleInformation info = serializer.Deserialize (data);
			BoolObstacleInformation expectedInfo = BoolObstacleInformation1;
			Assert.That (info.Equals (expectedInfo));
		}

		[Test]
		public void DeserializeEmptyListShouldThrowArgumentOutOfRangeException () {
			KeySerializer serializer = new KeySerializer ();

			// try deserialize an empty list
			try {
				serializer.Deserialize (new List<byte> ());
				// Oh no! Deserialize shouldn't be able to deseralize an empty list...
				Assert.Fail ();

			}
			catch (System.ArgumentOutOfRangeException e) {
                // Good! We can't deserialize that!
				Assert.Pass ();
				Debug.Log(e);
			}
		}
	}
}