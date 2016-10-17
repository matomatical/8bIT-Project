/*
 * Key block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class KeyBlock : MonoBehaviour {

		/// are we unlocked right now?
		private bool open = false;


		// components to disable/enable upon close/open

		/// The box colliders
		BoxCollider2D box;
		/// The sprite renderers
		SpriteRenderer sprite;

		void Start(){

			// link components

			box = GetComponent<BoxCollider2D> ();
			sprite = GetComponent<SpriteRenderer> ();
		}


		/// unlock this door
		public void Open(){
			open = true;

			box.enabled = false;
			sprite.enabled = false;
		}

		/// close this door again
		public void Close(){
			open = false;

			box.enabled = true;
			sprite.enabled = true;
		}

		/// are we open?
		public bool IsOpen(){
			return open;
		}
	}
}