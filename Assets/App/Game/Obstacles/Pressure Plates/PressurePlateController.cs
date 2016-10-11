/*
 * Pressure plate controller, allows nearby objects to press
 * slash activate the pressure plate.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {
	[RequireComponent (typeof(PressurePlate))]
	public class PressurePlateController : MonoBehaviour {

		/// the pressure plate to control
		PressurePlate plate;

		/// Collider for detecting collisions
		BoxCollider2D box;

		void Start(){

			// link components together

			plate = GetComponent<PressurePlate>();
			box = 	GetComponent<BoxCollider2D> ();
		}

		void OnTriggerEnter2D(Collider2D other) {
			// only trigger if this component is on and
			// inside the same physics layer
			if (enabled && ArcadePhysics.SameLayer(other, box)) {
				plate.Press ();
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			// only trigger if this component is on and
			// inside the same physics layer
			if (enabled && ArcadePhysics.SameLayer(other, box)) {
				plate.Release ();
			}
		}
	}
}