using System;
using UnityEngine;
using System.Collections.Generic;

namespace Team8bITProject
{
	public class PlayerSerializer : MonoBehaviour, ISerializer
	{
		public enum PlayerState : byte { MOVING, STILL, JUMP, DEFAULT };

		public IUpdateManager updateManager;

		Animator animator;
		Transform thisTransform;
	 
		void Start () {
			animator = GetComponent<Animator> ();
			thisTransform = GetComponent<Transform> ();
		}

		void FixedUpdate () {
			
		}

		public void Subscribe(IListener<List<byte>> o) {
			
		}

		public void Deserialize(List<byte> update)
		{
			float posx, posy;
			byte state;
			byte[] data = update.ToArray();

			posx = BitConverter.ToSingle (data, 0);
			posy = BitConverter.ToSingle (data, 4);
			state = data[8];

			thisTransform.position = new Vector3 (posx, posy, 0); 
			animator.SetInteger ("State", state);
		}

		public List<byte> Serialize()
		{
			// initialize list
			List<byte> bytes = new List<byte> ();

			// get current state and x and y values
			float posx = thisTransform.position.x;
			float posy = thisTransform.position.y;
			byte state = (byte)(animator.GetInteger("State"));

			bytes.AddRange (BitConverter.GetBytes (posx));
			bytes.AddRange (BitConverter.GetBytes (posy));
			bytes.Add (state);
			return bytes;
		}
	}
}

