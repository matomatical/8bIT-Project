/*
 * The multiplayer serializer for Boolean state obstacles
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	public abstract class BoolObstacleSerializer : MonoBehaviour, ISerializer<BoolObstacleInformation> {

		public IUpdateManager updateManager;	// IUpdateManager to send updates to
		public readonly byte ID;				// The unique ID of the obstacle
		private bool lastState;					// The last known state of this obstacle
		private bool firstRun = true;			// Used to detect if HasChanged has been run before
		private readonly int BITS_IN_BYTE = 8;	// The number of bits in a byte minus 1

		void FixedUpdate () {
			if (HasChanged ()) {
				updateManager.SendObstacleUpdate (Serialize (new BoolObstacleInformation(ID, GetState ())));
			}
		}

		/// <summary>
		/// Should be implemented to return the state of the object being serialized
		/// </summary>
		/// <returns>The state of the obstacle being serialized.</returns>
		public abstract bool GetState ();

		public abstract void Notify (List<byte> message);

		/// <summary>
		/// Determines whether the obstacle has changed since last being called.
		/// </summary>
		/// <returns><c>true</c> if this obstacle has changed; otherwise, <c>false</c>.</returns>
		public bool HasChanged () {

			bool ret;		// the value to be returned
			bool currState;	// the value of the current state

			// If this is the first time HasChanged is being called, there is no last state to refer to.
			if (firstRun) {
				firstRun = false;
				lastState = GetState ();
				return true;
			}

			// If the last state is the same as the current state, return true, otherwise false
			currState = GetState ();
			ret = lastState != currState;

			// Update the last seen state
			lastState = currState;

			return ret;	
		}

		public List<byte> Serialize (BoolObstacleInformation information)
		{
			List<byte> data = new List<byte> ();	// The list to store the update

			// Add information about the state to the data
			data.Add(information.state ? (byte)(information.ID + 2^(BITS_IN_BYTE-1)) : (byte)information.ID);

			return data;
		}

		public BoolObstacleInformation Deserialize (List<byte> data)
		{
			byte info = data [0];	// Information stored in data
			byte ID;				// ID of obstacle in the data
			bool state;				// State of the obstacle in the data

			// Deserialize
			if (info > (2^(BITS_IN_BYTE-1)-1)) {
				ID = (byte)(info - 2^(BITS_IN_BYTE-1));
				state = true;
			} else {
				ID = info;
				state = false;
			}
				
			return new BoolObstacleInformation (ID, state);
		}
	}
}
