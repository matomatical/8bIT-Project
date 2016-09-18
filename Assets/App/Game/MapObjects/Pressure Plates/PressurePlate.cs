/*
 * Pressure plate logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace _8bITProject.cooperace {
	public class PressurePlate : MonoBehaviour {

		[HideInInspector]
		public string address;

		public List<PressurePlateBlock> linked;

		int isColliding;

		void UpdateBlocksStatus() {
			// notify the block that pressure plate status may have changed.
			foreach (PressurePlateBlock block in linked) {
				block.UpdateStatus();
			}
		}

		void OnTriggerEnter2D() {
			++isColliding;
			UpdateBlocksStatus();
		}

		void OnTriggerExit2D() {
			--isColliding;
			UpdateBlocksStatus();
		}

		public bool IsPressed() {
			// true if any object is colliding with the plate.
			return isColliding > 0;
		}

	}
}
