/*
 * The multiplayer serializer for Player game objects (includes the partner)
 * Used for representation of the state of the object as a list of bytes and updating the state of from a list of bytes.
 *
 * Sam Beyer sbeyer@student.unimelb.edu.au
*/

using System;
using UnityEngine;
using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public class PlayerSerializer : MonoBehaviour, ISerializer
	{
		// Defines what states the player can be in
		// NEEDS UPDATING (Talk to Athir and Matt about PlayerController)
		public enum PlayerState : byte { MOVING, STILL, JUMP, DEFAULT };

		// the update manager which should be told about any updates
		public IUpdateManager updateManager;

		// Used for getting position and state
		Animator animator;
		Transform thisTransform;

		// The list of subscribers
		private List<IListener<List<byte>>> subscribers;

		// Keeps track of how long until we send an update
		private int stepsUntilSend;
		private static readonly int MAX_STEPS_BETWEEN_SENDS = 5;

		// Keeps track of the last update to see if anything has changed
		private float lastPosx;
		private float lastPosy;
	 
		void Start () {
			// Get compenents
			animator = GetComponent<Animator> ();
			thisTransform = GetComponent<Transform> ();

			// set up list of subscribers
			subscribers = new List<IListener<List<byte>>> ();

			stepsUntilSend = 0;

			// Fill out last positions with dummys
			lastPosx = 0;
			lastPosy = 0;
		}

		// Called at set intervals, used to let update manager know there is an update
		void Update () {
			// stores current state
			List<byte> update;

			Debug.Log ("x: " + thisTransform.position.x + "y: " + thisTransform.position.y);


			// if it's time to send another update
			if (stepsUntilSend < 1) {
				// get the update to be sent
				update = Serialize ();
				// if the update is different to the last one sent
				if (thisTransform.position.x != lastPosx || thisTransform.position.y != lastPosy) {
					Debug.Log ("Serializing");

					// send the update
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

		// Add the object wanting to subscribe to the list of subscribers
		public void Subscribe(IListener<List<byte>> sub) {
			subscribers.Add (sub);
		}

		// Tell this object there is an update from an observable
		public void Notify (List<byte> message)
		{
			Deserialize (message);
		}

		// Let subscribers know there is an update
		private void Send (List<byte> message)
		{
			foreach (IListener<List<byte>> sub in subscribers) {
				sub.Notify (message);
			}
		}

		// Takes an update and applies it to this serializer's object
		private void Deserialize(List<byte> update)
		{
			float posx, posy;
			byte state;
			byte[] data = update.ToArray();

			// Get the information from the list of bytes
			posx = BitConverter.ToSingle (data, 0);
			posy = BitConverter.ToSingle (data, 4);
			state = data[8];

			// Update the position and the animation state
			thisTransform.position = new Vector3 (posx, posy, 0); 
			animator.SetInteger ("State", state);
		}

		// Takes the state of this object and turns it into an update
		private List<byte> Serialize()
		{
			// initialize list
			List<byte> bytes = new List<byte> ();

			// get current state and x and y values
			float posx = thisTransform.position.x;
			float posy = thisTransform.position.y;
			byte state = (byte)(animator.GetInteger("State"));

			// Add the byte representation of the above values into the list
			bytes.Add(UpdateManager.PLAYER);
			bytes.AddRange (BitConverter.GetBytes (posx));
			bytes.AddRange (BitConverter.GetBytes (posy));
			bytes.Add (state);
			return bytes;
		}
	}
}

