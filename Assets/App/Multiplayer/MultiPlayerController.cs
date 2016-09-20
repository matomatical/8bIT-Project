/*
 * Acts a controller for the multiplayer side of the game
 * Works as an interface between the game and Google Play
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
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
	public class MultiplayerController : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
	{
	    public IRoomListener roomListener;
	    public IUpdateManager updateManager;

	    // Making this a singleton as it'll be used in both the main menu and the game
	    private static MultiplayerController _instance = null;

	    // Sticking to a 2 player game
	    private uint minimumPartners = 1;
	    private uint maximumPartners = 1;

	    // Sticking with a single game mode
	    private uint gameVariation = 0;

	    private String levelName = "Multiplayer Test Level";

	    private MultiplayerController()
	    {
	        PlayGamesPlatform.DebugLogEnabled = true;
	        PlayGamesPlatform.Activate();
	    }

	    public static MultiplayerController Instance
	    {
	        get
	        {
	            if (_instance == null)
	            {
	                _instance = new MultiplayerController();
	            }
	            return _instance;
	        }
	    }

	    public void StartMPGame()
	    {
	        StartMatchMaking();
	    }

	    private void StartMatchMaking()
	    {
	        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumPartners, maximumPartners, gameVariation, this);
	    }

	    private void ShowMPStatus(string message)
	    {
	        Debug.Log(message);
	        if (roomListener != null)
	        {
	            roomListener.SetRoomStatusMessage(message);
	        }
	    }

	    public void OnRoomSetupProgress(float percent)
	    {
	        ShowMPStatus("We are " + percent + "% done with setup");
	    }

	    public void OnRoomConnected(bool success)
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

	    public void OnLeftRoom()
	    {
	        ShowMPStatus("We have left the room. We should probably perform some clean-up stuff.");
	    }

	    public void OnParticipantLeft(Participant participant)
	    {
	        ShowMPStatus("Player " + participant.DisplayName + " has left.");
	    }

	    public void OnPeersConnected(string[] participantIds)
	    {
	        foreach (string participantID in participantIds)
	        {
	            ShowMPStatus("Player " + participantID + " has joined.");
	        }
	    }

	    public void OnPeersDisconnected(string[] participantIds)
	    {
	        foreach (string participantID in participantIds)
	        {
	            ShowMPStatus("Player " + participantID + " has left.");
	        }
	    }

		/* Called when an update is recieved from a peer
		 * 
		 * isReliable	- Whether the message was sent using the reliable protocol or not
		 * senderID		- The ParticipantID of the sender
		 * data			- The message recieved
		 */
	    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
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
	    public List<Participant> GetAllPlayers()
	    {
	        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
	    }

		// Returns the ParticipantID of the device
	    public string GetMyParticipantId()
	    {
	        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
	    }

		// Sends an update to all participants in the room using the reliable protocol
		public void SendMyReliable(List<byte> data)
	    {
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data.ToArray ());
	    }

		// Sends an update to all participants in the room using the unreliable protocol
		public void SendMyUnreliable(List<byte> data)
		{
			PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data.ToArray ());
		}
	}
}
