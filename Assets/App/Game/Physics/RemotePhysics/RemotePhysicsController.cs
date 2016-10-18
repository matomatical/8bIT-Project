/*
 * Player 'two' controller, extending arcade physics to
 * follow state updates from a network or file
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public abstract class RemotePhysicsController : ArcadePhysicsController {

		protected override void Start(){

			base.Start();
		}

		public abstract void SetState (Vector2 position, Vector2 velocity);

		protected override void ChangePosition(ref Vector2 position){

		}

		protected override void ChangeVelocity(ref Vector2 velocity){

		}
	}
}