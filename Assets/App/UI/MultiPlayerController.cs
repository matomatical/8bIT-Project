using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;

// Making this controller the listener as well to keep all the MultiPlayer logic in one class
public class MultiplayerController : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
{
    public MatchMaking room;

    // Making this a singleon as it'll be used in both the main menu and the game
    private static MultiplayerController _instance = null;

    // Sticking to a 2 player game
    private uint minimumPartners = 1;
    private uint maximumPartners = 1;

    // Sticking with a single game mode
    private uint gameVariation = 0;

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
        if (room != null)
        {
            room.SetRoomStatusMessage(message);
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

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        ShowMPStatus("We have received some gameplay messages from participant ID:" + senderId);
    }
}
