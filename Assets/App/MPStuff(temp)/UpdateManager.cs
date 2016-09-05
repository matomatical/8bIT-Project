using System;
using System.Collections.Generic;

namespace Team8bITProject
{
	public class UpdateManager : IUpdateManager
	{
		private float partnerPosx = 0;
		private float partnerPosy = 0;
		//private System.Collections.Queue<I HATE TYPES> obstacleQueue;
		private System.Collections.Queue<ChatMessage> chatQueue;
		public static readonly byte PROTOCOL_VERSION = 0;

		public void HandleUpdate (List update)
		{
			throw new NotImplementedException ();
		}

		public bool SendPlayerUpdate (float posx, float posy)
		{
			throw new NotImplementedException ();
		}

		public bool SendTextChat (string message)
		{
			return false;
		}

		public float GetPartnerPosx ()
		{
			throw new NotImplementedException ();
		}

		public float GetPartnerPosy ()
		{
			throw new NotImplementedException ();
		}

		public bool IsMoreObstacles ()
		{
			return false;
		}

		public ChatMessage GetNextChat ()
		{
			throw new NotImplementedException ();
		}

		public bool IsMoreChat ()
		{
			return false;
		}
	}
}

