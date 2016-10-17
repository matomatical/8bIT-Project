/*
 * Player 'two' controller, extending arcade physics to
 * follow state updates from a network or file
 * 
 * Smooths velocity of updates in the x direction. By delaying
 * updates a small amount (offset), is able to lerp (linearly
 * interpolate) between the previous state and the next one.
 * Position is corrected after each frame is passed to make
 * sure we don't accumulate error in position. When there are
 * no updates, the most recent velocity is maintained.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	public class RemotePhysicsController : ArcadePhysicsController {


		// how far back in time to lag? (default = 0.1 sec = 5 fixedupdates)
		public float offset = 0.1f;

		class StateTime {
			public Vector2 position, velocity;
			public float time;
			public bool pSet = false, vSet = false;
			public StateTime(Vector2 x, Vector2 v){
				this.time = Time.time;
				this.position = x;
				this.velocity = v;
			}
		}

		private Queue<StateTime> states = new Queue<StateTime>();

		StateTime last, next;

		protected override void Start(){

			base.Start();

			last = new StateTime (GetPosition(), GetVelocity());
			last.time = 0;
			next = null;
		}

		public void SetState(Vector2 position, Vector2 velocity){

			states.Enqueue(new StateTime(position, velocity));
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			// update the time

			float time = Time.time - offset;


			if (next != null && time < next.time) {
				
				// we still have time!

				float progress = (time - last.time) / (next.time - last.time);
				velocity.x = Mathf.Lerp (last.velocity.x, next.velocity.x, progress);

			} else {
				
				// time is already past next.time! let's advance to find a new next

				while (next != null && next.time < time) {
					last = next;
					next = (states.Count > 0) ? states.Dequeue () : null;
				}


				// what if we're so far ahead that we're ahead of the
				// latest update?

				if (next == null) {
					// do nothing until we have a new update:
					next = (states.Count > 0) ? states.Dequeue () : null;

				} else {
					// we found a new 'next' in the while loop, successfully
					// don't need to do anything, but this is the last case
				}


				// as soon as a new state becomes last, we should apply its
				// vertical velocity (but let gravity take care of the rest!)

				if(!last.vSet){
					velocity.y = last.velocity.y;
					last.vSet = true;
				}
			}
		}

		public bool projecting;
		protected override void ChangePosition(ref Vector2 position){

			// as soon as a new state becomes last,
			// we should correct our position

			if(!last.pSet){
				position = last.position;
				last.pSet = true;
			}

			// last.position + (Time.time - offset - last.time) * last.velocity
			// could be more accurate, but we'd lose collision prevention if we
			// project and teleport to a non-safe position (into floor or wall)
		}
	}
}