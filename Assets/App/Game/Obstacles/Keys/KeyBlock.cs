/*
 * Key block logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {

	public class KeyBlock : MonoBehaviour {

		/// are we unlocked right now?
		private bool open = false;

		/// unlock this door
		public void Open(){
			open = true;
			gameObject.SetActive(false);
		}

		/// close this door again
		public void Close(){
			open = false;
			gameObject.SetActive(true);
		}

		/// are we open?
		public bool IsOpen(){
			return open;
		}
	}
}