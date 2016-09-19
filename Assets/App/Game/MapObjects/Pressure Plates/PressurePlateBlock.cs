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
		public List<PressurePlate> linked;

		string address;

		// Called by the PressurePlate class whenever its state may have
		// changed.
		// This method checks if any linked plate is currently pressed,
		// opening if any are and closing if none are.
		public void UpdateStatus() {
			bool anyPressed = false; // true if any pressure plate is pressed
			foreach (PressurePlate plate in linked) {
				if (plate.IsPressed()) {
					anyPressed = true;
					break;
				}
			}

			gameObject.SetActive(!anyPressed);
		}

		public string GetAddress() {
			return address;
		}

		public void SetAddress(string address) {
			this.address = address;
		}

	}
}
