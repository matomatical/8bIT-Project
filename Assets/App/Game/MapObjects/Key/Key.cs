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
			KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
			if (holder != null && !holder.holdingKey) {
				holder.holdingKey = true;
				Destroy(gameObject);
			}
		}

	}
}
