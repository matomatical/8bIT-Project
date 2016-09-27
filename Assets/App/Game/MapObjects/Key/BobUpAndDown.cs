/*
 * Key bobbing animation logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class BobUpAndDown : MonoBehaviour {

		public float bobSpeed = 0.25f;
		public float bobOffset = 0.3f;

		float originY;
		int bobDir;

		void Start() {
			// record original position
			originY = transform.position.y;
			bobDir = 1;
		}

		void Update() {
			// reverse direction if the object moves too far
			if (transform.position.y > originY + bobOffset) {
				bobDir = -1;
			}
			if (transform.position.y < originY) {
				bobDir = 1;
			}

			// move the object up and down
			transform.position = new Vector3(
				transform.position.x,
				transform.position.y + bobDir * Time.deltaTime * bobSpeed,
				transform.position.z
			);
		}

	}
}
