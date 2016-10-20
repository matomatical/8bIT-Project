using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;

namespace xyz._8bITProject.cooperace.multiplayer {
	public class EditorMultiPlayerController : MultiPlayerController
	{

		// STARTING A GAME


		/// Look for a suitable partner to play the game with
		public override void StartMatchMaking(string level, IRoomListener room = null)
		{
			uint gameVariation = (uint)Maps.GetIndex(level);
			UILogger.Log("index: "+ gameVariation);

			this.roomListener = room;

			startedMatching = true;

			// start a matchmaking game right here
			OnRoomConnected(true);
		}

		public override void StopMatchMaking(){
			if (startedMatching && !startedGame) {
				startedMatching = false;
			}
		}






		// ROOM SET UP EVENTS

		/// If connection is established go back to whoever started matchmaking,
		/// so that they can continue and go to the game
		/// otherwise print an error message
		public override void OnRoomConnected(bool success)
		{
			if (success) {
				ShowMPStatus("Connected!");
				startedGame = true;
				startedMatching = false;
				updateManager = new UpdateManager (); // TODO later we'll do something really clever here
				roomListener.OnConnectionComplete ();
			}
			else
			{
				ShowMPStatus("Uh-oh. Looks like something went wrong when trying to connect to the room.");
			}
		}

		/// How's progress with setting up the room?
		public override void OnRoomSetupProgress(float percent)
		{
			ShowMPStatus("We are " + percent + "% done with setup");
		}

		/// What to do when a player declines an invitiation to join a room
		public override void OnParticipantLeft(Participant participant)
		{
			startedMatching = false;
			UILogger.Log("Player " + participant.DisplayName + " has left.");
		}





		// DURING A GAME

		public override void LeaveGame(){
			if (startedGame) {

				// leve the room
				Debug.Log("TODO: mock leave room");
				startedGame = false;
			}
		}



		// WHO IS IN THE ROOM?

		private bool editorhost = true;

		/// Get a list of all Participants in the room
		public override List<Participant> GetAllPlayers() {
			var list = new List<Participant> ();
			list.Add (
				new Participant ("matt", "editor", Participant.ParticipantStatus.Joined,
					new Player ("matt", "1", "https://s15-us2.ixquick.com/cgi-bin/serveimage?url=http%3A%2F%2Ft2.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcRp4pE8LCsGUPcyGDY-YB0d-UvwsSyeXhDMGrS7eUuAqJkl5SBTJw&sp=cac082468e9fdb45aa30dd9b4a892bdf"),
					true));
			list.Add (
				new Participant ("matt", "another ID", Participant.ParticipantStatus.Joined,
					new Player ("matt", "1", "https://s15-us2.ixquick.com/cgi-bin/serveimage?url=http%3A%2F%2Ft2.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcRp4pE8LCsGUPcyGDY-YB0d-UvwsSyeXhDMGrS7eUuAqJkl5SBTJw&sp=cac082468e9fdb45aa30dd9b4a892bdf"),
					true));
			return list;
		}

		/// Returns the ParticipantID of the device
		public override string GetMyParticipantId() {
			return (editorhost ? "editor" : "another ID");
		}





		// NETWORK EVENT DURING GAMEPLAY

		/// Sends an update to all participants in the room using the reliable protocol
		public override void SendMyReliable(List<byte> data) {
			this.updateManager.HandleUpdate (data, GetMyParticipantId());
		}

		/// Sends an update to all participants in the room using the unreliable protocol
		public override void SendMyUnreliable(List<byte> data) {
			if (UnityEngine.Random.value < 0.95f) {
				this.updateManager.HandleUpdate (data, GetMyParticipantId());
			}
		}

	}
}

