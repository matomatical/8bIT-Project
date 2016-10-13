/*
 * Logic for push blocks mediating between local
 * pushers and remote state updates, to mitigate
 * pusher delay while pushers update across a network
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent(typeof(BoxCollider2D))]
	public class RemoteBlockController : PushBlockController {

		Vector2 externalPosition;
		Vector2 externalVelocity;

		bool positionSet = false;
		bool setVelocity = false;

		private BoxCollider2D box;

		protected override void Start(){

			base.Start();

			externalPosition = transform.position;
			externalVelocity = Vector2.zero;

			// link components

			box = GetComponent<BoxCollider2D> ();

		}

		public void SetState(Vector2 position, Vector2 velocity){

			externalPosition = position;
			positionSet = false;

			externalVelocity = velocity;
			setVelocity = false;
		}

		protected override void ChangePosition(ref Vector2 position){

			if(!positionSet){
				// we are not where we should be!
				// we should be in: externalPosition
				// but, we are in:  position

				// if someone is pushing us, that's probably alright

				Vector2 testVelocity = Vector2.zero;
				base.ChangeVelocity(ref testVelocity);

				if(testVelocity.x != 0){
					// we're being pushed! so let's just wait before we move
					// (do nothing)

				} else if (externalVelocity.x == 0) {
					// we're out of position, but the block is not still
					// being pushed on the network

					// is it safe to move?
					
					// find the rectangle we are in

					float diagonal = box.bounds.size.magnitude;
					float half = 0.5f * (diagonal - Raycaster.skinWidth)
																	/ diagonal;

					Vector2 topRight = box.bounds.center
													+ half * box.bounds.size;
					Vector2 bottomLeft = box.bounds.center
													- half * box.bounds.size;

					// and where will we end up?

					topRight += (externalPosition - position);
					bottomLeft += (externalPosition - position);

					// is it safe to move there?

					LayerMask mask = LayerMaskAnd(
							base.pushersMask, base.collisionMask);

					Collider2D collider = Physics2D.OverlapArea(
							topRight, bottomLeft, mask);

					if(collider == null) {
						
						// it's safe! move there

						position = externalPosition;
						positionSet = true;

						// Set the velocity this frame, too!
						setVelocity = true;
					}
				} else {
					// we're being told to move, but we're not
					// being pushed on this side? if that's the case, wait
					// for someone to push us, or the remote to stop
					// (do nothing)
				}
			}
		}

		private LayerMask LayerMaskAnd(LayerMask a, LayerMask b) {
			return (LayerMask)(a.value & b.value);
		}

		protected override void ChangeVelocity(ref Vector2 velocity){

			// sometimes we'll use the remote velocity,
			// otherwise, respond like usual

			if(setVelocity){
				velocity = externalVelocity;
				setVelocity = false;

			} else {
				base.ChangeVelocity(ref velocity);

			}
		}
	}
}