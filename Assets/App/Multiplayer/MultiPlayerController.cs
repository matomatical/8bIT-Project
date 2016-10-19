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
		public IUpdateManager updateManager;

		// Making this a singleton
		private static MultiPlayerController _instance = null;

		// Sticking to a 2 player game
		private uint minimumPartners = 1;
		private uint maximumPartners = 1;

		// Sticking with a single game mode
		private uint gameVariation;

		// Whether or not you've started a game
		private bool startedGame = false;

		// Are we the host?
		public bool? host;

		//
		public string ourName = GamerTagManager.GetGamerTag ();
		public string theirName;

		public bool IsHost () {
			if (host.HasValue)
				return host.Value;
			else
				throw new NotYetSetException ("host not yet set");
		}

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
					_instance = new MultiPlayerController();
				}
				return _instance;
			}
		}

		/// Start a new Multiplayer game by looking for someone to play with
		public virtual void StartMPGame(uint index)
		{
			StartMatchMaking(index);
		}

		/// Look for a suitable partner to play the game with
		private void StartMatchMaking(uint index)
		{
			gameVariation = index;
			// if it's the random level
			gameVariation = (uint)(UnityEngine.Random.Range(0, Maps.maps.Length - 1) + 1);
			UILogger.Log("index: "+ gameVariation);
			PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumPartners, maximumPartners, gameVariation, this);
		}

		/// Let's the player know how the matchmaking process is going
		private void ShowMPStatus(string message)
		{
			Debug.Log(message);
			if (roomListener != null)
			{
				roomListener.SetRoomStatusMessage(message);
			}
		}

		/// How's progress with setting up the room?
		public virtual void OnRoomSetupProgress(float percent)
		{
			ShowMPStatus("We are " + percent + "% done with setup");
		}

		/// After connection is established go to the level specified, otherwise print an error message
		public virtual void OnRoomConnected(bool success)
		{
			if (success)
			{
				ShowMPStatus("We are connected to the room! WOOHOO!");
				roomListener.HideRoom();
				roomListener = null;
				startedGame = true;
				UIHelper.GoTo(Magic.Scenes.GAME_SCENE);
			}
			else
			{
				ShowMPStatus("Uh-oh. Looks like something went wrong when trying to connect to the room.");
			}
		}

		/// What to do if the player leaves the room.
		public virtual void OnLeftRoom()
		{
			ShowMPStatus("We have left the room. We should probably perform some clean-up stuff.");
			UILogger.Log("I have left the game");
			//LeaveRoom(startedGame);

		}

		/// What to do when a particular player leaves the room
		public virtual void OnParticipantLeft(Participant participant)
		{
			UILogger.Log("Player " + participant.DisplayName + " has left.");
			LeaveRoom(startedGame);
			//UIHelper.LeftGameMenu();

		}

		/// What to do when a player has joined the room
		public virtual void OnPeersConnected(string[] participantIds)
		{
			foreach (string participantID in participantIds)
			{
				ShowMPStatus("Player " + participantID + " has joined.");
			}
		}

		/// What to do when players leave the room
		public virtual void OnPeersDisconnected(string[] participantIds)
		{
			//UIHelper.LeftGameMenu();
			foreach (string participantID in participantIds)
			{
				UILogger.Log("Player " + participantID + " has left.");
			}
			LeaveRoom(startedGame);
		}

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

		/// Get a list of all Participants in the room
		public virtual List<Participant> GetAllPlayers()
		{
			return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
		}

		/// Returns the ParticipantID of the device
		public virtual string GetMyParticipantId()
		{
			return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
		}

		/// Sends an update to all participants in the room using the reliable protocol
		public virtual void SendMyReliable(List<byte> data)
		{
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data.ToArray ());
		}

		/// Sends an update to all participants in the room using the unreliable protocol
		public virtual void SendMyUnreliable(List<byte> data)
		{
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data.ToArray ());
		}

		/// Leave a room appropriately
		private void LeaveRoom(bool startedGame) {
			if (startedGame) {
				// If the game has started and your partner leaves, bring up a menu notifying
				// the player

				UIHelper.LeftGameMenu ();

			} else {
				// restart the matchmaking process
				UIHelper.LeaveRoom();
				//_instance.StartMPGame();
			}
		}
	}
}