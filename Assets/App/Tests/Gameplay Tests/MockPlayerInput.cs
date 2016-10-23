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

		void OnEnable(){
			inputManager = FindObjectOfType<InputManager> ();

			if (inputLeftDown) {
				inputManager.VirtualKeyLeftDown ();
			}

			if (inputRightDown) {
				inputManager.VirtualKeyRightDown ();
			}

			if (inputJumpDown) {
				inputManager.VirtualKeyUpDown ();
			}

		}

		void OnDisable() {

			if (inputLeftDown) {
				inputManager.VirtualKeyLeftUp ();
			}

			if (inputRightDown) {
				inputManager.VirtualKeyRightUp ();
			}

			if (inputJumpDown) {
				inputManager.VirtualKeyUpUp ();
			}

		}
	}
}