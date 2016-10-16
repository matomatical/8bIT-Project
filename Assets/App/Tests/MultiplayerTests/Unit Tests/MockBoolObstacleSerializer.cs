using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
	public class MockBoolObstacleSerializer : BoolObstacleSerializer {
		private bool state;
		public override bool GetState () {
			return state;
		}
		protected override void SetState (bool state) {
			this.state = state;
		}
	}
}
