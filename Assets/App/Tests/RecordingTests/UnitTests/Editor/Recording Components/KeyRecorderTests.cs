/*
 * Unit Tests for the KeyRecorder class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class KeyRecorderTests {

		GameObject gameObject;
		Key key;
		KeyRecorder keyRecorder;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();

			key = gameObject.AddComponent<Key>();
			gameObject.AddComponent<BoxCollider2D>();
			key.Start();

			keyRecorder = gameObject.AddComponent<KeyRecorder>();
			keyRecorder.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void PickedupStateIsRecorded() {
			key.Pickup();
			keyRecorder.CheckForChanges();
			bool stateIsTaken = keyRecorder.LastState();
			Assert.True(stateIsTaken);
		}

		[Test]
		public void RestoredStateIsRecorded() {
			key.Restore();
			keyRecorder.CheckForChanges();
			bool stateIsTaken = keyRecorder.LastState();
			Assert.False(stateIsTaken);
		}

	}
}
