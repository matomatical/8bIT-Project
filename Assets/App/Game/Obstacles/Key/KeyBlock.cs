/*
 * Key block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class KeyBlock : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D other) {
			// if the block collides with another game object that has a
			// KeyHolder component that is holding a key object,
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && holder.isHoldingKey()) {
				// open (deactivate self) and mark the object as no longer
				// holding a key
				holder.dropKey();
				gameObject.SetActive(false);
			}
		}

		public bool IsOpen(){
			return ! gameObject.activeSelf;
		}

	}
}
