/*
 * Exit level portal logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace _8bITProject.cooperace {
	public class ExitLevel : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D other) {
			// the game ends when the player touches the exit portal
			if (other.gameObject.CompareTag("Player")) {
				SceneManager.Load("PostGameMenu");
			}
		}

	}
}
