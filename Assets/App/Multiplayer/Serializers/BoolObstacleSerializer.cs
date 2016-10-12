/*
 * The multiplayer serializer for Boolean state obstacles
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;
using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	public abstract class BoolObstacleSerializer : MonoBehaviour, ISerializer<BoolObstacleInformation> {

		public IUpdateManager updateManager;	// IUpdateManager to send updates to
		protected byte ID = 12;						// The unique ID of the obstacle.
		private bool IDSet = false;				// A unique ID has been assigned
		private bool lastState;					// The last known state of this obstacle
		private bool firstRun = true;			// Used to detect if HasChanged has been run before
		private readonly byte BITS_IN_BYTE = 8;	// The number of bits in a byte

		/// Assign this serialiser a unique id,
		/// synched between devices, so that it
		/// knows which updates are relevant
		public void SetID(byte id){
			UILogger.Log (string.Format ("Trying to set obstacle ID to {0}", id));
			if (IDSet == false) {
				UILogger.Log (string.Format ("Setting obstacle ID to {0}", id));
				this.ID = id;
				IDSet = true;
			}
		}

		void FixedUpdate () {
			if (HasChanged ()) {
				UILogger.Log (string.Format ("ObjectID: {0}, sending", ID));
				updateManager.SendObstacleUpdate (Serialize (new BoolObstacleInformation(ID, GetState ())));
			}
		}

		/// <summary>
		/// Should be implemented to return the state of the object being serialized
		/// </summary>
		/// <returns>The state of the obstacle being serialized.</returns>
		public abstract bool GetState ();

		/// <summary>
		/// Sets the state of the gameobject this is attatched to
		/// </summary>
		/// <param name="state">The state to be set</param>
		protected abstract void SetState (bool state);


		public void Notify (List<byte> message) {
			
			// Deserialize the message
			BoolObstacleInformation info = Deserialize (message);

			if (info.ID == this.ID) {
				SetState (info.state);
			}

			UILogger.Log (string.Format ("ObjectID: {0}, recieved", info.ID));

		}

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
				return false;
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
			data.Add(information.state ? (byte)(information.ID + Math.Pow(2, BITS_IN_BYTE-1)) : (byte)information.ID);

			return data;
		}

		public BoolObstacleInformation Deserialize (List<byte> data)
		{
			byte info = data [0];	// Information stored in data
			byte ID;				// ID of obstacle in the data
			bool state;				// State of the obstacle in the data

			// Deserialize
			if (info > (Math.Pow(2, BITS_IN_BYTE-1)-1)) {
				ID = (byte)(info - Math.Pow(2, BITS_IN_BYTE-1));
				state = true;
			} else {
				ID = info;
				state = false;
			}
				
			return new BoolObstacleInformation (ID, state);
		}
	}
}
