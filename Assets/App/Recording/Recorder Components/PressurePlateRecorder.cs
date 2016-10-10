/*
 * A concrete recorder object for collecting the state of a pressure plate
 * each frame to build a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (PressurePlate))]
	public class PressurePlateRecorder : StaticRecorder {

		/// the pressure plate to track
		private PressurePlate plate;

		void Start(){

			// link components

			plate = GetComponent<PressurePlate>();
		}

		/// get the actual state of the pressure plate being tracked this frame
		protected override bool GetState(){
			return plate.IsPressed();
		}
	}
}