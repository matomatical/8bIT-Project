using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * The multiplayer serializer for Push block game objects
 * Used for representation of the state of the object as a list of bytes 
 * and updating the state of the object from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

namespace xyz._8bITProject.cooperace.multiplayer {
    public class PushBlockSerializer : MonoBehaviour,  ISerializer<DynamicObstacleInformation> {
        // The push block controller that is used to send the updates
        private PushBlockController localPushBlockController;

        // The push block controller that is used to recieve the updates
        private RemotePhysicsController remotePushBlockController;

        // the update manager which should be told about any updates
        public IUpdateManager updateManager;

        // Keeps track of how long until we send an update
        private int stepsUntilSend;
        private readonly int MAX_STEPS_BETWEEN_SENDS = 5;

        // Keeps track of the last update to see if anything has changed
        private DynamicObstacleInformation lastInfo;

        // Use this for initialization
        void Start() {
            stepsUntilSend = 0;

            // Fill out last positions with dummys
            lastInfo = null;

            // link Components
            localPushBlockController = GetComponent<PushBlockController>();
            remotePushBlockController = GetComponent<RemotePhysicsController>();
        }

        /// Called at set intervals, used to let update manager know there is an update
		void FixedUpdate() {
            // Stores current state
            List<byte> update;

            // Stores current information about the push block
            DynamicObstacleInformation info;

            // Only send if there is an update manager to send to and the transform is found
            if (updateManager != null) {

                // If it's time to send another update
                if (stepsUntilSend < 1) {

                    // Read information about the push block currently
                    info = new DynamicObstacleInformation(localPushBlockController.GetPosition(), 
                                                                localPushBlockController.GetVelocity());

                    // If the update is different to the last one sent
                    if (!info.Equals(lastInfo)) {
                        Debug.Log("Serializing push block");

                        // Get the update to be sent
                        update = Serialize(info);


                        // Send the update
                        Send(update);

                        // reflect changes in the last update
                        lastInfo = info;

                        // reset the steps since an update was sent
                        stepsUntilSend = MAX_STEPS_BETWEEN_SENDS;

                    }
                    else {
                        Debug.Log("push block has not changed since last update");
                    }

                }

                // show a step has been taken regardless of what happens
                stepsUntilSend -= 1;
            }
        }

        /// Let updateManager know there is an update
		private void Send(List<byte> message) {
            updateManager.SendPlayerUpdate(message);
        }

        /// Applies information in info to the player this serializer is attatched to
		private void Apply(DynamicObstacleInformation info) {
            remotePushBlockController.SetState(info.pos, info.vel);
        }

        public List<byte> Serialize(DynamicObstacleInformation information) {
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

        public DynamicObstacleInformation Deserialize(List<byte> update) {
            float posx, posy;
            float velx, vely;

            byte[] data = update.ToArray();

            // Just in case the List isn't long enough
            try {
                // Get the information from the list of bytes
                posx = BitConverter.ToSingle(data, 0);
                posy = BitConverter.ToSingle(data, 4);
                velx = BitConverter.ToSingle(data, 8);
                vely = BitConverter.ToSingle(data, 12);
            }
            catch (System.ArgumentOutOfRangeException e) {
                Debug.Log(e.Message);
                throw e;
            }

            // Create and return PlayerInformation with the data deserialized
            return new DynamicObstacleInformation(new Vector2(posx, posy), new Vector2(velx, vely));
        }

        public void Notify(List<byte> message) {
            DynamicObstacleInformation info = Deserialize(message);
            Apply(info);
        }
    }
}
