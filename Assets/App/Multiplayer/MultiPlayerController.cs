/*
 * Acts as a controller for the multiplayer side of the game
 * Works as an interface between the game and Google Play
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections.Generic;
using xyz._8bITProject.cooperace.ui;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.multiplayer
{
	// Making this controller the listener as well to keep all the MultiPlayer logic in one class
	public class MultiPlayerController : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
	{
		public IRoomListener roomListener;
		public UpdateManager updateManager;

		// Making this a singleton
		private static MultiPlayerController _instance = null;

		// Sticking to a 2 player game
		private uint minimumPartners = 1;
		private uint maximumPartners = 1;

		// Whether or not you've started a game
		protected bool startedGame = false;
		protected bool startedMatching = false;

		// Are we the host?
		public bool? host;

		// gamer tags
		public string ourName = GamerTagManager.GetGamerTag ();
		public string theirName = "???"; // until we know their tag, leave it as "???"


		public bool IsHost () {
			if (host.HasValue)
				return host.Value;
			else
				throw new NotYetSetException ("host not yet set");
		}






		// SINGLETON PATTERN

		/// initialises the Multiplayer controller instance
		protected MultiPlayerController()
		{
			PlayGamesPlatform.Activate();
			PlayGamesPlatform.DebugLogEnabled = true;
		}

		/// makes multiplayer controller a singleton
		public static MultiPlayerController Instance
		{
			get
			{
				if (_instance == null)
				{
					#if UNITY_EDITOR
					_instance = new EditorMultiPlayerController();
					#else
					_instance = new MultiPlayerController();
					#endif
				}
				return _instance;
			}
		}





		// STARTING A GAME


		/// Look for a suitable partner to play the game with
		public virtual void StartMatchMaking(string level, IRoomListener room = null)
		{
			uint gameVariation = (uint)Maps.GetIndex(level);
			UILogger.Log("index: "+ gameVariation);

			this.roomListener = room;

			startedMatching = true;
			ShowMPStatus("Connecting...");
			PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumPartners, maximumPartners, gameVariation, this);
		}

		public virtual void StopMatchMaking(){
			if (startedMatching && !startedGame) {
				PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
				startedMatching = false;
			}
		}






		// ROOM SET UP EVENTS

		/// If connection is established go back to whoever started matchmaking,
		/// so that they can continue and go to the game
		/// otherwise print an error message
		public virtual void OnRoomConnected(bool success)
		{
			if (success) {
				ShowMPStatus("Connected!");
				startedGame = true;
				startedMatching = false;
				updateManager = new UpdateManager ();
				roomListener.OnConnectionComplete ();
			} else {
				ShowMPStatus("Failed to connect.");
			}
		}
			
		/// How's progress with setting up the room?
		public virtual void OnRoomSetupProgress(float percent)
		{
			ShowMPStatus("Connecting... " + percent + "%");
		}

		/// What to do when a player declines an invitiation to join a room
		public virtual void OnParticipantLeft(Participant participant)
		{
			startedMatching = false;
			UILogger.Log("Player " + participant.DisplayName + " has left.");
		}



		/// Lets the player know how the matchmaking process is going
		protected void ShowMPStatus(string message)
		{
			Debug.Log(message);
			if (roomListener != null) {
				roomListener.SetRoomStatusMessage(message);
			}
		}







		// DURING A GAME

		public virtual void LeaveGame(){
			if (startedGame) {
				PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
				startedGame = false;
			}
		}



		// ROOM EVENTS DURING GAMEPLAY

		/// What to do if the player leaves the room.
		public virtual void OnLeftRoom()
		{
			UILogger.Log("I have left the game");
		}


		/// What to do when a player has joined the room
		public virtual void OnPeersConnected(string[] participantIds) {
			foreach (string participantID in participantIds) {
				UILogger.Log("Player " + participantID + " has joined.");
			}
		}

		/// What to do when players leave the room
		public virtual void OnPeersDisconnected(string[] participantIds) {
			foreach (string participantID in participantIds) {
				UILogger.Log("Player " + participantID + " has left.");
			}
		}





		// WHO IS IN THE ROOM?

		/// Get a list of all Participants in the room
		public virtual List<Participant> GetAllPlayers() {
			return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
		}

		/// Returns the ParticipantID of the device
		public virtual string GetMyParticipantId() {
			return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
		}





		// NETWORK EVENT DURING GAMEPLAY


		/* Called when an update is recieved from a peer
		 * 
		 * isReliable	- Whether the message was sent using the reliable protocol or not
		 * senderID		- The ParticipantID of the sender
		 * data			- The message recieved
		 */
		public virtual void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
		{
			// turn the data into a List instead of an array
			List<byte> listData = new List<byte> ();
			listData.AddRange (data);

			// Tell our update manager about this.
			if (updateManager != null)
			{
				updateManager.HandleUpdate(listData, senderId);
			}
		}




		/// Sends an update to all participants in the room using the reliable protocol
		public virtual void SendMyReliable(List<byte> data) {
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data.ToArray ());
		}

		/// Sends an update to all participants in the room using the unreliable protocol
		public virtual void SendMyUnreliable(List<byte> data) {
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data.ToArray ());
		}

	}
}