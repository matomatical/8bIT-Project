/*
 * Tests for HeaderManager
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
    public class HeaderManagerTest {

		[Test]
		public void StrippingEmptyDataListShouldThrowException(){
			List<byte> emptyHeader = new List<byte>();

			// empty header doesn't have enough bytes, strip should fail

			try {
				HeaderManager.StripHeader(emptyHeader);
				Assert.Fail();
			}
			catch (MessageHeaderException e){
				Assert.Pass (e.Message);
			}
		}

		[Test]
		public void StrippingShortDataListShouldThrowException(){
			List<byte> shortHeader = new List<byte>();
			shortHeader.Add ((byte)1);

			// short header doesn't have enough bytes, strip should fail

			try {
				HeaderManager.StripHeader(shortHeader);
				Assert.Fail();
			}
			catch (MessageHeaderException e){
				Assert.Pass (e.Message);
			}
		}

		[Test]
		public void StripHeaderShouldReturnCorrectHeaderStruct(){

			// create a data list with enough bytes

			List<byte> data = new List<byte> ();
			data.Insert(0, UpdateManager.PROTOCOL_VERSION); //insert the valid protocol type
			data.Insert(1, UpdateManager.PLAYER); // insert any update identifier


			// strip the header into a struct

			HeaderManager.Header result = HeaderManager.StripHeader(data);


			// shold return the right header struct for these bytes
			HeaderManager.Header expected =
				new HeaderManager.Header (UpdateManager.PROTOCOL_VERSION, UpdateManager.PLAYER);
			
			Assert.AreEqual (expected, result);
		}

		[Test]
		public void StripHeaderShouldLookAtFirstTwoBytes(){

			// create a data list with enough bytes

			List<byte> data = new List<byte> ();
			data.Insert(0, (byte)0); // this byte should end up being the protocol
			data.Insert(1, (byte)1); // this byte should end up being the type
			data.Insert(2, (byte)2); // this byte should not end up in the header


			// strip the header into a struct

			HeaderManager.Header result = HeaderManager.StripHeader(data);


			// shold return the right header struct for these bytes

			HeaderManager.Header expected = new HeaderManager.Header(0, 1);

			Assert.AreEqual (expected, result);
		}

		[Test]
		public void StripHeaderShouldRemoveFirstTwoBytes(){

			// create a data list with enough bytes

			List<byte> data = new List<byte> ();
			data.Insert(0, (byte)0); // this byte should end up being the protocol
			data.Insert(1, (byte)1); // this byte should end up being the type
			data.Insert(2, (byte)2); // this byte should not end up in the header


			// strip the header into a struct

			HeaderManager.StripHeader(data);


			// data list should now not contain those bytes

			List<byte> expected = new List<byte> ();
			expected.Add((byte)2);

			CollectionAssert.AreEqual (expected, data);
		}


		[Test]
		public void StripHeaderShouldRemoveTwoBytes(){

			// create a data list with enough bytes

			List<byte> data = new List<byte> ();
			data.Insert(0, (byte)0); // this byte should end up being the protocol
			data.Insert(1, (byte)1); // this byte should end up being the type
			data.Insert(2, (byte)2); // this byte should not end up in the header


			// strip the header into a struct

			HeaderManager.StripHeader(data);

			// data list should now now contain those bytes

			Assert.AreEqual (1, data.Count);
		}
    }
}
