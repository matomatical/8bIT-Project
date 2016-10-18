/*
 * ArcadePhysics provides advanced raycasting physics
 * for moving gameobjects, configurable by layermasks
 * for standard collisions.
 * 
 * Gameobjects should extend this class if they need to
 * control their movements in any way. They can do so by
 * overriding ChangePosition and/or ChangeVelocity.
 * This class will take care of the rest!
 *
 * If a base class needs a start or awake method, it
 * must override this class' methods, and should call
 * base.Start() or base.Awake() (as appropriate) at the
 * beginning of the call.
 *
 * ArcadePhysics also makes available a struct containing
 * collision information (base.collisions), and a
 * raycaster (base.raycaster) to help extension classes
 * perform their kinematics calculations.
 *
 * Finally, this class provides public access to its
 * kinematic state through its GetVelocity(),
 * GetPosition(), GetCollisions() and GetBoxCollider()
 *
 * Enjoy!
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof (BoxCollider2D))]

	public class ArcadePhysicsController : MonoBehaviour {

		// gravity-determining constants

		public static float jumpingTo = 4;
		public static float takesTime = 0.4f;

		// set those to determine the gravitational acceleration,

		protected static float gravity = -2*jumpingTo / (takesTime*takesTime);

		// kinematic state

		private Vector2 position;
		private Vector2 velocity;

		// collisions

		/// The layer mask to use for detecting collisions that
		/// prevent the object from moving
		/// NOTE: by default, set to layers 7 and 8
		/// (we'd like to use
		/// 	LayerMask.GetMask(new string[]{"Collision","Obstacle"});
		/// but unity won't allow this, so the defualt values are
		/// hard-coded instead, to be overriden in the inspector, anyway)
		public LayerMask collisionMask = 768;


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
			Vector2 velocity = this.velocity;

			if (velocity.x > 0 && collisions.right) {
				velocity.x = 0;
			} else if (velocity.x < 0 && collisions.left) {
				velocity.x = 0;
			}

			return velocity;
		}


		protected virtual void Awake () {

			// initialise kinematic state

			velocity = Vector2.zero;
			position = transform.localPosition;

			// link public components

			box = GetComponent<BoxCollider2D> ();

		}

		protected virtual void Start() {
			
			// initialise raycaster 

			raycaster = new Raycaster(box);

		}

		void FixedUpdate() {

			// first, get any changes to position from concrete controller

			ChangePosition (ref position);

			// sync raycast origins with current position (may have changed)

			transform.localPosition = position;
			raycaster.UpdateRayOrigins();

			// apply physics and inputs to velocity

			UpdateVelocity();

			// then, get the changes to velocity from concrete controller

			ChangeVelocity(ref velocity);

			// move with this velocity, updating collisions and velocity

			UpdatePosition(velocity * Time.deltaTime);

			// finally, apply this movement to our transform

			transform.localPosition = position;

		}

		protected virtual void ChangePosition(ref Vector2 position){
			// by default, don't change anything
		}


		// apply physics to velocity

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
		}

		protected virtual void ChangeVelocity(ref Vector2 velocity){
			// by default, don't change anything
		}

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
					- Raycaster.skinWidth, collisionMask, movement.x);
				
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