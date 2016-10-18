
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
    public class DynamicObjectSerializer : MonoBehaviour,  ISerializer<DynamicObjectInformation> {
       	
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

        // The push block controller that is used to recieve the updates
        protected RemotePhysicsController remoteController;

        // the update manager which should be told about any updates
        public IUpdateManager updateManager;

        // Keeps track of the last update to see if anything has changed
        protected DynamicObjectInformation lastInfo;

        protected bool isPushBlock = false;

        public List<byte> Serialize(DynamicObjectInformation information) {
            // initialize list to return
            List<byte> bytes = new List<byte>();

            // get the data to Serialize from the PlayerInformation
            float posx = information.pos.x;
            float posy = information.pos.y;
            float velx = information.vel.x;
            float vely = information.vel.y;

            // Add the byte representation of the above values into the list
            bytes.AddRange(BitConverter.GetBytes(posx));
            bytes.AddRange(BitConverter.GetBytes(posy));
            bytes.AddRange(BitConverter.GetBytes(velx));
            bytes.AddRange(BitConverter.GetBytes(vely));

            return bytes;
        }

        public DynamicObjectInformation Deserialize(List<byte> update) {
            float posx, posy;
            float velx, vely;
            byte[] data = update.ToArray();

            try {
                // Get the information from the list of bytes
                posx = BitConverter.ToSingle(data, 0);
                posy = BitConverter.ToSingle(data, 4);
                velx = BitConverter.ToSingle(data, 8);
                vely = BitConverter.ToSingle(data, 12);
            }
            catch (System.ArgumentOutOfRangeException e) {
                // something has gone wrong! not enough bytes in the message
				throw new MessageBodyException("Not enough bytes in dynamic message body:" + e.Message);
            }
            
            
            // Create and return DynamicObstaceleInformation with the data deserialized
            return new DynamicObjectInformation(new Vector2(posx, posy), new Vector2(velx, vely));
        }

        public void Notify(List<byte> message) {
            DynamicObjectInformation info = Deserialize(message);
            Apply(info);
        }
        protected void Apply(DynamicObjectInformation information) {
            Debug.Log("pos x = " + information.pos.x + " pos y = " + information.pos.y);
            remoteController.SetState(information.pos, information.vel);
        }

        protected virtual void Send(List<byte> message) {
            // send the update to update manager
        }


    }
}
