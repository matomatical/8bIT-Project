using UnityEngine;
using System.Collections;
using System;

namespace Team8bITProject {
	public class PlayerRecorder : Serializer {

		public override string Serialize() {
			return transform.position.x + " " + transform.position.y;
		}

		public override void Deserialize(string state) {
			string[] words = state.Split(' ');
			float newx = Single.Parse (words[0]);
			float newy = Single.Parse (words[1]);

			transform.position = new Vector3 ( newx, newy, transform.position.z );
		}
	}
}
