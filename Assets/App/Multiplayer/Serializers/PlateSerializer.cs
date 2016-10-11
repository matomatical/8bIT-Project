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

		/// When notified, update the pressure plate this script is associated with
		public override void Notify (List<byte> message) {

			// Deserialize the message
			BoolObstacleInformation info = Deserialize (message);

			// Act on the message
			if (info.ID == this.ID) {
				if(info.state){
					plate.Press();
				} else {
					plate.Release();
				}
			}
		}
	}
}