/*
 * Unit Tests for the KeyBlockReplayer class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class KeyBlockReplayerTests {

		GameObject gameObject;
		KeyBlock keyBlock;
		KeyBlockReplayer keyBlockReplayer;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();

			keyBlock = gameObject.AddComponent<KeyBlock>();
			gameObject.AddComponent<BoxCollider2D>();
			gameObject.AddComponent<SpriteRenderer>();
			keyBlock.Start();

			keyBlockReplayer = gameObject.AddComponent<KeyBlockReplayer>();
			keyBlockReplayer.Start();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void SetStateToTrueOpensKeyBlock() {
			keyBlockReplayer.SetState(true);
			bool stateIsOpen = keyBlock.IsOpen();

			Assert.True(stateIsOpen);
		}

		[Test]
		public void SetStateToFalseClosesKeyBlock() {
			keyBlockReplayer.SetState(false);
			bool stateIsOpen = keyBlock.IsOpen();

			Assert.False(stateIsOpen);
		}

	}
}
