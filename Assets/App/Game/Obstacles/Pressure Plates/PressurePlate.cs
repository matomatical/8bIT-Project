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


		/// Is this putton being pressed?
		public bool pressed {get; private set;}


		// sprites to show
		private SpriteRenderer spriteRenderer;
		public Sprite spriteOff, spriteOn;

		public void Start() {

			spriteRenderer = GetComponent<SpriteRenderer> ();

			spriteRenderer.sprite = spriteOff;

			pressed = false;
		}


		public void Press() {

			if (pressed == false) {

				pressed = true;

				NotifyStatusChangeToBlocks();

				spriteRenderer.sprite = spriteOn;
			}
		}

		public void Release() {

			if (pressed == true) {

				pressed = false;

				NotifyStatusChangeToBlocks();

				spriteRenderer.sprite = spriteOff;
			}

		}

		/// true if the plate is in the 'pressed' state
		public bool IsPressed() {
			
			return pressed;
		}
	}
}