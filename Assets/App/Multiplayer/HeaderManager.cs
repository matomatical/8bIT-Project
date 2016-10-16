/*
 * Used to apply and remove headers to and from the start of an update (list of bytes)
 * describing who the update is inteded for and the protocol verion.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class HeaderManager {

		public struct Header {
			public byte protocol;
			public byte messageType;

			public Header (byte protocol, byte messageType) {
				this.protocol = protocol;
				this.messageType = messageType;
			}
		}

		/// Strips off the header of an update, returns information contained in the header
		public static Header StripHeader(List<byte> data) {

			byte protocol;
			byte updateType;

			try {
				// get the protocol and remove it from the message
				protocol = data[0];
				data.RemoveAt (0);

				// get the update type and remove it from the message
				updateType = data[0];
				data.RemoveAt (0);
			
			// Make sure data isn't empty
			} catch(System.ArgumentOutOfRangeException e) {

				throw new MessageHeaderException ("not enough bytes in message: " + e.Message);
			}

			return new Header (protocol, updateType);
		}

		/// Applies a header to an upddate (data), at this point just a protocol version and update type
		public static void ApplyHeader(List<byte> data, Header header) {
			data.Insert(0, header.protocol);
			data.Insert(1, header.messageType);
		}
	}
}
