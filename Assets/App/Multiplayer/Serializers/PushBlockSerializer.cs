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
    public class PushBlockSerializer : DynamicObjectSerializer
	{
		// The controller which we are sending updates from
		private PushBlockController localController;

		// The controller that is used to recieve the updates
		private RemotePhysicsController remoteController;

		void Start () {
			// Get compenents
			remoteController = GetComponent<RemotePhysicsController> ();
			localController = GetComponent<PushBlockController> ();
		}

		/// Let updateManager know there is an update
		protected override void Send (List<byte> message)
		{
			updateManager.SendPushBlockUpdate(message);
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
