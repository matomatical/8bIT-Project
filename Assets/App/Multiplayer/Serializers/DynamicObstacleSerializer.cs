
/*
 * The multiplayer serializer for Push block game objects
 * Used for representation of the state of the object as a list of bytes 
 * and updating the state of the object from a list of bytes.
 *
 * Mariam Shahid  < mariams@student.unimelb.edu.au >
 * Sam Beyer     < sbeyer@student.unimelb.edu.au >
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace xyz._8bITProject.cooperace.multiplayer {
    public class DynamicObstacleSerializer : MonoBehaviour,  ISerializer<DynamicObstacleInformation> {
        
        public readonly byte ID;


        public List<byte> Serialize(DynamicObstacleInformation information) {
            
            return null;
        }

        public DynamicObstacleInformation Deserialize(List<byte> update) {
            
            return new DynamicObstacleInformation(new Vector2(0, 0), new Vector2(0, 0));
        }

        public void Notify(List<byte> message) {
            
        }
    }
}
