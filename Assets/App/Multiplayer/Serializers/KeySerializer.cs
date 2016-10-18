/*
 * The multiplayer serializer for Key game objects
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;
using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer
{
	[RequireComponent (typeof (Key))]
	public class KeySerializer : BoolObstacleSerializer
	{
		/// the key to track
		private Key key;

		void Start(){

			// link components

			key = GetComponent<Key>();
		}

		public override bool GetState () {
			return key.IsTaken();
		}

		// When notified, update the key this script is associated with
		protected override void SetState (bool state) {

			if (state) {
				key.Pickup ();
			} else {
				key.Restore ();
			}
		}
	}
}

