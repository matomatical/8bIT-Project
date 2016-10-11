/*
 * The multiplayer serializer for KeyBlock game objects
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace xyz._8bITProject.cooperace.multiplayer
{
	[RequireComponent (typeof (KeyBlock))]
	public class KeyBlockSerializer : BoolObstacleSerializer
	{
		/// the key block to track
		private KeyBlock block;

		void Start(){

			// link components

			block = GetComponent<KeyBlock>();
		}

		/// get the actual state of the key block being tracked this serilizer
		public override bool GetState(){
			return block.IsOpen();
		}

		/// When notified, update the key this script is associated with
		public override void Notify (List<byte> message) {

			// Deserialize the message
			BoolObstacleInformation info = Deserialize (message);

			// Act on the message
			if (info.ID == this.ID) {
				if (info.state) {
					block.Open ();
				} else {
					block.Close ();
				}
			}
		}
	}
}

