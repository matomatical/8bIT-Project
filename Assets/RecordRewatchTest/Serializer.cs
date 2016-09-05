using UnityEngine;
using System.Collections;

namespace Team8bITProject {
	public abstract class Serializer : MonoBehaviour {

		public abstract string Serialize ();

		public abstract void Deserialize (string state);

	}
}