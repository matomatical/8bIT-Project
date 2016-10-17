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

		// components to disable/enable upon pickup/restore

		/// The box colliders
		BoxCollider2D box;
		/// The sprite renderers
		SpriteRenderer[] sprites;

		void Start(){

			// link components

			box = GetComponent<BoxCollider2D> ();
			sprites = GetComponentsInChildren<SpriteRenderer> ();
		}

		/// Pickup this key, making it disappear from the map
		/// until it is Restore()'d
		public void Pickup(){
			taken = true;

			box.enabled = false;
			foreach (SpriteRenderer sprite in sprites) {
				sprite.enabled = false;
			}
		}

		/// Restore this key, returning it to the level until
		/// it is Pickup()'d
		public void Restore(){
			taken = false;

			box.enabled = true;
			foreach (SpriteRenderer sprite in sprites) {
				sprite.enabled = true;
			}
		}

		/// Has this key been taken right now, or is it still available?
		public bool IsTaken(){
			return taken;
		}
	}
}