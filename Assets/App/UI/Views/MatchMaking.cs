using UnityEngine;
using System.Collections;
using GooglePlayGames;
using System;

public class MatchMaking : MonoBehaviour, MPRoomListener {
    // Holds the image that will be the background of the dialogue box
    public GUISkin guiSkin;

    // Whether to display the room dialogue box and what message it should contain
    private bool showRoomDialogue;
    private string roomMessage;

	void Start()
    {
        showRoomDialogue = false;
        PlayGamesPlatform.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("here");
    }

    void OnGUI () {
        if (!showRoomDialogue)
        {
            roomMessage = "Starting a multi-player game...";
            showRoomDialogue = true;
            MultiplayerController.Instance.roomListener = this;
            MultiplayerController.Instance.StartMPGame();
        }
        
        if (showRoomDialogue)
        {
            GUI.skin = guiSkin;
            GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), roomMessage);
        }
    }

    public void SetRoomStatusMessage(string message)
    {
        roomMessage = message;
    }

    public void HideRoom()
    {
        roomMessage = "";
        showRoomDialogue = false;
    }
}
