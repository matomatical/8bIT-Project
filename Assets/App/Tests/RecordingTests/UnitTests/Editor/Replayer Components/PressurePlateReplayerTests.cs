/*
 * Unit Tests for the PressurePlateReplayer class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class PressurePlateReplayerTests {

		GameObject gameObject;
		PressurePlate pressurePlate;
		PressurePlateReplayer pressurePlateReplayer;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();
			
			pressurePlate = gameObject.AddComponent<PressurePlate>();
			gameObject.AddComponent<SpriteRenderer>();
			pressurePlate.linkedBlocks = new List<PressurePlateBlock>();
			pressurePlate.Start();
			
			pressurePlateReplayer = gameObject.AddComponent<PressurePlateReplayer>();
			pressurePlateReplayer.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void SetStateToTruePressesPressurePlate() {
			pressurePlateReplayer.SetState(true);
			bool stateIsPressed = pressurePlate.IsPressed();

			Assert.True(stateIsPressed);
		}

		[Test]
		public void SetStateToFalseReleasesPressurePlate() {
			pressurePlateReplayer.SetState(false);
			bool stateIsPressed = pressurePlate.IsPressed();

			Assert.False(stateIsPressed);
		}

	}
}
