/*
 * 
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof (InputManager))]
	public class PlayerController : ArcadePhysicsController {

		// player-specific jumping constants

		public float maxJumpHeight = 4;
		public float minJumpHeight = 1;

		float maxJumpSpeed;
		float minJumpSpeed;

		// horizontal movement variables

		public float maxMoveSpeed = 6;
		public float accelTimeAirborne = 0.2f;
		public float accelTimeGrounded = 0.1f;

		private float smoothDampVariable;


		// input management

		private InputManager inputManager;

		private InputManager.Inputs old;

		protected override void Awake () {
		
			base.Awake ();

			// link components

			inputManager = GetComponent<InputManager> ();

		}

		protected override void Start () {
			
			base.Start ();

			maxJumpSpeed = Mathf.Sqrt ( - 2 * gravity * maxJumpHeight );
			minJumpSpeed = Mathf.Sqrt ( - 2 * gravity * minJumpHeight );

		}


		protected override void NewVelocity(ref Vector2 velocity){

			// query inputs

			InputManager.Inputs inputs = inputManager.GetInputs();


			// apply input in x direction

			float target = inputs.horizontal * maxMoveSpeed;
			
			if(collisions.below){ 	// grounded
				
				velocity.x = Mathf.SmoothDamp(velocity.x, 
					target, ref smoothDampVariable, accelTimeGrounded);
				

			} else { 				// airborne
				
				velocity.x = Mathf.SmoothDamp(velocity.x, 
					target, ref smoothDampVariable, accelTimeAirborne);
			}


			// apply input in y-direction

			if(inputs.up){ // jump!

				if(collisions.below){ // can jump
					velocity.y = maxJumpSpeed;
				}
			}

			if(old.up && !inputs.up){ // stopped jumping!
				// stunt the jump, if we need to
				velocity.y = Mathf.Min (minJumpSpeed, velocity.y);
			}
			old = inputs;
		}
	}
}