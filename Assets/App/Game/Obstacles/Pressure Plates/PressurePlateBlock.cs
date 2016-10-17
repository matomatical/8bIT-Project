/*
 * Pressure plate block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {
	public class PressurePlateBlock : MonoBehaviour, IAddressLinkedObject {

		// a list of all plates that are linked to this block
		public List<PressurePlate> linkedPlates;

		/// string address, identifying linked pressure plates
		string address;

		/// is this block open when no pressure plates are active?
		/// then it's an 'inverse' block. default: no, false
		public bool inverse = false;

		// components to disable/enable upon close/open

		/// The box colliders
		BoxCollider2D box;
		/// The sprite renderers
		SpriteRenderer sprite;

		void Start(){

			// link components

			box = GetComponent<BoxCollider2D> ();
			sprite = GetComponent<SpriteRenderer> ();

			// active if inverse is false, else deactivate

			bool enabled = (!inverse);

			box.enabled = enabled;
			sprite.enabled = enabled;

		}

		// Called by the PressurePlate class whenever its state may have
		// changed.
		// This method checks if any linked plate is currently pressed,
		// opening if any are and closing if none are.
		public void UpdateStatus() {
			bool anyPressed = false; // true if any pressure plate is pressed
			foreach (PressurePlate plate in linkedPlates) {
				if (plate.IsPressed ()) {
					anyPressed = true;
					break;
				}
			}

			// if inverse is false and anyPressed is false, or if
			// inverse is true and anyPresses is true, the blocks
			// should be active (blocking players from passing)

			bool enabled = (inverse == anyPressed);

			box.enabled = enabled;
			sprite.enabled = enabled;

		}

		public string GetAddress() {
			return address;
		}

		public void SetAddress(string address) {
			this.address = address;
		}

	}
}
