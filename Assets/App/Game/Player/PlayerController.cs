/*
 * Advanced Player Controller. Detects collisions with configurable layers
 * of objects using configurable raycasting. Follows simple arcade physics,
 * controlled by an InputManager. In the future, this class will be refactored
 * to create a raycasting body for any moving objects.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

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

		Vector2 position;
		Vector2 velocity;
		CollisionInfo collisions;

		// animation state

		// boolean value of 'the player's sprite is facing left'
		bool animationLeft = false;

		// enum set up to cast to integer, sync'd with animator state machine
		private enum AnimationState { STOPPED = 0, WALKING = 1, JUMPING = 2 }
		AnimationState animationState = AnimationState.STOPPED;

		Animator animator;
		SpriteRenderer renderer;

		// movements slower than this amount will not cause walking animation
		public const float EPSILON = 0.05f;

		// input management

		InputManager inputManager;

		// raycasting

		public LayerMask collisionMask;

		public const float skinWidth = 0.015f;
		public const float raySpacing = 0.25f;

		BoxCollider2D box;
		public BoxCollider2D BoxCollider(){
			return box;
		}
		
		private struct RaycastOrigins {
			public Vector2 bottomLeft, bottomRight;
			public Vector2 topLeft, topRight;
		}

		RaycastOrigins origins;

		int rayCountHorizontal;
		int rayCountVertical;

		float raySpacingHorizontal;
		float raySpacingVertical;

		// link components
		void Awake () {
			
			animator = GetComponent<Animator> ();
			renderer = GetComponent<SpriteRenderer> ();

			inputManager = GetComponent<InputManager> ();

			box = GetComponent<BoxCollider2D> ();
		}

		void Start() {

			velocity = Vector2.zero;
			position = transform.position;

			// perform physics initialising calculations

			gravity = - 2 * maxJumpHeight / (timeToApex * timeToApex);
			maxJumpSpeed = - gravity * timeToApex;
			minJumpSpeed = Mathf.Sqrt (- 2 * gravity * minJumpHeight);


			// calculate ray origins and ray spacing

			UpdateRayOrigins();

			Bounds bounds = box.bounds;
			bounds.Expand (skinWidth * -2);

			float width = bounds.size.x;
			float height = bounds.size.y;

			
			rayCountHorizontal = Mathf.CeilToInt(height / raySpacing) + 1;
			rayCountVertical   = Mathf.CeilToInt(width  / raySpacing) + 1;
			// (minimum value of 2 - rays from corners)

			raySpacingHorizontal = height / (rayCountHorizontal - 1);
			raySpacingVertical   = width  / (rayCountVertical   - 1);
		}


		void Update() {

			// sync raycast origins with current position

			UpdateRayOrigins();

			// apply physics and inputs to velocity

			UpdateVelocity();

			// move with this input, updating collisions and velocity

			UpdatePosition(velocity * Time.deltaTime);

			// apply this movement to our transform

			transform.position = this.position;

			// finally, update the animation state

			UpdateAnimator();
		}



		void UpdateRayOrigins(){
			Bounds bounds = box.bounds;
			bounds.Expand(skinWidth * -2);

			origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
			origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
			origins.topRight = new Vector2(bounds.max.x, bounds.max.y);	
		}

		// track old up input to know when up is released
		// (used _just_ for this method)
		private bool oldUp;

		// apply physics and inputs to velocity

		void UpdateVelocity(){

			// apply physics in y direction if there's gravity,

			if (!collisions.below) {
				velocity.y = velocity.y + gravity * Time.deltaTime;
			}

			// or, land if there's not

			if (collisions.below) {
				velocity.y = 0;
			}

			// also, finish jumping if there's something above us

			if (collisions.above) {
				velocity.y = 0;
			}


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

			if(inputs.up){
				if(collisions.below){ // can jump
					velocity.y = maxJumpSpeed;
				}
			}

			if(oldUp && !inputs.up){
				// up input was released! stunt the jump, now
				velocity.y = Mathf.Min (minJumpSpeed, velocity.y);
			}
			oldUp = inputs.up;
		}

		// move the player, updating collisions and velocity
		void UpdatePosition(Vector2 movement){

			collisions.Reset ();

			// handle horizontal collisions

			if(movement.x != 0){

				// where to cast rays?

				float direction = Mathf.Sign(movement.x);
				float magnitude = Mathf.Abs (movement.x) + skinWidth;

				// cast the rays (bottom up)

				for(int i = 0; i < rayCountHorizontal; i++){

					Vector2 origin = (direction > 0) ?
						origins.bottomRight : origins.bottomLeft;
					origin.y += i * raySpacingHorizontal;

					// draw ray to see direction
					Debug.DrawRay(origin,
						Vector2.right * direction, Color.black);
					// draw ray to see magnitude
					Debug.DrawRay(origin,
						Vector2.right * direction * magnitude, Color.red);

					RaycastHit2D hit = Physics2D.Raycast(origin,
						Vector2.right * direction, magnitude, collisionMask);

					if(hit) {
						// a collision has been detected, horizontally
						collisions.left = direction < 0;
						collisions.right = direction > 0;

						// truncate movement to account
						movement.x = (hit.distance - skinWidth) * direction;
					
						// update magnitude for remaining raycasts
						magnitude = hit.distance;
					}

					// draw one more ray, AFTER raycast to see truncation
					Debug.DrawRay(origin,
						Vector2.right * direction * magnitude, Color.green);
				}
				
			}

			// handle vertical collisions

			if(movement.y != 0) {

				// where to cast rays?

				float direction = Mathf.Sign(movement.y);
				float magnitude = Mathf.Abs (movement.y) + skinWidth;

				// cast the rays (left-to-right)

				for(int i = 0; i < rayCountVertical; i++){
					Vector2 origin = (direction > 0) ?
						origins.topLeft : origins.bottomLeft;
					origin.x += i * raySpacingVertical;
					origin.x += movement.x; // cast from eventual position

					// draw ray to see direction
					Debug.DrawRay(origin,
						Vector2.up * direction, Color.black);
					// draw ray to see magnitude
					Debug.DrawRay(origin,
						Vector2.up * direction * magnitude, Color.red);

					RaycastHit2D hit = Physics2D.Raycast(origin,
						Vector2.up * direction, magnitude, collisionMask);

					if(hit) {
						// a collision has been detected, horizontally
						collisions.below = direction < 0;
						collisions.above = direction > 0;

						// truncate movement to account
						movement.y = (hit.distance - skinWidth) * direction;
					
						// update magnitude for remaining raycasts
						magnitude = hit.distance;
					}

					// draw one more ray, AFTER raycast to see truncation
					Debug.DrawRay(origin,
						Vector2.up * direction * magnitude, Color.green);
				}

			} else { // are we *sure* we're not touching the ground?
				
				for(int i = 0; i < rayCountVertical; i++){
					Vector2 origin = origins.bottomLeft
						+ Vector2.right*(i * raySpacingVertical + movement.x);

					RaycastHit2D hit = Physics2D.Raycast(origin,
						-Vector2.up, 2 * skinWidth, collisionMask);

					if(hit) {
						// a collision has been detected, horizontally
						collisions.below = true;
					}
				}
			}

			// apply this movement to our position

			position += movement;
		}

		// update the animation state of the player
		void UpdateAnimator(){

			// which way are we facing?

			if (velocity.x < 0) {
				animationLeft = true;
			} else if (velocity.x > 0) {
				animationLeft = false;
			} else {
				// if we are not moving, we should preserve the existing
				// value of animationLeft by not doing anything
			}

			// and how are we moving?

			if(collisions.below){
				// grounded
				if(Mathf.Abs(velocity.x) > EPSILON){
					animationState = AnimationState.WALKING;
				} else {
					animationState = AnimationState.STOPPED;
				}
			} else {
				// airborne
				animationState = AnimationState.JUMPING;
			}
			
			// update actual animator:

			animator.SetInteger("State", (int) animationState);
			renderer.flipX = animationLeft;
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