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
		SpriteRenderer renderer;

		// movements slower than this amount will not cause walking animation
		public const float EPSILON = 0.05f;

		// the player object which is being represented by this sprite
		ArcadePhysicsController player;

		void Awake () {
			
			// link components

			player = GetComponent<ArcadePhysicsController> ();

			animator = GetComponent<Animator> ();
			renderer = GetComponent<SpriteRenderer> ();

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
			
			
			// update actual animator:

			animator.SetInteger("State", (int) animationState);
			renderer.flipX = animationLeft;

		}

	}
}