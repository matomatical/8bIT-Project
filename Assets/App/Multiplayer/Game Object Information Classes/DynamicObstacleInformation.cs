using UnityEngine;
using System.Collections;

/*
 * This class is used to store information about  dynamic obstacles.
 * 
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

namespace xyz._8bITProject.cooperace.multiplayer {
    public class DynamicObstacleInformation : MonoBehaviour {

        // The position of the obstacle
        public readonly Vector2 pos;
        public readonly Vector2 vel;

        /// Use this for initialisation
        public DynamicObstacleInformation(Vector2 pos, Vector2 vel) {
            this.pos = pos;
            this.pos = vel;
        }

        /// Check if two objects are equal
		public override bool Equals(System.Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            DynamicObstacleInformation info = obj as DynamicObstacleInformation;
            if ((System.Object)info == null) {
                return false;
            }

            // Return true if the fields match:
            return (info.pos == pos && info.vel == vel);
        }

        /// Check if two objects of type BoolObstacleInformation are equal
        public bool Equals(DynamicObstacleInformation info) {
            // If parameter is null return false:
            if ((object)info == null) {
                return false;
            }

            // Return true if the fields match:
            return (info.pos == pos && info.vel == vel);
        }
    }
}
