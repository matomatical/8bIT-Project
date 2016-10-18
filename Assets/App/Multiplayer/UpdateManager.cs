/*
 * This is the class where all ingoing and outgoing updates pass through
 * It is responsible for deciding which type of  update it recieves as well as
 * sending messages with the appropriate update type header attached to them.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
 */
using System;
using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class UpdateManager : IUpdateManager, IObservable<List<byte>,UpdateManager.Channel>
	{
		#if UNITY_EDITOR
		static bool editor = true;
		#else
		static bool editor = false;
		#endif
		
		// The protcol being used to attach the header
		public static readonly byte PROTOCOL_VERSION = 0;

		// Obstacle update identifier
		public static readonly byte OBSTACLE = BitConverter.GetBytes ('o')[0];

		// PushBlock update identifier
		public static readonly byte PUSHBLOCK = BitConverter.GetBytes ('b')[0];

		// Player update identifier
		public static readonly byte PLAYER = BitConverter.GetBytes ('p')[0];

		// Chat update identifier
		public static readonly byte CHAT = BitConverter.GetBytes ('t')[0];

		// Clock update identifier
		public static readonly byte CLOCK = BitConverter.GetBytes ('c')[0];

		// Leaderboards update identifier
		public static readonly byte LEADERBOARDS = BitConverter.GetBytes ('l')[0];


		// The body of the message sent when the clock is to be started
		private static readonly byte START_CLOCK = 1;

		// The body of the message sent when the clock is to be stopped
		private static readonly byte STOP_CLOCK = 0;


		// The ChatController to which chat message updates should be sent
		public ChatController chatController;

		// The ClockController which should be told about clock updates
		public ClockController clock;


		// OBSERVER PATTERN

		// enumerated type of channels! subscribers can nominate one of these types
		// updates to subscribe to
		public enum Channel { PLAYER, OBSTACLE, PUSHBLOCK }

		// Collection of subscriber lists

		private List<IListener<List<byte>>>[] subscribers;

		// An object is added to the list of subscribers for this channel
		public void Subscribe (IListener<List<byte>> o, Channel c)
		{
			subscribers[(int)c].Add (o);
		}

		/// Notifies all subscribers of an update to this channel
		private void NotifyAll (List<byte> data, Channel c) {
			foreach (IListener<List<byte>> sub in subscribers[(int)c]) {
				sub.Notify (data);
			}
		}

		public UpdateManager(){

			// initialise the subscribers array of lists to be the right size
			// there SHOULD be a way to do this at compile time, but I don't
			// know a static way to find the number of channels (other than
			// hard coding it)

			int numChannels = Enum.GetNames(typeof(Channel)).Length;
			subscribers = new List<IListener<List<byte>>>[numChannels];

			for (int i = 0; i < numChannels; i++) {
				subscribers [i] = new List<IListener<List<byte>>> ();
			}
		}


		// Takes an update and the sender and then distributes the update to subscribers
		public void HandleUpdate (List<byte> data, string senderID)
		{
			Debug.Log ("Update recieved from " + senderID);

			// Strip the header off the update
			HeaderManager.Header header = HeaderManager.StripHeader(data);

			if (header.protocol == PROTOCOL_VERSION) {

				// If/else tree for handling the message type
				if (header.messageType == PLAYER) {
					Debug.Log ("Notifying everyone on the player channel");

					// Notify player subscribers of player updates
					NotifyAll (data, Channel.PLAYER);
				} else if (header.messageType == OBSTACLE) {
					Debug.Log ("Notifying everyone on the obstacle channel");

					// Notify all obstacle subscribers of an obstacle update
					NotifyAll (data, Channel.OBSTACLE);
				} else if (header.messageType == PUSHBLOCK) {
					Debug.Log ("Notifying everyone on the pushblock channel");

					// Notify all obstacle subscribers of an obstacle update
					NotifyAll (data, Channel.PUSHBLOCK);
				} else if (header.messageType == CHAT && chatController != null) {
					Debug.Log ("Notifying ChatController");

					// Give chat controller the message
					chatController.ReceiveMessage (data);
				} else if (header.messageType == CLOCK && clock != null) {
					Debug.Log ("Notifying Clock");
                   // UILogger.Log("Notifying Clock");

					// Start or stop the clock as appropriate
					if (data [0] == START_CLOCK) {
						clock.StartTiming ();
					} else if (data [0] == STOP_CLOCK) {
						clock.StopTiming ();
                        UILogger.Log("update manager - about to finalize game");
						FinalizeLevel.FinalizeGame (clock.GetTime ());
					}
				} else if (header.messageType == LEADERBOARDS) {
					FinalizeLevel.position = data [0];
				} else {
					throw new MessageHeaderException ("invalid update identifier");
				}

			} else {
				throw new MessageHeaderException ("invalid protocol version");
			}
		}

		// Sends an update for an obstacle
		public void SendObstacleUpdate (List<byte> data)
		{
			ApplyHeader(data, OBSTACLE);

			if (editor) {
				HandleUpdate (data, "myself");
			} else {
				MultiPlayerController.Instance.SendMyReliable (data);
			}

			Debug.Log ("Sending obstacle update");
		}

		// Sends an update for a player
		public void SendPlayerUpdate (List<byte> data)
		{
			ApplyHeader(data, PLAYER);
			if (editor) {
				HandleUpdate(data, "memes");
			} else {
				MultiPlayerController.Instance.SendMyUnreliable(data);
			}
			
			Debug.Log ("Sending player update");
		}

		// Sends an update for a pushblock
		public void SendPushBlockUpdate(List<byte> data) {
			ApplyHeader(data, PUSHBLOCK);
			if (editor) {
				HandleUpdate(data, "memes");
			}
			else {
				MultiPlayerController.Instance.SendMyReliable(data);
			}

			Debug.Log("Sending push block update");
		}


		// Sends an update for a chat message
		public void SendTextChat (List<byte> data)
		{
			ApplyHeader(data, CHAT);
			if (editor) {
				HandleUpdate(data, "myself");
			} else {
				MultiPlayerController.Instance.SendMyReliable (data);	
			}
			Debug.Log ("Sending chat message");
		}

		public void SendStartClock () {
			List<byte> update = new List<byte> ();
			update.Add (START_CLOCK);
			ApplyHeader (update, CLOCK);
			MultiPlayerController.Instance.SendMyReliable (update);
		}

		public void SendStopClock () {
			List<byte> update = new List<byte> ();
			update.Add (STOP_CLOCK);
			ApplyHeader (update, CLOCK);
			MultiPlayerController.Instance.SendMyReliable (update);
		}

		public void SendLeaderboardsUpdate (byte position) {
			List<byte> update = new List<byte> ();
			update.Add (position);
			ApplyHeader (update, LEADERBOARDS);
			MultiPlayerController.Instance.SendMyReliable (update);
		}

		private void ApplyHeader (List<byte> data, byte type) {
			HeaderManager.ApplyHeader (data, new HeaderManager.Header (PROTOCOL_VERSION, type));
		}
	}
}

