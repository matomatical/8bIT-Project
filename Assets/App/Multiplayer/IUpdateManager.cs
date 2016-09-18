/* 
 * Interface for the updateManger used by the MultiplayerController class
 * It is a central place where updates can be sent out from, and also recieved
 * 
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
*/

using System;
using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public interface IUpdateManager
	{
		// When an update is recieved, this method is called
		void HandleUpdate (List<byte> data, String senderID);
		// Methods for sending update of different kinds of game objects
		void SendObstacleUpdate (List<byte> data);
		void SendPlayerUpdate (List<byte> data);
		void SendTextChat(List<byte> data);
	}
}

