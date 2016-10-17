/*
 * Sprite Controller keeps a player's animation state,
 * making sure it is up to date with the kinematic state
 * of the object.
 *
 * Written for the Player sprite state machine at the moment.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof (Animator))]
	[RequireComponent (typeof (SpriteRenderer))]
	
	[RequireComponent (typeof (ArcadePhysicsController))]
	
	public class PlayerSpriteController : MonoBehaviour {

		// boolean value of 'the player's sprite is facing left'

		bool animationLeft = false;

		// enum set up to cast to integer, sync'd with animator state machine

		private enum AnimationState { STOPPED = 0, WALKING = 1, JUMPING = 2 }

		AnimationState animationState = AnimationState.STOPPED;

		Animator animator;
		SpriteRenderer spriteRenderer;

		// movements slower than this amount will not cause walking animation
		public const float EPSILON = 0.05f;

		// the player object which is being represented by this sprite
		ArcadePhysicsController player;





		void Start () {
			
			// link components


			// get first active APC (there may be multiple)

			foreach(ArcadePhysicsController player in
					GetComponents<ArcadePhysicsController> ()){
				if (player.enabled) {
					this.player = player;
					break;
				}
			}

			// but there's only one animator/spriterenderer so we're good

			animator = GetComponent<Animator> ();
			spriteRenderer = GetComponent<SpriteRenderer> ();

		}





		void Update() {

			Vector2 velocity = player.GetVelocity();

			ArcadePhysicsController.CollisionInfo collisions = 
				player.GetCollisions();

			// which way are we facing?

			if (velocity.x < 0) {
				animationLeft = true;

			} else if (velocity.x > 0) {
				animationLeft = false;

			} else {
				// if we are not moving, we should preserve the existing
				// value of animationLeft, by not doing anything
			}

			// and how are we moving?

			if(collisions.below){ // grounded
				
				if(Mathf.Abs(velocity.x) > EPSILON){ // moving
					animationState = AnimationState.WALKING;

				} else { // not moving
					animationState = AnimationState.STOPPED;
				}

			} else { // airborne

				animationState = AnimationState.JUMPING;
			}
			
			
			// update actual animator and sprite:

			animator.SetInteger("State", (int) animationState);
			spriteRenderer.flipX = animationLeft;

		}

	}
}