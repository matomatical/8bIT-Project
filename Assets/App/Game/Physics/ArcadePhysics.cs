/*
 * Class for Arcade Physics static helper methods
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {

	public class ArcadePhysics {

		public static bool layering = false;

		/// Are these two colliders within the same physics layer?
		/// If ArcadePhysics.layering is turned on, this method will 
		/// compare the top-level parent objects of the two
		/// objects provided (by their collider)
		/// If a and b are children of the same top-level object,
		/// (e.g. a level) this will return true. Otherwise, it will
		/// return false.
		/// If layering is turned off (ArcadePhysics.layering = false)
		/// then it will return true every time
		public static bool SameLayer(Collider2D a, Collider2D b){
			if (layering) {
				return a.transform.root == b.transform.root;
			} else {
				return true;
			}
		}
			
	}

}