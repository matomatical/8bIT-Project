/*
 * This is the class all ingoing and outgoing updates pass through
 * It attatches and detatches a header to the update telling the game about what will be in the update
 * It is also responsible for deciding whether 
 * 
*/
using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public class UpdateManager : IUpdateManager
	{
		// The protcol being used to attatch the header
		public static readonly byte PROTOCOL_VERSION = 0;
		// Obstacle update identifier
		private static readonly char OBSTACLE = 'u';
		// Player update identifier
		private static readonly char PLAYER = 'p';
		// Chat update identifier
		private static readonly char CHAT = 't';

		// Takes an update and the sender and then distributes the update to subscribers
		public void HandleUpdate (byte[] data, string senderID)
		{
			throw new NotImplementedException ();
		}

		// Sends an update for an obstacle
		public void SendObstacleUpdate (List<byte> data)
		{
			ApplyHeader (data, OBSTACLE);
			MultiplayerController.Instance.SendMyReliable (data);
		}

		// Sends an update for a player
		public void SendPlayerUpdate (List<byte> data)
		{
			ApplyHeader (data, PLAYER);
			MultiplayerController.Instance.SendMyUnreliable (data);
		}

		// Sends an update for a chat message
		public void SendTextChat (List<byte> data)
		{
			ApplyHeader (data, OBSTACLE);
			MultiplayerController.Instance.SendMyReliable (data);
		}

		// Strips off the header of an update, returns information contained in the header
		private List<byte> StripHeader(List<byte> data) {
			byte protocol = data[0];
			List<byte> headerInfo = new List<byte>();

			if (protocol == PROTOCOL_VERSION) {
				data.RemoveAt (0);
				headerInfo.Add (data[0]);
				data.RemoveAt (0);
			}

			return headerInfo;
		}

		// Applies a header to an upddate (data), type is the type of update
		private void ApplyHeader(List<byte> data, char type) {
			data.Insert (0, PROTOCOL_VERSION);
			data.InsertRange (1, BitConverter.GetBytes(type));
		}
	}
}

