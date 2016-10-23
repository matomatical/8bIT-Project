using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.multiplayer;
using System.Linq;

namespace xyz._8bITProject.cooperace.multiplayer.tests{
	
	public class MockMultiplayerInit : MonoBehaviour {

		public bool sender;

		// Use this for initialization
		void OnEnable () {
		
			UpdateManager updateManager = new UpdateManager ();

			// link the update manager to the clock and chat

			LinkUpdateManager (updateManager);

			// initialise the obstacles to send/receive updates

			if (sender) {
				
				InitializeObjectsSender (gameObject, updateManager);

			} else {

				MultiPlayerController.Instance.updateManager = updateManager;
				InitializeObjectsReceiver (gameObject, updateManager);
			}


			// assign serialisers unique IDs

			SetObjectIDs (gameObject);

			// set up the start lines, finish lines and finalizer

			InitializeFinalization (gameObject, updateManager);
		
		}


		private void LinkUpdateManager (UpdateManager updateManager) {

			// link clock with updateManager
			ClockController clock = gameObject.GetComponentInChildren<ClockController> ();
			updateManager.clock = clock;


			// link chat with updateManager

			ChatController chat = gameObject.GetComponentInChildren<ChatController>();

			updateManager.chatController = chat;
			if (chat != null) {
				chat.updateManager = updateManager;
			}

		}


		void InitializeObjectsSender(GameObject level, UpdateManager updateManager){


			// enable all the local players and their serializers on the sending side

			foreach (LocalPlayerController localPlayer in level.GetComponentsInChildren<LocalPlayerController>()) {
				
				localPlayer.enabled = true;
				PlayerSerializer localSerializer = localPlayer.GetComponent<PlayerSerializer> ();

				localSerializer.enabled = true;
				localSerializer.updateManager = updateManager;
			}

			// on the sender side, keys, key blocks and pressure playes should
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

			foreach (PushBlockController pbc in level.GetComponentsInChildren<PushBlockController>()){

				pbc.enabled = true;

				PushBlockSerializer pbs = pbc.GetComponent<PushBlockSerializer> ();
				pbs.enabled = true;
				pbs.updateManager = updateManager;

			}


		}

		void InitializeObjectsReceiver(GameObject level, UpdateManager updateManager){

			// enable all the local players and their serializers on the sending side

			foreach (LocalPlayerController remotePlayer in level.GetComponentsInChildren<LocalPlayerController>()) {

				remotePlayer.GetComponent<LerpingPhysicsController>().enabled = true;

				// enable the serialisers! and link them to update manager

				PlayerSerializer remoteSerializer = remotePlayer.GetComponent<PlayerSerializer> ();

				remoteSerializer.enabled = true;
				updateManager.Subscribe(remoteSerializer, UpdateManager.Channel.PLAYER);
			}


			// on the receiver side, there's no need to control keys and stuff via
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
			FinishLineSynchronizer[] finishes = level.GetComponentsInChildren<FinishLineSynchronizer> ();

			// Set up the start and finish line blocks to send updates
			foreach (StartLineSerializer start in starts) {
				start.enabled = true;	
				start.updateManager = updateManager;
			}

			foreach (FinishLineSynchronizer finish in finishes) {
				finish.enabled = true;
			}

			// set up the static state of the finaliser for a new game,
			// linking it to the update manager

//			FinalizeLevel.Init (updateManager);

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