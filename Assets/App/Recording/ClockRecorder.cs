using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {


	public class ClockRecorder : MonoBehaviour, TimeRecorder {
		
		private ClockController clock;

		void Start() {
			clock = GetComponent<ClockController>();
		}

		public float GetTime(){
			return clock.GetTime();
		}
	}

}