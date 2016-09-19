/*
 * Key block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace _8bITProject.cooperace {
	public class KeyBlock : MonoBehaviour {

		void OnCollisionEnter2D(Collision2D other) {
			// if the block collides with another game object that has a
			// KeyHolder component that is holding a key object,
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && holder.holdingKey) {
				// open (deactivate self) and mark the object as no longer
				// holding a key
				holder.holdingKey = false;
				gameObject.SetActive(false);
			}
		}

	}
}
