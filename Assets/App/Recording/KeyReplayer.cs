using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class KeyReplayer : StaticReplayer {

		private Key key;

		void Start(){
			key = GetComponent<Key>();
		}

		public override void SetState(bool state){

			if(state){
				key.SimulateTake();
			} else {
				key.SimulateRestore();
			}
		}
		
	}
}
