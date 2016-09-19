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
		// Defines what states the player can be in
		public enum PlayerState : byte { MOVING, STILL, JUMP, DEFAULT };

		public readonly float posx;
		public readonly float posy;
		public readonly PlayerState state;

		public PlayerInformation (float posx, float posy, PlayerState state)
		{
			this.posx = posx;
			this.posy = posy;
			this.state = state;
		}
	}
}

