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

		public LayerMask pushersMask;

		// how fast could they push it (with one pusher?)

		public float onePusherMoveSpeed = 3;


		protected override void ChangeVelocity(ref Vector2 velocity){

			// is there anyone pushing from the right?

			int rightPushers = CountPushers (1);

			// how about on the left?

			int leftPushers = CountPushers (-1);


			// great! we can move that much

			// NOTE: left pushers push right (from the left),
			// and right pushers push left! so it's backwards:

			velocity.x = (leftPushers - rightPushers) * onePusherMoveSpeed;

		}

		// how many unique pushers in this direction?

		int CountPushers(int direction){

			// we want to count UNIQUE pushes, so we'll use a hashmap

			HashSet<Transform> pushers = new HashSet<Transform> ();
			int count = 0;

			// get ALL of the rayhits very close by in this direction

			RaycastHit2D[] hits = raycaster.RaycastAllHorizontal (
				Mathf.Sign(direction) * Raycaster.skinWidth, pushersMask);

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