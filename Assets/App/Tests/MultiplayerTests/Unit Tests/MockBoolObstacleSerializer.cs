/*
 * This class is a very simple implementation of BoolObstacleSerializer for
 * unit testing of the functonality offered by the superclass, so as not to
 * duplicate that testing between testing of real implementations
 * 
 * Sam Beyer 	 < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
 */

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
