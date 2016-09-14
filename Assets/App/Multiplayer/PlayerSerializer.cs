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
		// NEEDS UPDATING (Talk to Athir and Matt about PlayerController
		public enum PlayerState : byte { MOVING, STILL, JUMP, DEFAULT };

		// the update manager which should be told about any updates
		public IUpdateManager updateManager;

		// Used for getting position and state
		Animator animator;
		Transform thisTransform;
	 
		void Start () {
			// Get compenents
			animator = GetComponent<Animator> ();
			thisTransform = GetComponent<Transform> ();
		}


		// THIS IS GOING TO CHANGE A FAIR BIT
		void FixedUpdate () {
			
		}

		// THIS IS GOING TO CHANGE A FAIR BIT
		public void Subscribe(IListener<List<byte>> o) {
			
		}

		// Takes an update and applies it to this serializer's object
		public void Deserialize(List<byte> update)
		{
			float posx, posy;
			byte state;
			byte[] data = update.ToArray();

			// Get the information from the list of bytes
			// WE SHOULD ADD IN A PROTOCOL VERSION HERE
			posx = BitConverter.ToSingle (data, 0);
			posy = BitConverter.ToSingle (data, 4);
			state = data[8];

			// Update the position and the animation state
			thisTransform.position = new Vector3 (posx, posy, 0); 
			animator.SetInteger ("State", state);
		}

		// Takes the state of this object and turns it into an update
		public List<byte> Serialize()
		{
			// initialize list
			List<byte> bytes = new List<byte> ();

			// get current state and x and y values
			float posx = thisTransform.position.x;
			float posy = thisTransform.position.y;
			byte state = (byte)(animator.GetInteger("State"));

			// Add the byte representation of the above values into the list
			bytes.AddRange (BitConverter.GetBytes (posx));
			bytes.AddRange (BitConverter.GetBytes (posy));
			bytes.Add (state);
			return bytes;
		}

		public void Notify (List<byte> message)
		{
			Deserialize (message);
		}
	}
}

