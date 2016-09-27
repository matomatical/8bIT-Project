/*
 * KeyHolder class.
 * Any entity with this class can pickup and use keys.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class KeyHolder : MonoBehaviour {

		bool holdingKey = false;

		public bool isHoldingKey() {
			return holdingKey;
		}

		public bool canPickupKey() {
			return !holdingKey;
		}

		public void pickupKey() {
			holdingKey = true;
		}

		public void dropKey() {
			holdingKey = false;
		}

	}
}
