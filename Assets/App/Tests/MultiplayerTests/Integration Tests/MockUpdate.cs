using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer.tests {

	public class MockUpdate : MonoBehaviour {

		public Vector2 position, velocity;
		public PlayerSerializer serializer;

		// Every frame, send an update to a player serialiser
		// they should be at position/velocity
		void Update () {
			serializer.Notify(serializer.Serialize(new DynamicObjectInformation(position, velocity)));
		}
	}
}