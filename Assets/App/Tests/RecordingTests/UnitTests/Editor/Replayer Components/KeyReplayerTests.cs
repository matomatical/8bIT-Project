/*
 * Unit Tests for the KeyReplayer class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class KeyReplayerTests {

		GameObject gameObject;
		Key key;
		KeyReplayer keyReplayer;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();

			key = gameObject.AddComponent<Key>();
			gameObject.AddComponent<BoxCollider2D>();
			key.Start();

			keyReplayer = gameObject.AddComponent<KeyReplayer>();
			keyReplayer.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void SetStateToTruePicksupKey() {
			keyReplayer.SetState(true);
			bool stateIsTaken = key.IsTaken();

			Assert.True(stateIsTaken);
		}

		[Test]
		public void SetStateToFalseRestoresKey() {
			keyReplayer.SetState(false);
			bool stateIsTaken = key.IsTaken();

			Assert.False(stateIsTaken);
		}

	}
}
