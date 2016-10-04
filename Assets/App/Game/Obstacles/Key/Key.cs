/*
 * Key game object logic.
 * Contains the actual collision handler.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class Key : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D other) {
			// if the key collides with another game object that has a
			// KeyHolder component that isn't already holding a key object,
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && holder.canPickupKey()) {
				// then mark that object as holding a key and deactivate self
				holder.pickupKey();
				gameObject.SetActive(false);
			}
		}

		public bool IsTaken(){
			return !gameObject.activeSelf;
		}

	}
}
