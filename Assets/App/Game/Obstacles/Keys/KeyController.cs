/*
 * Key game object controller, responds to collisions
 * by other objects, picking up the key if they have an
 * empty keyholder :)
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	
	[RequireComponent (typeof(Key))]
	public class KeyController : MonoBehaviour {

		/// The key object we're controlling
		Key key;

		void Start(){

			// link components

			key = GetComponent<Key> ();

		}

		void OnTriggerEnter2D(Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {
			
				// only trigger if this component is on
				if (enabled) {
				
					// if the key collides with another game object that has a
					// KeyHolder component that isn't already holding a key object,
					KeyHolder holder = other.gameObject.GetComponent<KeyHolder> ();
					if (holder != null && holder.CanPickupKey ()) {
						// then mark that object as holding a key and deactivate self
						holder.PickupKey ();
						key.Pickup ();
					}
				}
			}
		}
	}
}