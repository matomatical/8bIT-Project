/*
 * Unit Tests for the KeyBlockRecorder class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class KeyBlockRecorderTests {

		GameObject gameObject;
		KeyBlock keyBlock;
		KeyBlockRecorder keyBlockRecorder;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();

			keyBlock = gameObject.AddComponent<KeyBlock>();
			gameObject.AddComponent<BoxCollider2D>();
			gameObject.AddComponent<SpriteRenderer>();
			keyBlock.Start();

			keyBlockRecorder = gameObject.AddComponent<KeyBlockRecorder>();
			keyBlockRecorder.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void OpenStateIsRecorded() {
			keyBlock.Open();
			keyBlockRecorder.CheckForChanges();
			bool stateIsOpen = keyBlockRecorder.LastState();

			Assert.True(stateIsOpen);
		}

		[Test]
		public void CloseStateIsRecorded() {
			keyBlock.Close();
			keyBlockRecorder.CheckForChanges();
			bool stateIsOpen = keyBlockRecorder.LastState();

			Assert.False(stateIsOpen);
		}

	}
}
