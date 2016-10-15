﻿/*
 * Start line logic.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;

namespace xyz._8bITProject.cooperace {
	public class StartLine : MonoBehaviour {

		/// The Clock to start when we cross the line
		ClockController clock;

		void Start(){

			// link components together

			clock = FindObjectOfType<ClockController> ();
		}

		void OnTriggerEnter2D (Collider2D other) {
			
			if (this.transform.position.z == other.transform.position.z) {
				if (enabled) { // only trigger if this component is on

					// start the clock the first time a player comes through!
					if (other.gameObject.CompareTag ("Player")) {

						clock.StartTiming ();

					}

				}

			}

		}

	}
}
