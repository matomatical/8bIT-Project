/*
 * Pressure plate logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {
	public class PressurePlate : MonoBehaviour, IAddressLinkedObject {

		/// address for linking to blocks
		string address;

		public string GetAddress() {
			return address;
		}

		public void SetAddress(string address) {
			this.address = address;
		}


		// a list of all blocks that are linked to this plate
		public List<PressurePlateBlock> linkedBlocks;

		// notify all linked blocks that pressure plate status may have changed.
		void NotifyStatusChangeToBlocks() {
			foreach (PressurePlateBlock block in linkedBlocks) {
				block.UpdateStatus();
			}
		}



		// greater than zero if anything is colliding with the pressure plate
		int isColliding = 0;

		public void Press() {
			++isColliding;
			NotifyStatusChangeToBlocks();
		}

		public void Release() {
			--isColliding;
			NotifyStatusChangeToBlocks();
			
			if(isColliding < 0){
				isColliding = 0;
			}
		}

		public bool IsPressed() {
			// true if any object is colliding with the plate.
			return isColliding > 0;
		}
	}
}