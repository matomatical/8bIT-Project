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
		// The controller which we are tracking for updates
		private ArcadePhysicsController player;

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
					player = controller;
					break;
				}
			}
		}


		/// Let updateManager know there is an update
		protected override void Send (List<byte> message)
		{
			updateManager.SendPlayerUpdate (message);
		}

		protected override DynamicObjectInformation GetState ()
		{
			return new DynamicObjectInformation (
				player.GetPosition (), player.GetVelocity (), Time.time);
		}

		protected override void SetState (DynamicObjectInformation information)
		{
			lerper.AddState(information.pos, information.vel);
		}
	}
}

