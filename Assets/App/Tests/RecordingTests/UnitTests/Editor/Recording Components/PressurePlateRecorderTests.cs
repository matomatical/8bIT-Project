/*
 * Unit Tests for the PressurePlateRecorder class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class PressurePlateRecorderTests {

		GameObject gameObject;
		PressurePlate pressurePlate;
		PressurePlateRecorder pressurePlateRecorder;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();
			
			pressurePlate = gameObject.AddComponent<PressurePlate>();
			gameObject.AddComponent<SpriteRenderer>();
			pressurePlate.Start();
			
			pressurePlateRecorder = gameObject.AddComponent<PressurePlateRecorder>();
			pressurePlateRecorder.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void PressedStateIsRecorded() {
			pressurePlate.Press();
			pressurePlateRecorder.CheckForChanges();
			bool stateIsPressed = pressurePlateRecorder.LastState();
			Assert.True(stateIsPressed);
		}

		[Test]
		public void ReleasedStateIsRecorded() {
			pressurePlate.Release();
			pressurePlateRecorder.CheckForChanges();
			bool stateIsPressed = pressurePlateRecorder.LastState();
			Assert.False(stateIsPressed);
		}

	}
}
