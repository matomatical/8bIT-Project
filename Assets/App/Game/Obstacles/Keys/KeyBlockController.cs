/*
 * Key block controls, allowing a key block to be opened
 * by a collision with a key holder with a key
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	[RequireComponent (typeof(KeyBlock))]
	public class KeyBlockController : MonoBehaviour {

		/// the key block to control
		KeyBlock block;

		void Start(){

			// link components together

			block = GetComponent<KeyBlock>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (enabled) { // only trigger if this component is on
				// if the block collides with another game object that has a
				// KeyHolder component that is holding a key object,
				KeyHolder holder = other.gameObject.GetComponent<KeyHolder> ();
				if (holder != null && holder.IsHoldingKey ()) {
					// open (deactivate self) and mark the object as no longer
					// holding a key
					holder.DropKey ();
					block.Open ();
				}
			}
		}
	}
}