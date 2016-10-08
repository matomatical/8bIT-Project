/*
 * A concrete replayer object for controlling a pressure plate to follow along 
 * with a recording
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	[RequireComponent (typeof (PressurePlate))]
	public class PressurePlateReplayer : StaticReplayer {

		/// the pressure plate to control
		private PressurePlate plate;
		
		void Start(){

			// link components
			
			plate = GetComponent<PressurePlate>();
		}

		/// update the pressure plate's state to match this state
		public override void SetState(bool state){

			if(state){
				plate.Press();
			} else {
				plate.Release();
			}
		}
	}
}