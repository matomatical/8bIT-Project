using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class KeyBlockReplayer : StaticReplayer {

		private KeyBlock block;

		void Start(){
			block = GetComponent<KeyBlock>();
		}

		public override void SetState(bool state){

			if(state){
				block.Open();
			} else {
				block.Close();
			}
		}
		
	}
}