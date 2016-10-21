using System;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

namespace xyz._8bITProject.cooperace.multiplayer.tests
{
	public class MockMultiPlayerController : MultiPlayerController
	{
		// not needed for testing
		public override void StartMatchMaking(string level, IRoomListener room = null)
		{
			return;
		}

		// not needed for testing
		public override void OnRoomSetupProgress (float percent)
		{
			return;
		}

		// not needed for testing
		public override void OnRoomConnected (bool success)
		{
			return;
		}

		// not needed for testing
		public override void OnLeftRoom ()
		{
			return;
		}

		// not needed for testing
		public override void OnParticipantLeft (GooglePlayGames.BasicApi.Multiplayer.Participant participant)
		{
			return;
		}

		// not needed for testing
		public override void OnPeersConnected (string[] participantIds)
		{
			return;
		}

		// not needed for testing
		public override void OnPeersDisconnected (string[] participantIds)
		{
			return;
		}

		public override void OnRealTimeMessageReceived (bool isReliable, string senderId, byte[] data)
		{
			throw new NotImplementedException ();
		}

		// Not needed for testing
		public override List<Participant> GetAllPlayers()
		{
			return new List<Participant> ();
		}

		// Not needed for testing
		public override string GetMyParticipantId()
		{
			return "samma";
		}

		// To be written for testing
		public override void SendMyReliable(List<byte> data)
		{
			return;
		}

		// To be written for testing
		public override void SendMyUnreliable(List<byte> data)
		{
			return;
		}
	}
}

