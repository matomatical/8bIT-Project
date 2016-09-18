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
			if (other.gameObject.CompareTag("Player")) {
				UIHelper.GoTo("PostGameMenu");
			}
		}

	}
}
