/*
 * Advanced camera controller with smoothing,
 * lookahead, simple lookabove, and a deadzone (focus area)
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using Tiled2Unity;
using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class CameraController : MonoBehaviour {

		// this is the level we will lock to

		public TiledMap level;

		// the bounds imposed by the level

		float minx, miny, maxx, maxy;

		// the player to follow

		public PlayerController target;
		
		// deadzone ('focus area')

		FocusArea focus;
		public Vector2 focusAreaSize = new Vector2(3, 5);

		// lookahead/above settings

		public float lookAheadDistance = 3, lookAheadTime = 0.4f;
		public float lookAboveDistance = 1, lookAboveTime = 0.2f;

		// movements slower than this amount will not cause camera lookahead
		public const float EPSILON = 0.1f;

		float currentLookAhead, goalLookAhead;
		float lookAheadDirection;
		bool lookAheadStopped;

		float smoothDampAheadVariable;
		float smoothDampAboveVariable;

		void Start() {

			// set up level lock

			Camera camera = GetComponent<Camera> ();

			// helper values

			float camerax = camera.orthographicSize * Screen.width / Screen.height * 2;
			float cameray = camera.orthographicSize * 2;

			float levelx = level.GetMapWidthInPixelsScaled();
			float levely = level.GetMapHeightInPixelsScaled();

			float cornerx = level.transform.position.x;
			float cornery = level.transform.position.y;

			// actual values

			maxx = cornerx + levelx - (camerax / 2);
			minx = cornerx + (camerax / 2);

			maxy = cornery - (cameray / 2);
			miny = cornery - levely + (cameray / 2);

			// set up deadzone ('focus area')

			focus = new FocusArea(target.BoxCollider().bounds, focusAreaSize);
		}

		void LateUpdate(){

			focus.Update(target.BoxCollider().bounds);

			Vector2 goal = focus.bounds.center + Vector3.up * lookAboveDistance;

			// handle lookAbove (the easy one)

			goal.y = Mathf.SmoothDamp(transform.position.y,
				goal.y, ref smoothDampAboveVariable, lookAboveTime);

			// and lookAhead (a little less simple)

			if(focus.shift.x != 0) {
				
				if (Mathf.Abs (focus.shift.x) / Time.deltaTime > EPSILON) {
					lookAheadDirection = Mathf.Sign(focus.shift.x);
					goalLookAhead = lookAheadDirection * lookAheadDistance;
					lookAheadStopped = false;
				} else {
					if (!lookAheadStopped) {
						goalLookAhead = currentLookAhead + (lookAheadDirection
							* lookAheadDistance - currentLookAhead) / 4;
						lookAheadStopped = true;
					}
				}
			}

			currentLookAhead = Mathf.SmoothDamp(currentLookAhead,
				goalLookAhead, ref smoothDampAheadVariable, lookAheadTime);
			goal.x += currentLookAhead;


			// apply these movements, locking to the level

			Vector3 position;

			position.x = Mathf.Clamp(goal.x, minx, maxx);
			position.y = Mathf.Clamp(goal.y, miny, maxy);
			position.z = transform.position.z; // Don't mess with the Z

			// change the actual camera position

			transform.position = position;
		}

		// show the focus area as a gizmo

		void OnDrawGizmos() {
			Gizmos.color = new Color (1, 0, 0, 0.5f);
			Gizmos.DrawCube (focus.bounds.center, focus.bounds.size);
		}

		// the area of focus

		private struct FocusArea {
			public Vector2 shift;
			public Bounds bounds;

			// set up a new focus area

			public FocusArea(Bounds target, Vector2 size) {

				bounds = target;
				bounds.SetMinMax(target.min, target.min+(Vector3)size);

				shift = Vector2.zero;
			}

			// move a focus area to capture a specified bound object

			public void Update(Bounds target){

				shift = Vector2.zero;

				if(target.min.x < bounds.min.x){
					shift.x = target.min.x - bounds.min.x;
				} else if (target.max.x > bounds.max.x) {
					shift.x = target.max.x - bounds.max.x;
				}

				if(target.min.y < bounds.min.y){
					shift.y = target.min.y - bounds.min.y;
				} else if (target.max.y > bounds.max.y) {
					shift.y = target.max.y - bounds.max.y;
				}

				bounds.center += (Vector3)shift;
			}
		}
	}
}
