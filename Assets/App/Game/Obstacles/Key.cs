/*
 * Key game object state
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	public class Key : MonoBehaviour {

		bool taken = false;

		public void Pickup(){
			taken = true;
			gameObject.SetActive(false);
		}

		public void Restore(){
			taken = false;
			gameObject.SetActive(true);
		}

		public bool IsTaken(){
			return taken;
		}
	}
}