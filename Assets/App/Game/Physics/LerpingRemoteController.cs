/*
 * Player 'two' controller, extending arcade physics to
 * follow state updates from a network or file, lerping
 * through positions
 * 
 * ...
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof(RemotePhysicsController))]
	public class LerpingRemoteController : MonoBehaviour {
		
		// how far back in time to lag? (default = 0.1 sec = 5 fixedupdates)
		public float offset = 0.1f;

		private Queue<StateTime> states = new Queue<StateTime>();

		// the most recently dequeued state; our target for lerping
		StateTime next = null;

		// the second most recently dequeued state; our source for lerping
		StateTime last;

		// the most recently enqueued state
		StateTime tail = null;

		RemotePhysicsController remote;

		void Start(){

			remote = GetComponent<RemotePhysicsController> ();

			last = new StateTime (remote.GetPosition(), remote.GetVelocity(), 0);
		}


		public void AddState(Vector2 position, Vector2 velocity, float time){

			// enqueue this update if we have not already received
			// a more up-to-date one

			if(tail != null && tail.time < time) {
				tail = new StateTime (position, velocity, time);
				states.Enqueue (tail);
			}
		}

		public void AddState(Vector2 position, Vector2 velocity){
			AddState(position, velocity, Time.time);
		}


		void FixedUpdate(){

			// lerp and eventually call remote.SetState();

		}

		/// class representing a position/velocity state
		/// representing the motion of our object at a
		/// certain time
		class StateTime {
			public Vector2 position, velocity;
			public float time;
			public bool pSet = false, vSet = false;
			public StateTime(Vector2 x, Vector2 v, float t){
				this.time = t;
				this.position = x;
				this.velocity = v;
			}
		}
	}
}