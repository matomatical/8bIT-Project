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

	public class OriginalRemotePhysicsController : RemotePhysicsController {

		Vector2 externalPosition;
		Vector2 externalVelocity;

		bool positionSet = false;
		bool velocitySet = false;

		protected override void Start(){

			base.Start();

			externalPosition = transform.localPosition;

			externalVelocity = Vector2.zero;

		}

		public override void SetState(Vector2 position, Vector2 velocity){

			externalPosition = position;
			positionSet = false;

			externalVelocity = velocity;
			velocitySet = false;
		}

		protected override void ChangePosition(ref Vector2 position){

			if(!positionSet){
				position = externalPosition;
				positionSet = true;
			}
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			if(!velocitySet){
				velocity = externalVelocity;
				velocitySet = true;
			}

			// velocity = externalVelocity;
		}
	}
}