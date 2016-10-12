/*
 * The multiplayer serializer for Pressure Plate game objects
 * Used for representation of the state of the object as a list of bytes and updating the state from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer {
	
	[RequireComponent (typeof (PressurePlate))]
	public class PlateSerializer : BoolObstacleSerializer {

		/// The pressure plate to track
		private PressurePlate plate;

		void Start(){

			// Link components

			plate = GetComponent<PressurePlate>();
		}

		/// Get the state of the pressure plate
		public override bool GetState(){
			return plate.IsPressed();
		}

		protected override void SetState(bool state) {
			
			if (state) {
				plate.Press ();
			} else {
				plate.Release ();
			}
		}
	}
}