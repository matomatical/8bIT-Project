/*
 * Exit level portal logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class LevelBoundary : MonoBehaviour {

		void Start(){
			// need a start method to allow enabling/disabling inside the editor
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (this.transform.position.z == other.transform.position.z) {
				if (enabled) {
					
					// the game ends when the player touches the exit portal
					if (other.gameObject.CompareTag (Magic.Tags.PLAYER)) {

						SceneManager.ExitGame (ExitType.FINISH);

					}
				}
			}
		}
	}
}
