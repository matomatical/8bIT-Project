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
    public class PushBlockSerializer : DynamicObstacleSerializer {
        // The push block controller that is used to send the updates
        private PushBlockController localPushBlockController;

        // Keeps track of how long until we send an update
        private int stepsUntilSend;
        private readonly int MAX_STEPS_BETWEEN_SENDS = 5;


        // Use this for initialization
        void Start() {
            stepsUntilSend = 0;

            // Fill out last positions with dummys
            lastInfo = null;

            // link Components
            localPushBlockController = GetComponent<PushBlockController>();
            remoteController = GetComponent<RemotePhysicsController>();
        }

        /// Called at set intervals, used to let update manager know there is an update
		void FixedUpdate() {
            // Stores current state
            List<byte> update;

            // Stores current information about the push block
            DynamicObjectInformation info;

            // Only send if there is an update manager to send to and the transform is found
            if (updateManager != null) {

                // If it's time to send another update
                if (stepsUntilSend < 1) {

                    // Read information about the push block currently
                    info = new DynamicObjectInformation(localPushBlockController.GetPosition(), 
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
		protected override void Send(List<byte> message) {
            updateManager.SendPlayerUpdate(message);
        }
    }
}
