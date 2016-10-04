/*
 * This class is used to store information about the player and their current state in the game
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;
using UnityEngine;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class PlayerInformation
	{
		public readonly Vector2 pos;
		public readonly Vector2 vel;

		public PlayerInformation (Vector2 pos, Vector2 vel)
		{
			this.pos = pos;
			this.vel = vel;
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			PlayerInformation info = obj as PlayerInformation;
			if ((System.Object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (pos.x == info.pos.x) && (pos.y == info.pos.y && vel.x == info.vel.x && vel.y == info.vel.y);
		}

		public bool Equals(PlayerInformation info)
		{
			// If parameter is null return false:
			if ((object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (pos.x == info.pos.x) && (pos.y == info.pos.y) && (vel.x == info.vel.x) && (vel.y == info.vel.y);
		}
	}
}

