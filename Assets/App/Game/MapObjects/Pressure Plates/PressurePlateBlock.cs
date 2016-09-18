/*
 * Pressure plate block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections.Generic;

namespace _8bITProject.cooperace {
	public class PressurePlateBlock : MonoBehaviour {

		[HideInInspector]
		public string address;

		public List<PressurePlate> linked;

		public void UpdateStatus() {
			// open if any pressure plate is pressed
			// else close
			bool isOpen = false;
			foreach (PressurePlate plate in linked) {
				if (plate.IsPressed()) {
					isOpen = true;
					break;
				}
			}

			gameObject.SetActive(!isOpen);
		}

	}
}
