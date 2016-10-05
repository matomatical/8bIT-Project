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
	public class PlayerSerializer : MonoBehaviour, ISerializer<PlayerInformation>
	{
		// the update manager which should be told about any updates
		public IUpdateManager updateManager;

		// Used for getting position
		private RemotePlayerController remoteController;
		private LocalPlayerController localController;

		// Keeps track of how long until we send an update
		private int stepsUntilSend;
		private readonly int MAX_STEPS_BETWEEN_SENDS = 5;

		// Keeps track of the last update to see if anything has changed
		private PlayerInformation lastInfo;

		// Used to guard serialize and deserialize being run if the RemotePlayerContoller is not yet found
		private bool ready = false;
	 
		void Start () {
			// Get compenents
			remoteController = GetComponent<RemotePlayerController> ();
			localController = GetComponent<LocalPlayerController> ();

			stepsUntilSend = 0;

			// Fill out last positions with dummys
			lastInfo = null;

			ready = true;
		}

		// Called at set intervals, used to let update manager know there is an update
		void FixedUpdate () {
			// Stores current state
			List<byte> update;

			// Stores current information about the player
			PlayerInformation info;

			// Only send if there is an update manager to send to and the transform is found
			if (updateManager != null && ready) {
				
				// If it's time to send another update
				if (stepsUntilSend < 1) {
					 
					// Read information about the player currently
					info = new PlayerInformation (localController.GetPosition (), localController.GetVelocity ());

					// If the update is different to the last one sent
					if (!info.Equals(lastInfo)) {
						// Get the update to be sent
						update = Serialize (info);

						Debug.Log ("Serializing");

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

		// Tell this object there is an update from an observable
		public void Notify (List<byte> message)
		{
			PlayerInformation info = Deserialize (message);
			Apply (info);
		}

		// Let updateManager know there is an update
		private void Send (List<byte> message)
		{
			updateManager.SendPlayerUpdate (message);
		}

		// Applies information in info to the player this serializer is attatched to
		private void Apply(PlayerInformation info) {
			remoteController.SetState (info.pos, info.vel);
		}

		// Takes an update and applies it to this serializer's object
		public PlayerInformation Deserialize(List<byte> update)
		{
			float posx, posy;
			float velx, vely;
			byte[] data = update.ToArray ();

			// Just in case the List isn't long enough
			try {
				// Get the information from the list of bytes
				posx = BitConverter.ToSingle (data, 0);
				posy = BitConverter.ToSingle (data, 4);
				velx = BitConverter.ToSingle (data, 8);
				vely = BitConverter.ToSingle (data, 12);
			}
			catch (System.ArgumentOutOfRangeException e) {
				Debug.Log (e.Message);
				throw e;
			}

			// Create and return PlayerInformation with the data deserialized
			return new PlayerInformation(new Vector2 (posx, posy), new Vector2 (velx, vely));
		}

		// Takes the state of this object and turns it into an update
		public List<byte> Serialize(PlayerInformation info)
		{
			// initialize list to return
			List<byte> bytes = new List<byte> ();

			// get the data to Serialize from the PlayerInformation
			float posx = info.pos.x;
			float posy = info.pos.y;
			float velx = info.vel.x;
			float vely = info.vel.y;

			// Add the byte representation of the above values into the list
			bytes.AddRange (BitConverter.GetBytes (posx));
			bytes.AddRange (BitConverter.GetBytes (posy));
			bytes.AddRange (BitConverter.GetBytes (velx));
			bytes.AddRange (BitConverter.GetBytes (vely));

			return bytes;
		}
	}
}

