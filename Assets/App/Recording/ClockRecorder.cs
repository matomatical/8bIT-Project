using UnityEngine;
using xyz._8bITProject.cooperace;

namespace xyz._8bITProject.cooperace.recording {

	public class ClockRecorder : TimeRecorder {
		
		private ClockController clock;

		void Start() {
			clock = GetComponent<ClockController>();
		}

		public override float GetTime(){
			return clock.GetTime();
		}
	}

}