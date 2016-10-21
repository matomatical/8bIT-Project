/*
 * Push Block Controller, extends arcade physics to
 * respond to being pushed by any number of 'player'-
 * layer objects pushing from either side.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	public class PushBlockController : ArcadePhysicsController {

		// who can push this block?

		public LayerMask pushersMask = Magic.Layers.PLAYER;

		// how fast could they push it (with one pusher?)

		public float onePusherMoveSpeed = 3;

		// enum for raycast directions

		private enum Direction { LEFT = -1, RIGHT = +1 }

		protected override void ChangeVelocity(ref Vector2 velocity){

			// is there anyone pushing from the right?

			int rightPushers = CountPushers (Direction.RIGHT);

			// how about on the left?

			int leftPushers = CountPushers (Direction.LEFT);


			// great! we can move that much

			// NOTE: left pushers push right (from the left),
			// and right pushers push left! so it's backwards:

			velocity.x = (leftPushers - rightPushers) * onePusherMoveSpeed;


			// BUT we don't want to push ourselves INTO any pushers (because
			// then those pushers would walk through us) so raycast horizntl

			if(velocity.x != 0){

				float movement = velocity.x * Time.deltaTime;
				bool hitOnce = false;

				RaycastHit2D[] hits = raycaster.RaycastHorizontal(movement,
					pushersMask);

				foreach(RaycastHit2D hit in hits) {

					// a collision has been detected
					hitOnce = true;

					// truncate movement to account
					movement = hit.distance * Mathf.Sign(movement);
				}

				if (hitOnce) {
					velocity.x = movement / Time.deltaTime;
				}
			}
		}

		// how many unique pushers in this direction?

		int CountPushers(Direction direction){

			// we want to count UNIQUE pushes, so we'll use a hashmap

			HashSet<Transform> pushers = new HashSet<Transform> ();
			int count = 0;

			// get ALL of the rayhits very close by in this direction

			RaycastHit2D[] hits = raycaster.RaycastAllHorizontal (
				Mathf.Sign((int) direction) * Raycaster.skinWidth, pushersMask);

			// sift through them counting unique pushers

			foreach (RaycastHit2D hit in hits) {
				if (!pushers.Contains (hit.transform)) {
					// hey, new guy! thanks for coming. but,
					// i better not see you around here again!
					pushers.Add (hit.transform);
					count++;
				}
			}

			return count;
		}
	}
}