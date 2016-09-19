/*
 * This is the class all ingoing and outgoing updates pass through
 * It attatches and detatches a header to the update telling the game about what will be in the update
 * It is also responsible for deciding whether 
 * 
 * Mariam Shaid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 * 
*/
using System;
using UnityEngine;
using System.Collections.Generic;

namespace _8bITProject.cooperace.multiplayer
{
	public class UpdateManager : IUpdateManager, IObservable<List<byte>>
	{
		// The protcol being used to attatch the header
		public static readonly byte PROTOCOL_VERSION = 0;
		// Obstacle update identifier
		public static readonly byte OBSTACLE = BitConverter.GetBytes ('o')[0];
		// Player update identifier
		public static readonly byte PLAYER = BitConverter.GetBytes ('p')[0];
		// Chat update identifier
		public static readonly byte CHAT = BitConverter.GetBytes ('t')[0];

		// List of subscribers
		private List<IListener<List<byte>>> subscribers = new List<IListener<List<byte>>> ();

		// Takes an update and the sender and then distributes the update to subscribers
		public void HandleUpdate (List<byte> data, string senderID)
		{
			Debug.Log ("Update recieved from " + senderID);
			List<byte> header = StripHeader (data);
			if (header [0] == PROTOCOL_VERSION) {
				if (header [1] == PLAYER) {
					Debug.Log ("Notifying everyone");
					NotifyAll (data);
				} // handle other types of updates in this if/else tree
			} // Handle other protocols in this if/else tree
		}

		// Sends an update for an obstacle
		public void SendObstacleUpdate (List<byte> data)
		{
			ApplyHeader (data, OBSTACLE);
			MultiplayerController.Instance.SendMyReliable (data);
			Debug.Log ("Sending obstacle update");
		}

		// Sends an update for a player
		public void SendPlayerUpdate (List<byte> data)
		{
			ApplyHeader (data, PLAYER);
			// uncomment/comment the following lines to change between in editor and real testing
			MultiplayerController.Instance.SendMyUnreliable (data);
			//HandleUpdate(data, "memes");
			Debug.Log ("Sending player update");
		}

		// Sends an update for a chat message
		public void SendTextChat (List<byte> data)
		{
			ApplyHeader (data, CHAT);
			MultiplayerController.Instance.SendMyReliable (data);
			Debug.Log ("Sending chat message");
		}

		public void Subscribe (IListener<List<byte>> o)
		{
			subscribers.Add (o);
		}

		// Strips off the header of an update, returns information contained in the header
		private List<byte> StripHeader(List<byte> data) {
			// get the protocol
			byte protocol = data[0];
			List<byte> headerInfo = new List<byte>();

			// add protocol to header and remove from message
			headerInfo.Add(protocol);
			data.RemoveAt (0);

			if (protocol == PROTOCOL_VERSION) {
				// get the update type
				headerInfo.Add (data[0]);
				// remove the update type
				data.RemoveAt (0);
			}

			return headerInfo;
		}

		// Applies a header to an upddate (data), at this point just a protocol version and update type
		private void ApplyHeader(List<byte> data, byte type) {
			data.Insert (0, PROTOCOL_VERSION);
			data.Insert(1, type);
		}

		// Notifies all subscribers of an update
		private void NotifyAll (List<byte> data) {
			foreach (IListener<List<byte>> sub in subscribers) {
				sub.Notify (data);
			}
		}
	}
}

