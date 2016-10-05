/*
 * A temporary class for controlling a remote
 * player based on the past movements of an
 * input-controlled local player
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class RemotePlayerTester : MonoBehaviour {

		/// The local player to follow
		public LocalPlayerController local;

		/// The remote player to lead
		public RemotePhysicsController remote;

		// for storing position/velocity data inside
		// an array
		private struct State {
			public Vector2 position, velocity;
		}

		/// the number of frames to store at once
		/// (follower will be n/2 frames behind)
		public int n = 128;

		// circular buffer of state data
		State[] buffer;
		int i = 0; // and current index
		
		public float probability = 1; // probability a state will be applied

		// have we started following yet? need to wait for some states first
		bool started = false;


		void Start(){

			// initialise state buffer
			buffer = new State[n];
		}

		void Update(){

			// save the leading player's state

			buffer[i].position = local.GetPosition();
			buffer[i].velocity = local.GetVelocity();


			// if we've started following,

			if(started){

				// apply a previous state with specified probability
				int j = (i + n/2) % n;
				if (Random.value <= probability) {
					remote.SetState (buffer [j].position, buffer [j].velocity);
				}
			
			} else {

				// otherwise, make sure we start as soon as we can
				started = (i+1==n);
			}

			// (circularly) increment the buffer index

			i = (i + 1) % n;
		}


	}
}