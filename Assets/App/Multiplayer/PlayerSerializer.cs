/*
 * The multiplayer serializer for Player game objects (includes the partner)
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
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
		private Transform thisTransform;

		// Keeps track of how long until we send an update
		private int stepsUntilSend;
		private readonly int MAX_STEPS_BETWEEN_SENDS = 5;

		// Keeps track of the last update to see if anything has changed
		private float lastPosx;
		private float lastPosy;

		// used to guard serialize and deserialize being run if the transform is not yet found
		private bool ready = false;
	 
		void Start () {
			// Get compenents
			thisTransform = GetComponent<Transform> ();

			stepsUntilSend = 0;

			// Fill out last positions with dummys
			lastPosx = 0;
			lastPosy = 0;

			ready = true;
		}

		// Called at set intervals, used to let update manager know there is an update
		void FixedUpdate () {
			// Stores current state
			List<byte> update;
			// Stores current information about the player
			PlayerInformation info;

			// Variables to store the players current state in the game
			float posx;
			float posy;

			// Only send if there is an update manager to send to and the transform is found
			if (updateManager != null && ready) {
				
				// Ff it's time to send another update
				if (stepsUntilSend < 1) {
					// Read information about the player currently
					posx = thisTransform.position.x;
					posy = thisTransform.position.y;

					// Get the update to be sent
					info = new PlayerInformation(posx, posy);
					update = Serialize (info);

					// Ff the update is different to the last one sent
					if (thisTransform.position.x != lastPosx || thisTransform.position.y != lastPosy) {
						Debug.Log ("Serializing");

						// Send the update
						Send (update);

						// reflect changes in the last update
						lastPosx = thisTransform.position.x;
						lastPosy = thisTransform.position.y;
						
					} else {
						Debug.Log ("Nothing has changed since last update");
					}

				} else {
					// reset the steps since an update was sent
					stepsUntilSend = MAX_STEPS_BETWEEN_SENDS;
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
			thisTransform.position = new Vector3 (info.posx, info.posy, 0);
		}

		// Takes an update and applies it to this serializer's object
		public PlayerInformation Deserialize(List<byte> update)
		{
			float posx, posy;
			byte[] data = update.ToArray ();

			// Get the information from the list of bytes
			posx = BitConverter.ToSingle (data, 0);
			posy = BitConverter.ToSingle (data, 4);

			// Create and return PlayerInformation with the data deserialized
			return new PlayerInformation(posx, posy);
		}

		// Takes the state of this object and turns it into an update
		public List<byte> Serialize(PlayerInformation info)
		{
			// initialize list to return
			List<byte> bytes = new List<byte> ();

			// get the data to Serialize from the PlayerInformation
			float posx = info.posx;
			float posy = info.posy;

			// Add the byte representation of the above values into the list
			bytes.AddRange (BitConverter.GetBytes (posx));
			bytes.AddRange (BitConverter.GetBytes (posy));
			return bytes;
		}
	}
}

