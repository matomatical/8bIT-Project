/*
 * The multiplayer serializer for Dynamic game objects
 * Used for representation of the state of the object as a list of bytes 
 * and updating the state of the object from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace xyz._8bITProject.cooperace.multiplayer {
    public abstract class DynamicObjectSerializer : MonoBehaviour,  ISerializer<DynamicObjectInformation> {
       	
		// Keeps track of how long until we send an update
		private int stepsUntilSend = 0;
		private readonly int MAX_STEPS_BETWEEN_SENDS = 5;

		// Keeps track of the last update to see if anything has changed
		protected DynamicObjectInformation lastInfo = null;

		// the update manager which should be told about any updates
		public IUpdateManager updateManager;

        private byte ID;				// The unique ID of the obstacle.
        private bool IDSet = false;		// A unique ID has been assigned


		/// Assign this serialiser a unique id,
		/// synched between devices, so that it
		/// knows which updates are relevant
		public void SetID(byte id) {
			if (IDSet == false) {
				this.ID = id;
				IDSet = true;
			}
		}

		/// Get this serialiser a unique id, if it has been set
		/// (else throw NotYetSetException)
		public byte GetID(){
			if (!IDSet)
				throw new NotYetSetException ("id not yet set");

			return ID;
		}


        public List<byte> Serialize(DynamicObjectInformation information) {
            // initialize list to return
            List<byte> bytes = new List<byte>();

            // get the data to Serialize from the PlayerInformation
            float posx = information.pos.x;
            float posy = information.pos.y;
            float velx = information.vel.x;
            float vely = information.vel.y;
			float time = information.time;

            // Add the byte representation of the above values into the list
            bytes.AddRange (BitConverter.GetBytes (posx));
            bytes.AddRange (BitConverter.GetBytes (posy));
            bytes.AddRange (BitConverter.GetBytes (velx));
            bytes.AddRange (BitConverter.GetBytes (vely));
			bytes.AddRange (BitConverter.GetBytes (time));

            return bytes;
        }

        public DynamicObjectInformation Deserialize(List<byte> update) {
            float posx, posy;
            float velx, vely;
			float time;
            byte[] data = update.ToArray();

            try {
                // Get the information from the list of bytes
                posx = BitConverter.ToSingle(data, 0);
                posy = BitConverter.ToSingle(data, 4);
                velx = BitConverter.ToSingle(data, 8);
                vely = BitConverter.ToSingle(data, 12);
				time = BitConverter.ToSingle(data, 16);
            }
            catch (System.ArgumentOutOfRangeException e) {
                // something has gone wrong! not enough bytes in the message
				throw new MessageBodyException("Not enough bytes in dynamic message body:" + e.Message);
            }

            
            // Create and return DynamicObstaceleInformation with the data deserialized
            return new DynamicObjectInformation(new Vector2(posx, posy), new Vector2(velx, vely), time);
        }

        public void Notify(List<byte> message) {
            DynamicObjectInformation info = Deserialize(message);
            SetState(info);
        }

		protected abstract void SetState (DynamicObjectInformation information);


		/// Called at set intervals, used to let update manager know there is an update

		void FixedUpdate () {
			// Stores current state
			List<byte> update;

			// Stores current information about the player
			DynamicObjectInformation info;

			// Only send if there is an update manager to send to and the transform is found
			if (updateManager != null) {

				// If it's time to send another update
				if (stepsUntilSend < 1) {

					// Read information about the player currently
					info = GetState();

					// If the update is different to the last one sent
					if (!info.Equals(lastInfo)) {
						// Get the update to be sent
						update = Serialize (info);

						Debug.Log ("Serializing new update posx = " + info.pos.x + " pos y = " + info.pos.y);

						// Send the update
						Send (update);

						// reflect changes in the last update
						lastInfo = info;

						// reset the steps since an update was sent
						stepsUntilSend = MAX_STEPS_BETWEEN_SENDS;

					} else {
						Debug.Log ("Local player has not changed since last update");
					}

				}

				// show a step has been taken regardless of what happens
				stepsUntilSend -= 1;
			}
		}

		protected abstract void Send (List<byte> message);

		protected abstract DynamicObjectInformation GetState ();

    }
}
