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
		// The controller which we are sending updates from
		private LocalPlayerController localController;

		// The controller that is used to recieve the updates
		private RemotePhysicsController remoteController;
	 
		void Start () {
			// Get compenents
			remoteController = GetComponent<RemotePhysicsController> ();
			localController = GetComponent<LocalPlayerController> ();
		}


		/// Let updateManager know there is an update
		protected override void Send (List<byte> message)
		{
			updateManager.SendPlayerUpdate (message);
		}

		protected override DynamicObjectInformation GetState ()
		{
			return new DynamicObjectInformation (localController.GetPosition (),
				localController.GetVelocity (),
				Time.time);
		}

		protected override void SetState (DynamicObjectInformation information)
		{
			remoteController.SetState(information.pos, information.vel);
		}
	}
}

