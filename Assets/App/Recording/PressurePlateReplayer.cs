using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class PressurePlateReplayer : StaticReplayer {

		private PressurePlate plate;
		
		void Start(){
			plate = GetComponent<PressurePlate>();
		}

		public override void SetState(bool state){

			if(state){
				plate.SimulatePress();
			} else {
				plate.SimulateRelease();
			}
		}
	}
}