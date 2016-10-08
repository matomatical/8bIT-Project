/*
 * KeyHolder component.
 * Any entity with this component can pickup and use keys.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class KeyHolder : MonoBehaviour {

		/// are we olding a key right now?
		bool holdingKey = false;

		/// can we use a key right now?
		public bool IsHoldingKey() {
			return holdingKey;
		}

		/// do we have space for a key?
		public bool CanPickupKey() {
			return !holdingKey;
		}

		/// take a key
		public void PickupKey() {
			holdingKey = true;
		}

		/// use a key
		public void DropKey() {
			holdingKey = false;
		}
	}
}
