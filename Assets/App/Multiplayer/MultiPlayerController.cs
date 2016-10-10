/*
 * Acts a controller for the multiplayer side of the game
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

namespace xyz._8bITProject.cooperace.multiplayer
{
	// Making this controller the listener as well to keep all the MultiPlayer logic in one class
	public class MultiPlayerController : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
	{
		public IRoomListener roomListener;
		public IUpdateManager updateManager;

		// Making this a singleton as it'll be used in both the main menu and the game
		private static MultiPlayerController _instance = null;

		// Sticking to a 2 player game
		private uint minimumPartners = 1;
		private uint maximumPartners = 1;

		// Sticking with a single game mode
		private uint gameVariation = 0;

		// The unity scene to go to when starting the game
		private String levelName = "Game Scene";



		// initialises the Multiplayer controller instance
		protected MultiPlayerController()
		{
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate();
		}

		// makes multiplayer controller a singleton
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

		// Start a new Multiplayer game by looking for someone to play with
		public virtual void StartMPGame()
		{
			StartMatchMaking();
		}

		// Look for a suitable partner to play the game with
		private void StartMatchMaking()
		{
			PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumPartners, maximumPartners, gameVariation, this);
		}

		// Let's the player know how the matchmaking process is going
		private void ShowMPStatus(string message)
		{
			Debug.Log(message);
			if (roomListener != null)
			{
				roomListener.SetRoomStatusMessage(message);
			}
		}

		// How's progress with setting up the room?
		public virtual void OnRoomSetupProgress(float percent)
		{
			ShowMPStatus("We are " + percent + "% done with setup");
		}

		// After connection is established go to the level specified, otherwise print an error message
		public virtual void OnRoomConnected(bool success)
		{
			if (success)
			{
				ShowMPStatus("We are connected to the room! WOOHOO!");
				roomListener.HideRoom();
				roomListener = null;
				UIHelper.GoTo(levelName);
			}
			else
			{
				ShowMPStatus("Uh-oh. Looks like something went wrong when trying to connect to the room.");
			}
		}

		// What to do if the player leaves the room.
		// NOTE : Have still to handle this properly
		public virtual void OnLeftRoom()
		{
			ShowMPStatus("We have left the room. We should probably perform some clean-up stuff.");
            UILogger.Log("On left room");
            //LeaveGame();

        }

		// What to do when a particular player leaves the room
		public virtual void OnParticipantLeft(Participant participant)
		{
			ShowMPStatus("Player " + participant.DisplayName + " has left.");
            //UILogger.Log("OnParticipantLeft - player just left");
            LeaveGame();

        }

		// What to do when a player has joined the room
		public virtual void OnPeersConnected(string[] participantIds)
		{
			foreach (string participantID in participantIds)
			{
				ShowMPStatus("Player " + participantID + " has joined.");
			}
		}

		// What to do when players leave the room
		public virtual void OnPeersDisconnected(string[] participantIds)
		{
			foreach (string participantID in participantIds)
			{
				ShowMPStatus("Player " + participantID + " has left.");
				//UILogger.Log("OnPeersDisconnected - player just left");
                LeaveGame();
                //GameObject menu = GameObject.Find("PlayerDisconnectedMenu");
                //menu.SetActive(true);
            }
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

		// Get a list of all Participants in the room
		public virtual List<Participant> GetAllPlayers()
		{
			return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
		}

		// Returns the ParticipantID of the device
		public virtual string GetMyParticipantId()
		{
			return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
		}

		// Sends an update to all participants in the room using the reliable protocol
		public virtual void SendMyReliable(List<byte> data)
		{
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data.ToArray ());
		}

		// Sends an update to all participants in the room using the unreliable protocol
		public virtual void SendMyUnreliable(List<byte> data)
		{
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data.ToArray ());
		}

        // Causes the player to leave the game
		public void LeaveGame() {
            //Debug.Log("Leave game was called");
            GameObject gui = GameObject.Find("In-Game GUI");
            //this.transform.FindChild("KillZone").gameObject;
            /*
            if (gui) {
                UILogger.Log("gui found");
            }
            else {
                UILogger.Log("gui not found");
            }*/


            //GameObject menu = GameObject.Find("In-Game GUI/PlayerDisconnectedMenu");

            GameObject menu = gui.transform.FindChild("PlayerDisconnectedMenu").gameObject;
            /*
            if (menu) {
                UILogger.Log("menu was found");
            }
            else {
                UILogger.Log("menu was not found....fuck");
            }*/
            menu.SetActive(true);
            //UILogger.Log("menu should now be active");

        }
    }
}