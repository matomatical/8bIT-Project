/*
 * 
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class PlayerTwoController : ArcadePhysicsController {

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

		protected override bool ChangePosition(ref Vector2 position){

			if(!positionSet){
				position = externalPosition;
				positionSet = true;
				return true;
			}

			return false;
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			// TODO: experiment with continual velocity updating vs. gradual

			velocity.x = externalVelocity.x;

//			if(velocity.y == 0){
//				// maybe we're stopped for a reason
//			}
//
//			if (externalVelocity.y != 0){
//				velocity.y = externalVelocity.y;
//			}

		}
	}
}