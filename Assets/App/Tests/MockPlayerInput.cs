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

		public PlayerController player;
		public bool inputLeftDown = false;
		public bool inputRightDown = false;

		void Update () {
			if (inputLeftDown) {
				player.SetInputLeft();
			} else {
				player.UnsetInputLeft();
			}

			if (inputRightDown) {
				player.SetInputRight();
			} else {
				player.UnsetInputRight();
			}
		}

	}
}