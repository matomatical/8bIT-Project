/*
 * Simple class to test playing against a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {
	class PlayAgainstTester : MonoBehaviour {
	
		void Start(){

			// shade mesh renderer

			MeshRenderer renderer = GetComponentInChildren<MeshRenderer> ();
			renderer.material.color = new Color (0.75f, 0.75f, 0.75f);

		}
	
	}
}