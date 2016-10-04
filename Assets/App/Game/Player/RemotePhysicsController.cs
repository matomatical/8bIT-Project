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

	public class RemotePhysicsController : ArcadePhysicsController {

		Vector2 externalPosition;
		Vector2 externalVelocity;

		bool positionSet = false;

		protected override void Start(){

			base.Start();

			externalPosition = transform.position;

			externalVelocity = Vector2.zero;

		}

		public void SetState(Vector2 position, Vector2 velocity){

			externalPosition = position;
			this.positionSet = false;

			externalVelocity = velocity;
		}

		protected override void ChangePosition(ref Vector2 position){

			if(!positionSet){
				position = externalPosition;
				positionSet = true;
			}
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			// TODO: experiment with continual velocity updating vs. gradual

			velocity.x = externalVelocity.x;
		}
	}
}