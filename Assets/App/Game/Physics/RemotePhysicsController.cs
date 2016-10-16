/*
 * Player 'two' controller, extending arcade physics to
 * follow state updates from a network or file
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Sam Beyer <sbeyer@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using System.Diagnostics;

namespace xyz._8bITProject.cooperace {

	public class RemotePhysicsController : ArcadePhysicsController {

		private Vector2 nextPosition;
		private Vector2 nextVelocity;

		private Vector2 previousPosition;
		private Vector2 previousVelocity;

		private bool positionApplied = false;
		private bool velocityApplied = false;

		private RollingAverage rollingAverage = new RollingAverage (100);

		Stopwatch stopWatch = new Stopwatch ();

		protected override void Start(){

			base.Start();

			// Time.timeScale = 0.3f;
			// Time.fixedDeltaTime *= Time.timeScale;

			previousPosition = transform.position;
			nextPosition = transform.position;

			previousVelocity = Vector2.zero;
			nextVelocity = Vector2.zero;

			stopWatch.Start ();

		}

		public void SetState(Vector2 position, Vector2 velocity){

			// Get the time since nextPosition and nextVelocity were last set
			rollingAverage.Add (stopWatch.ElapsedMilliseconds);

			// Restart the stop watch
			stopWatch.Reset ();
			stopWatch.Start ();

			previousPosition = nextPosition;
			nextPosition = position;
			positionApplied = false;

			previousVelocity = nextVelocity;
			nextVelocity = velocity;
			velocityApplied = false;
		}

		protected override void ChangePosition(ref Vector2 position){

			if(!positionApplied){
				position = previousPosition;
				positionApplied = true;
			}
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			Vector2 lerpedVel;
			float timeToNext = rollingAverage.average;
			UnityEngine.Debug.Log (string.Format ("Lerping from 0 to {0}ms", timeToNext));

			// Get the time since we got the last update
			long elapsed = stopWatch.ElapsedMilliseconds;

			// Only lerp if there is time between updates
			if (timeToNext != 0) {

				// Lerp between previous velocity and the next one
				lerpedVel = Vector2.Lerp (previousVelocity, nextVelocity, elapsed / timeToNext);

				// Update the velocity
				velocity.x = lerpedVel.x;

				if (!velocityApplied) {
					velocity.y = previousVelocity.y;
					velocityApplied = true;
				}
			}

			// Otherwise, just update the velocity
			else {
				if (!velocityApplied) {
					velocity = previousVelocity;
					velocityApplied = true;
				}
			}
		}
	}
}