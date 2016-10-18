using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.leaderboards

namespace xyz._8bITProject.cooperace.multiplayer {
	public class FinalizeLevel : MonoBehaviour {



		public void Finalize () {
			ClockController clock = FindObjectOfType<ClockController> ();
			float time;

			clock.StopTiming ();

			clock.GetTime ();
		}
	}
}
