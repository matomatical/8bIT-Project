using UnityEngine;
using System.Collections;

namespace Team8bITProject {
	
	public class DeserializeController : MonoBehaviour {

		public float fps = 30;
		public Serializer[] objects;



		void Start() {

		}

		// how to control the time of this? not sure

		void FixedUpdate () {

			// load next frame

			string frame = this.frame;

			string[] words = frame.Split(';');

			for(int i = 0; i < objects.Length; i++) { 

				objects [i].Deserialize (words [i]);
				
			}

		}

		// delete

		private string frame;
		public void Send(string frame){
			this.frame = frame;
		}
	}
}

