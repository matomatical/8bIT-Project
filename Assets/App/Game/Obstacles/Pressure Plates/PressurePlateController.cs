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

		// how many colliders are colliding with the collider?
		int isColliding = 0;

		void Start(){

			// link components together

			plate = GetComponent<PressurePlate>();

		}


		void OnTriggerEnter2D(Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {

				// only trigger if this component is on
				if (enabled) {
				
				++isColliding;

				if (isColliding == 1) { // first presser!
					plate.Press ();
				}
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {
				
				// only trigger if this component is on
				if (enabled) {

				--isColliding;

				if (isColliding < 0) { // clamp at zero
					isColliding = 0;
				}

				if (isColliding == 0) { // last releaser!
					plate.Release ();
				}
			}
		}
	}
}