﻿/*
 * Key bobbing animation logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 * 
 * Modified to use a sine curve and localposition and a vector
 * amplitude
 * 
 * Matthew Farrugia-Roberts <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	
	public class Bob : MonoBehaviour {

		/// How many seconds for a full cycle?
		public float period = 2;
		/// How far above/below the origin do we go?
		public Vector3 amplitude = 0.2f*Vector3.up;

		/// Keep track of when we are in the cycle
		private float t = 0;

		/// the object to bob up and down
		Transform target;
		/// where does this sprite's transform begin?
		Vector3 origin;

		void Start() {

			// link up components

			target = gameObject.transform;

			// store the initial position

			origin = target.localPosition;
		}

		void Update() {

			// what's our new phase?

			t += Time.deltaTime;
			while (t > period) { t -= period; }

			// calculate the relevant offset using Sin

			Vector3 offset = amplitude * Mathf.Sin (2 * Mathf.PI * t / period);

			// apply that offset

			target.localPosition = origin + offset;
		}

	}
}
