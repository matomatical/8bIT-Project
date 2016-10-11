using System;
using UnityEngine;

namespace xyz._8bITProject.cooperace.multiplayer
{
	[RequireComponent (typeof (KeyBlock))]
	public class KeyBlockSerializer : BoolObstacleSerializer
	{
		/// the key block to track
		private KeyBlock block;

		void Start(){

			// link components

			block = GetComponent<KeyBlock>();
		}

		/// get the actual state of the key block being tracked this frame
		public override bool GetState(){
			return block.IsOpen();
		}

	}
}

