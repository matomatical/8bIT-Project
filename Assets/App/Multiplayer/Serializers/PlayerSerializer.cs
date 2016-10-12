/*
 * The multiplayer serializer for Player game objects (includes the partner)
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
	public class PlayerSerializer : DynamicObjectSerializer
	{
		private LocalPlayerController localController;

		// Keeps track of how long until we send an update
		private int stepsUntilSend;
		private readonly int MAX_STEPS_BETWEEN_SENDS = 5;
	 
		void Start () {
			// Get compenents
			remoteController = GetComponent<RemotePhysicsController> ();
			localController = GetComponent<LocalPlayerController> ();

			stepsUntilSend = 0;

			// Fill out last positions with dummys
			lastInfo = null;
			
		}

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
					info = new DynamicObjectInformation (localController.GetPosition (), localController.GetVelocity ());

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

		/// Let updateManager know there is an update
		protected override void Send (List<byte> message)
		{
			updateManager.SendPlayerUpdate (message);
		}
	}
}

