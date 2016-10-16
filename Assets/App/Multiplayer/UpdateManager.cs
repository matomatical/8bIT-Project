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


		// The ChatController to which chat message updates should be sent
		public ChatController chatController;




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

			string msg = c.ToString() + " subscribers: [";
			foreach(IListener<List<byte>> l in subscribers[(int)c]){
				msg += l.ToString () + " ";
			}

			Debug.Log (msg + "]");
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
			List<byte> header = HeaderManager.StripHeader(data);

            try {
                try {
					
                    if (header[0] == PROTOCOL_VERSION) {
						
                        if (header[1] == PLAYER) {
                            Debug.Log("Notifying everyone on the player channel");

							// Notify player subscribers of player updates
                            NotifyAll(data, Channel.PLAYER);
                        }

						else if (header[1] == OBSTACLE) {
							Debug.Log("Notifying everyone on the obstacle channel");

							// Notify all obstacle subscribers of an obstacle update
							NotifyAll(data, Channel.OBSTACLE);
						}

						else if (header[1] == PUSHBLOCK) {
							Debug.Log("Notifying everyone on the pushblock channel");

							// Notify all obstacle subscribers of an obstacle update
							NotifyAll(data, Channel.PUSHBLOCK);
						}
				
                        else if (header[1] == CHAT && chatController != null) {
                            Debug.Log("Notifying ChatController");

							// Give chat controller the message
                            chatController.RecieveMessage(data);

                        } // Handle other types of updates in this if/else tree
                    } // Handle other protocols in this if/else tree

                } catch (Exception e) {
                    Debug.Log("Invalid update identifier");
                    throw e;
                }
            } catch (Exception e) {
                Debug.Log("Invalid Protocol");
                throw e;
            }
		}

		// Sends an update for an obstacle
		public void SendObstacleUpdate (List<byte> data)
		{
            HeaderManager.ApplyHeader(data, OBSTACLE);

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
            HeaderManager.ApplyHeader(data, PLAYER);
            if (editor) {
                HandleUpdate(data, "memes");
            } else {
                MultiPlayerController.Instance.SendMyUnreliable(data);
            }
            
			Debug.Log ("Sending player update");
        }

        // Sends an update for a pushblock
        public void SendPushBlockUpdate(List<byte> data) {
            HeaderManager.ApplyHeader(data, PUSHBLOCK);
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
            HeaderManager.ApplyHeader(data, CHAT);
			if (editor) {
				HandleUpdate(data, "myself");
			} else {
				MultiPlayerController.Instance.SendMyReliable (data);	
			}
			Debug.Log ("Sending chat message");
        }

	}
}

