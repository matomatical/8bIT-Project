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

	public class PositionLerpRemotePhysicsController : RemotePhysicsController {

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

		public override void SetState(Vector2 position, Vector2 velocity){

			states.Enqueue(new StateTime(position, velocity));
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			// as soon as a new state becomes last,
			// we should correct our velocity

			if(!last.vSet){
				velocity = last.velocity;
				last.vSet = true;
			}
		}

		protected override void ChangePosition(ref Vector2 position){

			// current time

			float time = Time.time - offset;

			// which updates are we going through?

			if (next != null && time < next.time) {

				// we still have time! lerp position

				float progress = (time - last.time) / (next.time - last.time);
				position.x = Mathf.Lerp (last.position.x, next.position.x, progress);

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

					if (!last.pSet) {
						position.x = last.position.x;
					}
				} else {
					// we found a new 'next' in the while loop, successfully
					// don't need to do anything, but this is the last case
				}
					
				// as soon as a new state becomes last, we should apply its
				// vertical position (which is not lerped - we let gravity take
				// care of the rest)

				if(!last.pSet){
					position.y = last.position.y;
					last.pSet = true;
				}
			}
		}
	}
}