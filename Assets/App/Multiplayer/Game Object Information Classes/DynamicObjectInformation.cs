using UnityEngine;
using System.Collections;

/*
 * This class is used to store information about  dynamic obstacles.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
 */


#pragma warning disable 0659
namespace xyz._8bITProject.cooperace.multiplayer {
    public class DynamicObjectInformation {

        // The position of the obstacle
        public readonly Vector2 pos;
        public readonly Vector2 vel;
		public readonly float time;

        // Keeps track of the last update to see if anything has changed
        private DynamicObjectInformation lastInfo;

        /// Use this for initialisation
		public DynamicObjectInformation(Vector2 pos, Vector2 vel, float time) {
            this.pos = pos;
            this.vel = vel;
			this.time = time;
        }

        /// Check if two objects are equal
		public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to DynamicObjectInformation return false.
            DynamicObjectInformation info = obj as DynamicObjectInformation;
            if ((System.Object)info == null) {
                return false;
            }

            // Return true if the fields match:
			return this.Equals(info);
        }

        /// Check if two objects of type BoolObstacleInformation are equal
		/// Note: time is ignored for equality checking
        public bool Equals(DynamicObjectInformation info) {
            // If parameter is null return false, otherwise
            // Return true if the fields match:
			return (info!=null) && (info.pos == pos && info.vel == vel);
        }
    }
}
