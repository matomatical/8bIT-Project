using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections.Generic;

// Making this controller the listener as well to keep all the MultiPlayer logic in one class
public class MultiplayerController : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
{
    public MPRoomListener roomListener;
    public MPUpdateListener updateListener;

    // Making this a singleon as it'll be used in both the main menu and the game
    private static MultiplayerController _instance = null;

    // Sticking to a 2 player game
    private uint minimumPartners = 1;
    private uint maximumPartners = 1;

    // Sticking with a single game mode
    private uint gameVariation = 0;

    private String levelName = "MP Empty Level";

    private byte protocolVersion = 1;
    // Byte + Byte + 2 floats for position + 2 floats for velcocity
    private int updateMessageLength = 18;
    private List<byte> updateMessage;

    private MultiplayerController()
    {
        updateMessage = new List<byte>(updateMessageLength);
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

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        // We'll be doing more with this later...
        byte messageVersion = (byte)data[0];

        // Let's figure out what type of message this is.
        char messageType = (char)data[1];

        // The u represents 'update'
        if (messageType == 'U' && data.Length == (updateMessageLength))
        {
            float posX = System.BitConverter.ToSingle(data, 2);
            float posY = System.BitConverter.ToSingle(data, 6);
            float velX = System.BitConverter.ToSingle(data, 10);
            float velY = System.BitConverter.ToSingle(data, 14);
            Debug.Log("Player " + senderId + " is at (" + posX + ", " + posY + ") traveling (" + velX + ", " + velY + ") ");
            // Tell our GameController about this.
            if (updateListener != null)
            {
                updateListener.UpdateReceived(senderId, posX, posY, velX, velY);
            }
        }
    }

    public List<Participant> GetAllPlayers()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    public string GetMyParticipantId()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
    }

    public void SendMyUpdate(float posX, float posY, Vector2 velocity)
    {
        updateMessage.Clear();
        updateMessage.Add(protocolVersion);

        // The u represents the message type i.e 'update'
        updateMessage.Add((byte)'U');
        updateMessage.AddRange(System.BitConverter.GetBytes(posX));
        updateMessage.AddRange(System.BitConverter.GetBytes(posY));
        updateMessage.AddRange(System.BitConverter.GetBytes(velocity.x));
        updateMessage.AddRange(System.BitConverter.GetBytes(velocity.y));
        byte[] messageToSend = updateMessage.ToArray();

        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, messageToSend);
    }
}
