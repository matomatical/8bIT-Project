/*
 * 
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class PlayerTwoTester : MonoBehaviour {

		public PlayerOneController one;

		public PlayerTwoController two;

		private struct State{
			public Vector2 position, velocity;
		}

		public int n = 128;
		
		State[] buffer;
		int i = 0;
		
		// float p; // probability message will send
		bool started = false;

		void Start(){

			buffer = new State[n];

		}

		void Update(){

			buffer[i].position = one.GetPosition();
			buffer[i].velocity = one.GetVelocity();

			if(started){

				int j = (i + n/2) % n;
				two.SetState(buffer[j].position, buffer[j].velocity);

			} else if(i+1==n) {
				started = true;
				
			}

			i = (i + 1) % n;
		}


	}
}