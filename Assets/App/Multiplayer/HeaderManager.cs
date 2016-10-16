/*
 * Used to apply and remove headers to and from the start of an update (list of bytes)
 * describing who the update is inteded for and the protocol verion.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class HeaderManager {
		
		/// Strips off the header of an update, returns information contained in the header
		public static List<byte> StripHeader(List<byte> data) {
			List<byte> headerInfo = new List<byte>();

			// Make sure data isn't empty or null
			try {
				// get the protocol
				byte protocol = data[0];

				// add the protocol to the header and remove it from the message
				headerInfo.Add(protocol);
				data.RemoveAt(0);

				try {
					if (protocol == UpdateManager.PROTOCOL_VERSION) {
						// get the update type
						headerInfo.Add(data[0]);
						
                        // remove the update type
						data.RemoveAt(0);
					}
				} catch (System.IndexOutOfRangeException e) {
					Debug.Log("Invalid protocol version. " + e);
					throw e;
				}
			} catch(System.ArgumentOutOfRangeException e) {
				Debug.Log("data hasn't been initialized. " + e);
				throw e;
			}
			catch (System.NullReferenceException e) {
				Debug.Log("data is null. " + e);
				throw e;
			}
			return headerInfo;
		}

		/// Applies a header to an upddate (data), at this point just a protocol version and update type
		public static void ApplyHeader(List<byte> data, byte type) {
			data.Insert(0, UpdateManager.PROTOCOL_VERSION);
			data.Insert(1, type);
		}
	}
}
