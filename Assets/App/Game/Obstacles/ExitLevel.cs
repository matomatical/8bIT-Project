/*
 * Exit level portal logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class ExitLevel : MonoBehaviour {

		/// Collider for detecting collisions
		BoxCollider2D box;

		void Start(){

			// link components together
			
			box = GetComponent<BoxCollider2D> ();
		}

		void OnTriggerEnter2D(Collider2D other) {
			// the game ends when the player touches the exit portal
			if (other.gameObject.CompareTag("Player")) {

				// making sure we're talking about the same level
				if (ArcadePhysics.SameLayer (other, box)) {
					
					SceneManager.Load ("PostGameMenu");

				}
			}
		}
	}
}
