using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	[RequireComponent (typeof (InputManager))]
	public class PlayerController : MonoBehaviour {

		// jumping movement variables

		public float maxJumpHeight = 4;
		public float minJumpHeight = 1;
		public float timeToApex = 0.4f;
		
		float gravity;
		float maxJumpSpeed;
		float minJumpSpeed;

		// horizontal movement variables

		public float maxMoveSpeed = 6;
		public float accelTimeAirborne = 0.2f;
		public float accelTimeGrounded = 0.1f;
		private float smoothDampVariable;

		// kinematic state

		Vector2 velocity = Vector2.zero;
		CollisionInfo collisions;

		// animation state

		private enum AnimationDirection { LEFT, RIGHT }
		AnimationDirection animationDirection = AnimationDirection.RIGHT;

		private enum AnimationState { STOPPED, WALKING, JUMPING, FALLING }
		AnimationState animationState = AnimationState.STOPPED;

		Animator animator;

		// input management

		InputManager inputManager;

		void Start() {

			// perform physics calculations

			gravity = - 2 * maxJumpHeight / (timeToApex * timeToApex);
			maxJumpSpeed = - gravity * timeToApex;
			minJumpSpeed = Mathf.Sqrt (- 2 * gravity * minJumpHeight);

			// link components

			animator = GetComponent<Animator> ();
			inputManager = GetComponent<InputManager> ();
		}

		void Update() {

			// apply physics and inputs to velocity

			VelocityUpdate();

			// move with this input, updating collisions and velocity

			Move(velocity * Time.deltaTime);

			// update the animation state

			UpdateAnimator();
		}

		// track old up input to know when up is released
		// (just for this method)
		private bool oldUp;

		// apply physics and inputs to velocity
		void VelocityUpdate(){
			// query inputs

			InputManager.Inputs inputs = inputManager.GetInputs();

			// apply input in x direction

			float target = inputs.horizontal * maxMoveSpeed;
			if(collisions.below){
				// grounded
				velocity.x = Mathf.SmoothDamp(velocity.x, 
					target, ref smoothDampVariable, accelTimeGrounded);

			} else {
				// airborne
				velocity.x = Mathf.SmoothDamp(velocity.x, 
					target, ref smoothDampVariable, accelTimeAirborne);
			}

			// apply input in y-direction

			if(inputs.up){
				if(collisions.below){
					// can jump
					velocity.y = maxJumpSpeed;
				}				
			}

			if(oldUp && !inputs.up){
				// up input was released! stunt jump
				velocity.y = Mathf.Min (minJumpSpeed, velocity.y);
			}
			oldUp = inputs.up;
		}

		// move the player, updating collisions and velocity
		void Move(Vector2 movement){

		}

		// update the animation state of the player
		void UpdateAnimator(){

			// which way are we facing?

			if (velocity.x < 0) {
				animationDirection = AnimationDirection.LEFT;
			} else if (velocity.y > 0) {
				animationDirection = AnimationDirection.RIGHT;
			}

			// and how are we moving?

			if(collisions.below){
				// grounded
				if(velocity.x != 0){
					animationState = AnimationState.WALKING;
				} else {
					animationState = AnimationState.STOPPED;
				}

			} else {
				// airborne
				if(velocity.y > 0){
					animationState = AnimationState.JUMPING;
				} else {
					animationState = AnimationState.FALLING;
				}
			}
			
			// update actual animator:

			// animator.SetInteger("State", A NUMBER REPRESENTING STATE);
			// also maybe FLIP ANIMATOR.TRANSFORM to reflect DIRECTION
		}

		private struct CollisionInfo {
			public bool above, below;
			public bool left, right;
			public void Reset(){
				above = below = false;
				left  = right = false;
			}
		}
	}
}