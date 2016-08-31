using UnityEngine;
using System.Collections;

namespace Team8bITProject {
	
	public class SerializeController : MonoBehaviour {

		public float fps = 30;
		public Serializer[] objects;

		public DeserializeController ds;

		void Start() {

		}

		// how to control the time of this? not sure

		void FixedUpdate () {

			string frame = "";

			foreach (Serializer s in objects){
				frame += s.Serialize () + ";";
			}

			ds.Send (frame);
		}
	}
}