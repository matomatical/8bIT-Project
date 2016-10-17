using System;

/*
 * This class is used to store information about  static obstacles 
 * and their current state in the game.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

#pragma warning disable 0659
namespace xyz._8bITProject.cooperace.multiplayer
{
	public class BoolObstacleInformation
	{
		public readonly byte ID;		// ID to uniquely identify the obstacle
		public readonly bool state;		// State of the obstacle

		/// Use this for initialisation
		public BoolObstacleInformation (byte ID, bool state)
		{
			this.ID = ID;
			this.state = state;
		}

		/// Check if two objects are equal
		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to BooleanObstacleInformation return false.
			BoolObstacleInformation info = obj as BoolObstacleInformation;
			if ((System.Object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this.Equals(info);
		}

		/// Check if two objects of type BoolObstacleInformation are equal
		public bool Equals(BoolObstacleInformation info)
		{
			// If parameter is null return false, otherwise
			// Return true if the fields match:
			return (info!=null) && (info.ID == ID && info.state == state);
		}
	}
}

