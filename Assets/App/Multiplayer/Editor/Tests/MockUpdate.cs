﻿using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace.multiplayer.tests {
	
	public class MockUpdate : MonoBehaviour {

		PlayerSerializer serializer;

		void Start () {
			// Find the player and serializer
			GameObject player1 = GameObject.Find("Player1");
			serializer = player1.GetComponent<PlayerSerializer> ();
		}

		// Every frame, send an update to the player they should be at 0,0
		void Update () {
			serializer.Notify (serializer.Serialize(new PlayerInformation(0, 0)));
		}
	}
}