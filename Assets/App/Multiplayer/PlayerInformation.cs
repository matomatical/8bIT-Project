/*
 * This class is used to store information about the player and their current state in the game
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class PlayerInformation
	{
		public readonly float posx;
		public readonly float posy;

		public PlayerInformation (float posx, float posy)
		{
			this.posx = posx;
			this.posy = posy;
		}

		public override bool Equals(Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			PlayerInformation info = obj as PlayerInformation;
			if ((Object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (posx == info.posx) && (posy == info.posy);
		}

		public bool Equals(PlayerInformation info)
		{
			// If parameter is null return false:
			if ((object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (this.posx == info.posx) && (posy == info.posy);
		}
	}
}

