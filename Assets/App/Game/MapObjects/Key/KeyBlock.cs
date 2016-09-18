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
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && holder.holdingKey) {
				holder.holdingKey = false;
				Destroy(gameObject);
			}
		}

	}
}
