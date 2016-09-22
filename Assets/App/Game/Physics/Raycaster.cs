/*
 * Raycaster provides 2D raycasting for objects with
 * a box-collider
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

		public void UpdateRayOrigins(){

			Bounds bounds = box.bounds;
			bounds.Expand(skinWidth * -2);

			origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
			origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
			origins.topRight = new Vector2(bounds.max.x, bounds.max.y);

		}

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

				RaycastHit2D hit = Physics2D.Raycast(origin,
						Vector2.right * direction, magnitude, layers);

				if(hit) {
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

				RaycastHit2D hit = Physics2D.Raycast(origin,
					Vector2.up * direction, magnitude, layers);

				if(hit) {
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
	}
}