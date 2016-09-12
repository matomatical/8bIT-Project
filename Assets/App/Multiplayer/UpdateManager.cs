using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public class UpdateManager : IUpdateManager
	{
		public static readonly byte PROTOCOL_VERSION = 0;
		private static readonly char OBSTACLE = 'u';
		private static readonly char PLAYER = 'p';
		private static readonly char CHAT = 't';

		public void HandleUpdate (byte[] data, string senderID)
		{
			throw new NotImplementedException ();
		}

		public void SendObstacleUpdate (List<byte> data)
		{
			ApplyHeader (data, OBSTACLE);
			MultiplayerController.Instance.SendMyReliable (data);
		}

		public void SendPlayerUpdate (List<byte> data)
		{
			ApplyHeader (data, PLAYER);
			MultiplayerController.Instance.SendMyUnreliable (data);
		}

		public void SendTextChat (List<byte> data)
		{
			ApplyHeader (data, OBSTACLE);
			MultiplayerController.Instance.SendMyReliable (data);
		}

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

		private void ApplyHeader(List<byte> data, char type) {
			data.Insert (0, PROTOCOL_VERSION);
			data.InsertRange (1, BitConverter.GetBytes(type));
		}
	}
}

