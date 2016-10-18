/*
 * This class is a very simple implementation of BoolObstacleSerializer for
 * unit testing of the functonality offered by the superclass, so as not to
 * duplicate that testing between testing of real implementations
 * 
 * Sam Beyer 	 < sbeyer@student.unimelb.edu.au >
 * Matt Farrugia < farrugiam@student.unimelb.edu.au >
 */

using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
	public class MockDynamicObjectSerializer : DynamicObjectSerializer {
		
		private DynamicObjectInformation state;

		protected override DynamicObjectInformation GetState () {
			return state;
		}

		protected override void SetState (DynamicObjectInformation state) {
			this.state = state;
		}

		protected override void Send (List<byte> message) {
			// Doesn't need to do anything for tests
			return;
		}
	}
}
