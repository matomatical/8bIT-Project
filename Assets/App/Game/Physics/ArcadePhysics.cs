using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace{
	
	public class ArcadePhysics {

		/// Are these two things inside the same arcade physics layer?
		public static bool SameWorld(Component a, Component b){
			return a.transform.position.z == b.transform.position.z;
		}

		/// Are these two things inside the same arcade physics layer?
		public static bool SameWorld(GameObject a, GameObject b){
			return SameWorld (a.transform, b.transform);
		}

		/// Are these two things inside the same arcade physics layer?
		public static bool SameWorld(Component a, GameObject b){
			return SameWorld (a.transform, b.transform);
		}

		/// Are these two things inside the same arcade physics layer?
		public static bool SameWorld(GameObject a, Component b){
			return SameWorld (a.transform, b.transform);
		}
	}
}