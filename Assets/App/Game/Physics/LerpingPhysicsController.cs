/*
 * An arcade physics controller that makes itself controllable
 * by remotely setting its state. It then smooths its position
 * and velocity between these states. Smoothing happens in the
 * x-direction, and y-direction updates are just applied once
 * (gravity does a pretty good job there)
 * 
 * By delaying the application of updates a small amount (via
 * the offset field), we're able to lerp (linearly interpolate)
 * between the previous state and the next one.
 * 
 * When there are no new updates, the latest update is applied
 * once, and then physics takes over.
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	public class LerpingPhysicsController : ArcadePhysicsController {
		
		/// how far back in time to sit, to avoid having to project?
		/// (the default, 0.15 sec, is 7.5 fixedupdates)
		public float offset = 0.15f;

		/// Chronological FIFO Queue of states we have recieved
		UpdateQueue<StateTime> states = new UpdateQueue<StateTime>();

		/// the most recently dequeued state; our target for lerping
		StateTime next = null;

		/// the second most recently dequeued state; our source for lerping
		StateTime last;

		/// The current time we are working with this frame
		float time;

		protected override void Start() {

			base.Start ();

			last = new StateTime (base.GetPosition(), base.GetVelocity(), 0);
		}

		public void AddState(Vector2 position, Vector2 velocity, float time) {
			states.Enqueue (new StateTime (position, velocity, time), time);
		}

		public void AddState(Vector2 position, Vector2 velocity) {
			AddState(position, velocity, Time.time);
		}


		void StateUpdate() {

			// what time is it!?

			time = Time.time - offset;

			// okay, which updates are we between?

			if (next == null) {
				next = states.Dequeue ();
			}

			while(next != null && next.time < time){
				last = next;
				next = states.Dequeue();
			}

			// draw a debug line to help us see position lerping in action

			if (next != null) {

				// figure out the local position to start drawing the line from
				Vector3 localPosition = (Vector3)last.position + (transform.position - transform.localPosition);

				// how far through this line are we?
				float progress = (time - last.time) / (next.time - last.time);

				// draw the line we are lerping along, and our progress along it
				Debug.DrawRay (localPosition, next.position - last.position, Color.cyan);
				Debug.DrawRay (localPosition, (next.position - last.position)*progress, Color.blue);
			}
		}





		protected override void ChangePosition(ref Vector2 position){

			// ChangePosition is called at the start of the physics cycle,
			// so we should start by fast forwarding to the correct states

			StateUpdate ();


			// then we can actually conduct some lerping!

			// the first time we are between these two
			// states, we'll apply the the source state
			// (note: we might change the horizontal component later)
			if (!last.papplied) {
				position = last.position;
				last.papplied = true;
			}

			// also, every update we'll continually lerp between
			// the horizontal components (if we are between states)
			if (next != null) {
				float progress = (time - last.time) / (next.time - last.time);
				position.x = Mathf.Lerp (last.position.x, next.position.x, progress);
			}
		}


		protected override void ChangeVelocity(ref Vector2 velocity){

			// At this point, we will have already fast forwarded to the
			// correct sates, so we can jump straight into lerping!

			// the first time we are between these two
			// states, we'll apply the the source state
			// (note: we might change the horizontal component later)
			if (!last.vapplied) {
				velocity = last.velocity;
				last.vapplied = true;
			}

			// also, every update we'll continually lerp between
			// the horizontal components (if we are between states)
			if (next != null) {
				float progress = (time - last.time) / (next.time - last.time);
				velocity.x = Mathf.Lerp (last.velocity.x, next.velocity.x, progress);
			}
		
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
			public bool papplied = false, vapplied = false;
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