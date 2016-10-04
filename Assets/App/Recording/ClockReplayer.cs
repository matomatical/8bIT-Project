using UnityEngine;

namespace xyz._8bITProject.cooperace.recording {

	public class ClockReplayer : TimeReplayer {

		private ClockController clock;

		void Start() {
			clock = GetComponent<ClockController>();
		}

		public override void SetTime(float time){
			clock.SetTime(time);
		}
	}
}

