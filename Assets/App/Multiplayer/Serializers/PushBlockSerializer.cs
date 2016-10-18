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
		// The controller which we are tracking for updates
		private ArcadePhysicsController block;

		// The lerper that is used to recieve the updates
		private LerpingPhysicsController lerper;

		void Start () {

			// link components

			lerper = GetComponent<LerpingPhysicsController> ();

			// find the first enabled player controller

			ArcadePhysicsController[] controllers = GetComponents<ArcadePhysicsController> ();
			foreach (ArcadePhysicsController controller in controllers) {
				if (controller.enabled) {
					// found an enabled controller! let's track this one
					block = controller;
					break;
				}
			}
		}

		/// Let updateManager know there is an update
		protected override void Send (List<byte> message)
		{
			updateManager.SendPushBlockUpdate (message);
		}

		protected override DynamicObjectInformation GetState ()
		{
			return new DynamicObjectInformation (
				block.GetPosition (), block.GetVelocity (), Time.time);
		}

		protected override void SetState (DynamicObjectInformation information)
		{
			lerper.AddState(information.pos, information.vel);
		}
	}
}
