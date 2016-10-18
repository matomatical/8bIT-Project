/*
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
	public class LerpingPhysicsController : MonoBehaviour {
		
		/// how far back in time to sit, to avoid having to project?
		/// (the default, 0.15 sec, is 7.5 fixedupdates)
		public float offset = 0.15f;

		/// Chronological FIFO Queue of states we have recieved
		UpdateQueue<StateTime> states = new UpdateQueue<StateTime>();

		// the most recently dequeued state; our target for lerping
		StateTime next = null;

		// the second most recently dequeued state; our source for lerping
		StateTime last;

		/// The remote physics controller for us to hand synthesised updates to
		RemotePhysicsController remote;

		void Start() {
			remote = GetComponent<RemotePhysicsController> ();

			last = new StateTime (remote.GetPosition(), remote.GetVelocity(), 0);
		}

		public void AddState(Vector2 position, Vector2 velocity, float time) {
			states.Enqueue (new StateTime (position, velocity, time), time);
		}

		public void AddState(Vector2 position, Vector2 velocity) {
			AddState(position, velocity, Time.time);
		}


		void FixedUpdate(){

			// what time is it!?

			float time = Time.time - offset;


			// okay, which updates are we between?

			if (next == null) {
				next = states.Dequeue ();
			}

			while(next != null && next.time < time){
				last = next;
				next = states.Dequeue();
			}


			// let's find a target state, interpolated between
			// these two updates!

			Vector2 position = remote.GetPosition ();
			Vector2 velocity = remote.GetVelocity ();

			// the first time we are between these two
			// states, we'll apply the it
			// (we might change the horizontal later)
			if (last.unapplied) {
				last.unapplied = false;

				position = last.position;
				velocity = last.velocity;
			}

			// but every update we'll continually lerp between
			// the horizontal components if we are between two
			// states
			if (next != null) {
				float progress = (time - last.time) / (next.time - last.time);

				position.x = Mathf.Lerp (last.position.x, next.position.x, progress);
				velocity.y = Mathf.Lerp (last.velocity.x, next.velocity.x, progress);
			}


			// when all is said and done, we'll actually apply
			// this calculated intermediate state to the remote
			// physics controller, and it will do the rest!

			remote.SetState (position, velocity);

		}

		/// A class representing a position/velocity state
		/// for the motion of our object at a certain time
		class StateTime {
			public readonly Vector2 position, velocity;
			public readonly float time;

			/// Create a new StateTime storing this position, velocity and time
			public StateTime(Vector2 position, Vector2 velocity, float t){
				this.time = t;
				this.position = position;
				this.velocity = velocity;
			}

			/// has this state been applied already?
			public bool unapplied = true;
		}

		/// A data structure that acts as a FIFO list of timed updates
		/// of type T. Each update is enqueued with a 'time' only if
		/// no more-recent update has already been queued.
		class UpdateQueue<T> {
			
			/// The underlying queue storing updates
			Queue<T> q = new Queue<T>();

			/// The time of the latest update enqueued
			float latest = 0;

			/// Enqueue this item only if it is more
			/// recent than the last one we enqueued
			/// Note: t must be a positive time
			public void Enqueue(T update, float time) {
				if(time > latest) {
					q.Enqueue (update);
					latest = time;
				}
			}

			/// Safely dequeue an item from the queue
			/// (returning null if the queue is empty,
			/// instead of throwing an exception)
			public T Dequeue(){
				return (q.Count > 0 ? q.Dequeue () : default(T));
			}
		}
	}
}