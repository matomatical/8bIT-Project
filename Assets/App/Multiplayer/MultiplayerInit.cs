/*
 * Level initialisation script for setting up the
 * components in a multiplayer level!
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer      < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia  < farrugiam@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames.BasicApi.Multiplayer;

namespace xyz._8bITProject.cooperace.multiplayer
{
	public class MultiplayerInit : MonoBehaviour {


		/// welcome to the start of our multiplayer connection!
		/// today we will be setting up a level with all of
		/// its child objects and their components, so that they
		/// can send and receive updates over the network!
		public static void MultiplayerAwake(GameObject level){

			// decide which player is the host by participant ID

			string myID = MultiPlayerController.Instance.GetMyParticipantId ();
			List<Participant> participants = MultiPlayerController.Instance.GetAllPlayers ();

			// the host is the participant with the first ID in the list

			bool host = (participants [0].ParticipantId.Equals (myID));
			MultiPlayerController.Instance.host = host;

			UILogger.Log("I am the " + (host ? "host" : "client"));


			// Get localPlayer and remotePlayer gameObjects sorted

			LocalPlayerController[] players = level.GetComponentsInChildren<LocalPlayerController>();
			GameObject localPlayer;
			GameObject remotePlayer;
		
			if (host) {
				localPlayer  = players [0].gameObject;
				remotePlayer = players [1].gameObject;

			} else {
				localPlayer  = players [1].gameObject;
				remotePlayer = players [0].gameObject;

			}


			// Enter update manager, stage left!

			UpdateManager updateManager = MultiPlayerController.Instance.updateManager;


			// link clock with updateManager
			ClockController clock = FindObjectOfType<ClockController> ();
			updateManager.clock = clock;


			// link chat with updateManager

			ChatController chat = FindObjectOfType<ChatController>();

			updateManager.chatController = chat;
			chat.updateManager = updateManager;



			// camera should track local player

			CameraController camera = FindObjectOfType<CameraController> ();

			camera.target = localPlayer.GetComponent<ArcadePhysicsController>();







			// set up objects and their components!

			InitializePlayers (localPlayer, remotePlayer, updateManager);


			if (host) {
				InitializeObstaclesHost (level, updateManager);

			} else {
				InitializeObstaclesClient (level, updateManager);

			}

			// assign serialisers unique IDs

			SetObjectIDs (level);


			// set up the start lines, finish lines and finalizer

			InitializeFinalization (level, updateManager);



			// Dont forget to let our peer know our gamertag!

			updateManager.SendGamerTag ();
		}

		static void InitializePlayers(GameObject localPlayer, GameObject remotePlayer, UpdateManager updateManager){

			// enable the controller components

			localPlayer.GetComponent<LocalPlayerController> ().enabled = true;
			remotePlayer.GetComponent<LerpingPhysicsController> ().enabled = true;


			// enable the serialisers! and link them to update manager

			PlayerSerializer localSerializer = localPlayer.GetComponent<PlayerSerializer> ();

			localSerializer.enabled = true;
			localSerializer.updateManager = updateManager;


			PlayerSerializer remoteSerializer = remotePlayer.GetComponent<PlayerSerializer> ();

			remoteSerializer.enabled = true;
			updateManager.Subscribe(remoteSerializer, UpdateManager.Channel.PLAYER);

		}

		static void InitializeObstaclesHost(GameObject level, UpdateManager updateManager){

			// on the host side, keys, key blocks and pressure playes should
			// all respond to collisions, not remote updates!

			foreach (KeyController key in level.GetComponentsInChildren<KeyController>()) {

				key.enabled = true;
			}


			foreach (PressurePlateController plate in level.GetComponentsInChildren<PressurePlateController>()) {

				plate.enabled = true;
			}


			foreach (KeyBlockController block in level.GetComponentsInChildren<KeyBlockController>()) {

				block.enabled = true;
			}


			// all of these obstacles need to be set up to notify the 
			// update manager when something changes!

			foreach (BoolObstacleSerializer obstacle in level.GetComponentsInChildren<BoolObstacleSerializer>()) {

				obstacle.enabled = true;
				
				obstacle.updateManager = updateManager;
			}



			// as for push blocks, we'll need to enable their
			// normal controllers to, so that they can be pushed around
			// and we'll also set them up to 

			foreach (PushBlockController pbc in
				level.GetComponentsInChildren<PushBlockController>()){

				pbc.enabled = true;

				PushBlockSerializer pbs = pbc.GetComponent<PushBlockSerializer> ();
				pbs.enabled = true;
				pbs.updateManager = updateManager;

			}

		}

		static void InitializeObstaclesClient(GameObject level, UpdateManager updateManager){

			// on the client side, there's no need to control keys and stuff via
			// collisions! they should just respond to network updates through their
			// serializer

			foreach (BoolObstacleSerializer obstacle in level.GetComponentsInChildren<BoolObstacleSerializer>()) {

				obstacle.enabled = true;

				updateManager.Subscribe(obstacle, UpdateManager.Channel.OBSTACLE);
			}


			// push blocks are also meant to subscribe to notifications from the
			// network, and should be remote controllable

			foreach (PushBlockController pbc in level.GetComponentsInChildren<PushBlockController>()){

				LerpingPhysicsController rpc = pbc.GetComponent<LerpingPhysicsController> ();
				rpc.enabled = true;

				PushBlockSerializer pbs = pbc.GetComponent<PushBlockSerializer> ();
				pbs.enabled = true;

				updateManager.Subscribe(pbs, UpdateManager.Channel.PUSHBLOCK);

			}
		}

		static void InitializeFinalization(GameObject level, UpdateManager updateManager) {

			StartLineSerializer[] starts = level.GetComponentsInChildren<StartLineSerializer> ();
			FinishLineSerializer[] finishes = level.GetComponentsInChildren<FinishLineSerializer> ();

			// Set up the start and finish line blocks to send updates
			foreach (StartLineSerializer start in starts) {
				start.enabled = true;	
				start.updateManager = updateManager;
			}

			foreach (FinishLineSerializer finish in finishes) {
				finish.enabled = true;
				finish.updateManager = updateManager;
			}
				

			// Set up Finalize Level, linked to UpdateManager

			FinalizeLevel.updateManager = updateManager;
			FinalizeLevel.sentRequest = false;
		}


		// helper method to assign IDs to each object inside the level

		static void SetObjectIDs(GameObject level){

			// assign serialiser IDs to obstacles, in sorted
			// order to guarantee same result over network
			// (sorting by object name)

			byte id = 0;

			BoolObstacleSerializer[] bools = level
				.GetComponentsInChildren<BoolObstacleSerializer> ()
				.OrderBy(gameObject => gameObject.name ).ToArray ();

			foreach(BoolObstacleSerializer b in bools){

				b.SetID(id++);
			}

			DynamicObjectSerializer[] dynamics = level
				.GetComponentsInChildren<DynamicObjectSerializer> ()
				.OrderBy(gameObject => gameObject.name ).ToArray();

			foreach(DynamicObjectSerializer d in dynamics){

				d.SetID(id++);
			}

		}
	}
}
