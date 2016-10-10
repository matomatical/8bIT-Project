/*
 * Key game object state
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class Key : MonoBehaviour {

		/// The key begins visible.
		bool taken = false;

		/// Pickup this key, making it disappear from the map
		/// until it is Restore()'d
		public void Pickup(){
			taken = true;
			gameObject.SetActive(false);
		}

		/// Restore this key, returning it to the level until
		/// it is Pickup()'d
		public void Restore(){
			taken = false;
			gameObject.SetActive(true);
		}

		/// Has this key been taken right now, or is it still available?
		public bool IsTaken(){
			return taken;
		}
	}
}