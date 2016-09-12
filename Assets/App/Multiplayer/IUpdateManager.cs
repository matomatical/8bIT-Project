using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public interface IUpdateManager
	{
		void HandleUpdate (byte[] data, String senderID);
		void SendObstacleUpdate (List<byte> data);
		void SendPlayerUpdate (List<byte> data);
		void SendTextChat(List<byte> data);
	}
}

