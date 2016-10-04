/*
 * Very simple mock player input class for testing.
 * Capable of giving constant left or right input.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class MockPlayerInput : MonoBehaviour {

		public InputManager inputManager;
		public bool inputLeftDown = false;
		public bool inputRightDown = false;
		public bool inputJumpDown = false;

		void Update () {
			if (inputLeftDown) {
				inputManager.VirtualKeyLeftDown ();
			} else {
				inputManager.VirtualKeyLeftUp ();
			}

			if (inputRightDown) {
				inputManager.VirtualKeyRightDown ();
			} else {
				inputManager.VirtualKeyRightUp ();
			}

			if (inputJumpDown) {
				inputManager.VirtualKeyUpDown ();
			} else {
				inputManager.VirtualKeyUpUp ();
			}
		}
	}
}