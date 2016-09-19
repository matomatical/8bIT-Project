/*
 * Key game object logic.
 * Contains the actual collision handler.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace _8bITProject.cooperace {
	public class Key : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D other) {
			// if the key collides with another game object that has a
			// KeyHolder component that isn't already holding a key object,
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && !holder.holdingKey) {
				// then mark that object as holding a key and delete self
				holder.holdingKey = true;
				Destroy(gameObject);
			}
		}

	}
}
