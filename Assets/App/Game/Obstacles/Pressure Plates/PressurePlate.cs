/*
 * Pressure plate logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {
	public class PressurePlate : MonoBehaviour, IAddressLinkedObject {

		// a list of all blocks that are linked to this plate
		public List<PressurePlateBlock> linkedBlocks;

		string address;

		// notify all linked blocks that pressure plate status may have changed.
		void NotifyStatusChangeToBlocks() {
			foreach (PressurePlateBlock block in linkedBlocks) {
				block.UpdateStatus();
			}
		}

		// greater than zero if anything is colliding with the pressure plate
		int isColliding;

		void OnTriggerEnter2D() {
			++isColliding;
			NotifyStatusChangeToBlocks();
		}

		void OnTriggerExit2D() {
			--isColliding;
			NotifyStatusChangeToBlocks();
		}

		public bool IsPressed() {
			// true if any object is colliding with the plate.
			return isColliding > 0;
		}

		public string GetAddress() {
			return address;
		}

		public void SetAddress(string address) {
			this.address = address;
		}

	}
}
