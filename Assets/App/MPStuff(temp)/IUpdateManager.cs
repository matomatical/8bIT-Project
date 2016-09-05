using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public interface IUpdateManager
	{
		// takes and update and works out what to do with it
		void HandleUpdate(List<byte> update);

		// for sending updates
		//bool sendObstacle( what type should this be?);
		bool SendPlayerUpdate(float posx, float posy);
		bool SendTextChat(String message);

		// methods for getting the information gained from updates
		float GetPartnerPosx();
		float GetPartnerPosy();
		//<still don't know what type> getNextObstacle();
		bool IsMoreObstacles();
		ChatMessage GetNextChat();
		bool IsMoreChat();

	}
}

