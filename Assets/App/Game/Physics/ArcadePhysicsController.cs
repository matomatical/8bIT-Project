/*
 * 
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof (BoxCollider2D))]

	public abstract class ArcadePhysicsController : MonoBehaviour {

		// gravity-determining constants

		public static float jumpingTo = 4;
		public static float takesTime = 0.4f;

		// set those to determine the gravitational acceleration,

		protected static float gravity = -2*jumpingTo / (takesTime*takesTime);

		// kinematic state

		private Vector2 position;
		private Vector2 velocity;

		// collisions

		public LayerMask collisionMask;

		public struct CollisionInfo {
			public bool above, below, left, right;
			public void Reset(){
				above = below = left = right = false;
			}
		}

		protected CollisionInfo collisions;

		protected Raycaster raycaster;

		private BoxCollider2D box;



		// accessors

		public BoxCollider2D GetBoxCollider(){
			return box;
		}

		public CollisionInfo GetCollisions(){
			return collisions;
		}

		public Vector2 GetPosition(){
			return position;
		}

		public Vector2 GetVelocity(){
			return velocity;
		}


		protected virtual void Awake () {
			
			// link components

			box = GetComponent<BoxCollider2D> ();

		}

		protected virtual void Start() {

			// initialise kinematic state

			velocity = Vector2.zero;
			position = transform.position;


			// initialise raycaster 

			raycaster = new Raycaster(box);

		}


		void Update() {

			// sync raycast origins with current position

			raycaster.UpdateRayOrigins();

			// apply physics and inputs to velocity

			UpdateVelocity();

			// move with this velocity, updating collisions and velocity

			UpdatePosition(velocity * Time.deltaTime);

			// finally, apply this movement to our transform

			transform.position = position;

		}

		
		// apply physics and inputs to velocity

		void UpdateVelocity(){

			// apply physics in y direction if we're falling

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


			// then, get the changes to velocity specific to this

			NewVelocity(ref velocity);
		}

		protected abstract void NewVelocity(ref Vector2 velocity);

		/// move the player, updating collisions and velocity
		void UpdatePosition(Vector2 movement){

			collisions.Reset ();

			// handle horizontal collisions

			if(movement.x != 0){

				RaycastHit2D[] hits = raycaster.RaycastHorizontal(movement.x,
					collisionMask);
				
				foreach(RaycastHit2D hit in hits) {

					// a collision has been detected, horizontally
					collisions.left  = movement.x < 0;
					collisions.right = movement.x > 0;

					// truncate movement to account
					movement.x = hit.distance * Mathf.Sign(movement.x);

				}
			}

			// handle vertical collisions

			if(movement.y != 0) {
				
				RaycastHit2D[] hits = raycaster.RaycastVertical(movement.y,
					collisionMask, movement.x);
				
				foreach(RaycastHit2D hit in hits) {

					// a collision has been detected, horizontally
					collisions.below = movement.y < 0;
					collisions.above = movement.y > 0;

					// truncate movement to account
					movement.y = hit.distance * Mathf.Sign(movement.y);

				}

			} else { // are we *sure* we're not touching the ground?
				
				RaycastHit2D[] hits = raycaster.RaycastVertical(
					- Raycaster.skinWidth, collisionMask);
				
				foreach(RaycastHit2D hit in hits) {
					
					if(hit) {
						collisions.below = true;
						break;
					}
				}
			}

			// apply this movement to our position

			position += movement;
		}

	}
}