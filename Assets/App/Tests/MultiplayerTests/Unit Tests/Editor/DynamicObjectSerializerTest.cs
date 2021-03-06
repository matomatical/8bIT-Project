﻿/*
 * Tests for PlayerSerializer
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.multiplayer.tests
{
	[TestFixture]
	public class DynamicObjectSerializerTest
	{


		// testing object
		DynamicObjectSerializer serializer;

		// call this before every test to set up the testing object
		[SetUp]
		public void SetUp () {
			GameObject obj = new GameObject ("Serializer");
			serializer = obj.AddComponent<MockDynamicObjectSerializer> ();
		}

		// call this after every test to clean up testing object
		[TearDown]
		public void TearDown () {
			GameObject.DestroyImmediate (serializer.gameObject);
		}



		// TESTS

		[Test]
		public void SerializeThenDeserializeShouldPreserveOriginal () {

			// Serialize some information
			DynamicObjectInformation info = PlayerInformation1 ();
			List<byte> data = serializer.Serialize (info);

			// Make sure the serializer isn't just spitting out the same thing
			Assert.That(!info.Equals(data));

			// Deserialize the result
			DynamicObjectInformation resultingInfo = serializer.Deserialize (data);

			// Check the information is the same as the original
			Assert.That(resultingInfo.Equals(info));
		}

		[Test]
		public void DeserializeThenSerializeShouldPreserveOriginal () {

			DynamicObjectInformation info;
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

			DynamicObjectInformation info = PlayerInformation2 ();
			List<byte> data = serializer.Serialize (info);
			List<byte> expectedData = data2 ();

			for (int i=0; i<data.Count; i++) {
				Assert.AreEqual(data [i], expectedData [i]);
			}
		}

		[Test]
		public void DeserializeTestCaseShouldReturnExpected () {

			List<byte> data = data1 ();
			DynamicObjectInformation info = serializer.Deserialize (data);
			DynamicObjectInformation expectedInfo = PlayerInformation1 ();
			Assert.That (info.Equals (expectedInfo));
		}

		[Test]
		public void DeserializeEmptyListShouldThrowMessageBodyException () {

			// try deserialize an empty list
			try {
				serializer.Deserialize (new List<byte> ());
				// Oh no! Deserialize shouldn't be able to deseralize an empty list...
				Assert.Fail ();
			}
			catch (MessageBodyException e) {
				// Good! We can't deserialize that!
				Assert.Pass ();
				Debug.Log(e);

			}
		}

		[Test]
		public void SetIDFirstTimeShouldSetID () {
			serializer.SetID (12);
			Assert.AreEqual (12, serializer.GetID ());
		}

		[Test]
		public void SetIDSecondTimeShouldNotSetID () {
			serializer.SetID (12);
			serializer.SetID (24);
			Assert.AreEqual (12, serializer.GetID ());
		}

		[Test]
		public void GetIDBeforeStateSetShouldThrowNotYetSetException () {
			try {
				serializer.GetID ();
				Assert.Fail ("NotYetSetException should have been thrown");
			} catch (NotYetSetException e) {
				Assert.Pass (e.Message);
			}
		}


		// test helper methods and example data

		private readonly float POSX1 = 34.351f;
		private readonly float POSY1 = 91.984f;
		private readonly float VELX1 = 0.415f;
		private readonly float VELY1 = 0.13f;
		private readonly float TIME1 = 26.2f;

		private readonly float POSX2 = 74285.13f;
		private readonly float POSY2 = -143570108.57f;
		private readonly float VELX2 = 14958.848f;
		private readonly float VELY2 = 24058.4209f;
		private readonly float TIME2 = 245285.1445f;


		private DynamicObjectInformation PlayerInformation1 () {
			return new DynamicObjectInformation (new Vector2 (POSX1, POSY1), new Vector2 (VELX1, VELY1), TIME1);
		}

		private DynamicObjectInformation PlayerInformation2 () {
			return new DynamicObjectInformation (new Vector2 (POSX2, POSY2), new Vector2 (VELX2, VELY2), TIME2);
		}

		private List<byte> data1 () {
			List<byte> data =  new List<byte> ();
			// add each individual byte into the list
			data.Add(FloatGetByte(POSX1, 0));
			data.Add(FloatGetByte(POSX1, 1));
			data.Add(FloatGetByte(POSX1, 2));
			data.Add(FloatGetByte(POSX1, 3));

			data.Add(FloatGetByte(POSY1, 0));
			data.Add(FloatGetByte(POSY1, 1));
			data.Add(FloatGetByte(POSY1, 2));
			data.Add(FloatGetByte(POSY1, 3));

			data.Add(FloatGetByte(VELX1, 0));
			data.Add(FloatGetByte(VELX1, 1));
			data.Add(FloatGetByte(VELX1, 2));
			data.Add(FloatGetByte(VELX1, 3));

			data.Add(FloatGetByte(VELY1, 0));
			data.Add(FloatGetByte(VELY1, 1));
			data.Add(FloatGetByte(VELY1, 2));
			data.Add(FloatGetByte(VELY1, 3));

			data.Add(FloatGetByte (TIME1, 0));
			data.Add(FloatGetByte (TIME1, 1));
			data.Add(FloatGetByte (TIME1, 2));
			data.Add(FloatGetByte (TIME1, 3));


			return data;
		}

		private List<byte> data2 () {
			List<byte> data = new List<byte> ();
			// add each individual byte into the list
			data.Add(FloatGetByte(POSX2, 0));
			data.Add(FloatGetByte(POSX2, 1));
			data.Add(FloatGetByte(POSX2, 2));
			data.Add(FloatGetByte(POSX2, 3));

			data.Add(FloatGetByte(POSY2, 0));
			data.Add(FloatGetByte(POSY2, 1));
			data.Add(FloatGetByte(POSY2, 2));
			data.Add(FloatGetByte(POSY2, 3));

			data.Add(FloatGetByte(VELX2, 0));
			data.Add(FloatGetByte(VELX2, 1));
			data.Add(FloatGetByte(VELX2, 2));
			data.Add(FloatGetByte(VELX2, 3));

			data.Add(FloatGetByte(VELY2, 0));
			data.Add(FloatGetByte(VELY2, 1));
			data.Add(FloatGetByte(VELY2, 2));
			data.Add(FloatGetByte(VELY2, 3));

			data.Add(FloatGetByte (TIME2, 0));
			data.Add(FloatGetByte (TIME2, 1));
			data.Add(FloatGetByte (TIME2, 2));
			data.Add(FloatGetByte (TIME2, 3));

			return data;
		}

		// returns the nth byte in the float
		private byte FloatGetByte (float x, int n) {
			byte[] bytes = BitConverter.GetBytes (x);
			return bytes [n];
		}
	}
}
