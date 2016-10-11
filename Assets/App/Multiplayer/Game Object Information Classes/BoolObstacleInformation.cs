using System;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class BoolObstacleInformation
	{
		public readonly byte ID;		// ID to uniquley identify the obstacle
		public readonly bool state;		// State of the obstacle

		public BoolObstacleInformation (byte ID, bool state)
		{
			this.ID = ID;
			this.state = state;
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			BoolObstacleInformation info = obj as BoolObstacleInformation;
			if ((System.Object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (info.ID == ID && info.state == state);
		}

		public bool Equals(BoolObstacleInformation info)
		{
			// If parameter is null return false:
			if ((object)info == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (info.ID == ID && info.state == state);
		}
	}
}

