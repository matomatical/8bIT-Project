/*
 * Raycaster provides easy 2D raycasting helper methods
 * for objects with a box-collider.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	public class Raycaster {

		public const float skinWidth = 0.015f;
		public const float raySpacing = 0.25f;

		private struct RaycastOrigins {
			public Vector2 bottomLeft, bottomRight;
			public Vector2 topLeft, topRight;
		}

		RaycastOrigins origins;

		int rayCountHorizontal;
		int rayCountVertical;

		float raySpacingHorizontal;
		float raySpacingVertical;

		BoxCollider2D box;

		public Raycaster (BoxCollider2D box) {

			this.box = box;

			// calculate ray origins and ray spacing
			
			UpdateRayOrigins ();
			
			CalculateRaySpacing ();
		
		}



		void CalculateRaySpacing() {

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

		/// Update the corners to use for raycasting. Should always be 
		/// called when position changes before raycasting at all
		public void UpdateRayOrigins(){

			Bounds bounds = box.bounds;
			bounds.Expand(skinWidth * -2);

			origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
			origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
			origins.topRight = new Vector2(bounds.max.x, bounds.max.y);

		}

		/// <summary>
		/// Cast vertical rays and get resulting hits back
		/// </summary>
		/// <returns>Raycast hits</returns>
		/// <param name="ray">Magnitude represents length, sign represents
		/// right (+) or left (-) raycast</param>
		/// <param name="layers">Layermask to check for collisions under</param>
		/// <param name="offset">Default 0, if specified: translate rays up
		/// this amount first</param>
		/// <param name="decreasing">Whether to reduce ray length upon hits</param>
		public RaycastHit2D[] RaycastHorizontal(float ray, LayerMask layers,
				float offset = 0, bool decreasing = true) {

			List<RaycastHit2D> hits = new List<RaycastHit2D> ();

			// where to cast rays?

			float direction = Mathf.Sign(ray);
			float magnitude = Mathf.Abs(ray) + skinWidth;

			// cast the rays (bottom up)

			for(int i = 0; i < rayCountHorizontal; i++){

				Vector2 origin = (direction > 0) ?
					origins.bottomRight : origins.bottomLeft;
				origin.y += i * raySpacingHorizontal;
				origin.y += offset;

				// draw black ray to see direction
				Debug.DrawRay(origin,
					Vector2.right * direction, Color.black);
				
				// draw red ray to see magnitude
				Debug.DrawRay(origin,
					Vector2.right * direction * magnitude, Color.red);


				// cast the actual ray!

				RaycastHit2D hit = Physics2D.Raycast(origin,
					Vector2.right * direction, magnitude, layers);

				// if there's something and we're layering, we'd better check
				// all hits to make sure we're in the right layer

				if(hit && ArcadePhysics.layering){
				
					if (!ArcadePhysics.SameLayer (hit.collider, this.box)) {
						// check ALL hits

						RaycastHit2D[] all = Physics2D.RaycastAll(origin,
							Vector2.right * direction, magnitude, layers);

						// find the closest hit in our physics layer
						bool first = true;
						foreach (RaycastHit2D r in all) {
							if(first || r.distance < hit.distance){
								if(ArcadePhysics.SameLayer(r.collider, this.box)){
									hit = r;
									first = false;
								}
							}
						}
					}
				}

				if(hit && (!ArcadePhysics.layering || ArcadePhysics.SameLayer(hit.collider, this.box))) {
					// if we're decreasing,
					// update magnitude for remaining raycasts
					if(decreasing){
						magnitude = hit.distance;
					}

					// remove the skinWidth from the hit
					hit.distance -= skinWidth;

					// then, add to the array of hits to be returned
					hits.Add(hit);
				}

				// draw green ray, AFTER raycast to see truncation

				Debug.DrawRay(origin,
					Vector2.right * direction * magnitude, Color.green);

			}

			return hits.ToArray ();

		}

		/// <summary>
		/// Cast vertical rays and get resulting hits back
		/// </summary>
		/// <returns>Raycast hits</returns>
		/// <param name="ray">Magnitude represents length, sign represents
		/// up (+) or down (-) raycast</param>
		/// <param name="layers">Layermask to check for collisions under</param>
		/// <param name="offset">Default 0, if specified: translate rays right
		/// this amount</param>
		/// <param name="decreasing">Whether to reduce ray length upon hits</param>
		public RaycastHit2D[] RaycastVertical(float ray, LayerMask layers,
				float offset = 0, bool decreasing = true) {

			List<RaycastHit2D> hits = new List<RaycastHit2D> ();

			// where to cast rays?

			float direction = Mathf.Sign(ray);
			float magnitude = Mathf.Abs(ray) + skinWidth;

			// cast the rays (left-to-right)

			for(int i = 0; i < rayCountVertical; i++){
				
				Vector2 origin = (direction > 0) ?
					origins.topLeft : origins.bottomLeft;
				origin.x += i * raySpacingVertical;
				origin.x += offset;

				// draw ray to see direction
				Debug.DrawRay(origin, Vector2.up * direction, Color.black);
				// draw ray to see magnitude
				Debug.DrawRay(origin, Vector2.up * direction * magnitude,
					Color.red);



				// cast an actual ray!

				RaycastHit2D hit = Physics2D.Raycast (origin,
					Vector2.up * direction, magnitude, layers);

				// if there's something and we're layering, we'd better check
				// all hits to make sure we're in the right layer

				if(hit && ArcadePhysics.layering){

					if (!ArcadePhysics.SameLayer (hit.collider, this.box)) {
						// check ALL hits

						RaycastHit2D[] all = Physics2D.RaycastAll (origin,
							Vector2.up * direction, magnitude, layers);

						// find the closest hit in our physics layer
						bool first = true;
						foreach (RaycastHit2D r in all) {
							if(first || r.distance < hit.distance){
								if(ArcadePhysics.SameLayer(r.collider, this.box)){
									hit = r;
									first = false;
								}
							}
						}
					}
				}

				if(hit && (!ArcadePhysics.layering || ArcadePhysics.SameLayer(hit.collider, this.box))) {
					// if we're decreasing,
					// update magnitude for remaining raycasts
					if(decreasing){
						magnitude = hit.distance;
					}

					// remove the skinWidth from the hit
					hit.distance -= skinWidth;

					// then, add to the array of hits to be returned
					hits.Add(hit);
				}

				// draw one more ray, AFTER raycast to see truncation

				Debug.DrawRay(origin,
					Vector2.up * direction * magnitude, Color.green);

			}

			return hits.ToArray();

		}

		/// Return all raycast results. always non-decreasing.
		public RaycastHit2D[] RaycastAllHorizontal(float ray,
			LayerMask layers, float offset = 0) {

			List<RaycastHit2D> hits = new List<RaycastHit2D> ();

			// where to cast rays?

			float direction = Mathf.Sign(ray);
			float magnitude = Mathf.Abs(ray) + skinWidth;

			// cast the rays (bottom up)

			for(int i = 0; i < rayCountHorizontal; i++){

				Vector2 origin = (direction > 0) ?
					origins.bottomRight : origins.bottomLeft;
				origin.y += i * raySpacingHorizontal;
				origin.y += offset;

				// draw black ray to see direction
				Debug.DrawRay(origin,
					Vector2.right * direction, Color.black);

				// draw red ray to see magnitude
				Debug.DrawRay(origin,
					Vector2.right * direction * magnitude, Color.red);

				// cast rays to detect all collisions!

				RaycastHit2D[] hit = Physics2D.RaycastAll(origin,
					Vector2.right * direction, magnitude, layers);

				// add valid results to the output list

				for(int j = 0; j < hit.Length; j++) {

					if(hit[j] && (!ArcadePhysics.layering || ArcadePhysics.SameLayer(hit[j].collider, this.box))) {

						// remove the skinWidth from the hit's distance
						hit[j].distance -= skinWidth;

						// then, add to the array of hits to be returned
						hits.Add(hit[j]);
					}
				}

				// draw green ray, AFTER raycast to see truncation

				Debug.DrawRay(origin,
					Vector2.right * direction * magnitude, Color.green);

			}

			return hits.ToArray ();

		}

		/// Return all raycast results. always non-decreasing
		public RaycastHit2D[] RaycastAllVertical(float ray,
			LayerMask layers, float offset = 0) {

			List<RaycastHit2D> hits = new List<RaycastHit2D> ();

			// where to cast rays?

			float direction = Mathf.Sign(ray);
			float magnitude = Mathf.Abs(ray) + skinWidth;

			// cast the rays (left-to-right)

			for(int i = 0; i < rayCountVertical; i++){

				Vector2 origin = (direction > 0) ?
					origins.topLeft : origins.bottomLeft;
				origin.x += i * raySpacingVertical;
				origin.x += offset;

				// draw ray to see direction
				Debug.DrawRay(origin, Vector2.up * direction, Color.black);
				// draw ray to see magnitude
				Debug.DrawRay(origin, Vector2.up * direction * magnitude,
					Color.red);
				
				// cast rays to detect all collisions!

				RaycastHit2D[] hit = Physics2D.RaycastAll(origin,
					Vector2.right * direction, magnitude, layers);

				// add valid results to the output list

				for(int j = 0; j < hit.Length; j++) {

					if(hit[j] && (!ArcadePhysics.layering || ArcadePhysics.SameLayer(hit[j].collider, this.box))) {

						// remove the skinWidth from the hit's distance
						hit[j].distance -= skinWidth;

						// then, add to the array of hits to be returned
						hits.Add(hit[j]);
					}
				}

				// draw one more ray, AFTER raycast to see truncation

				Debug.DrawRay(origin,
					Vector2.up * direction * magnitude, Color.green);

			}

			return hits.ToArray();

		}
	}
}