/*
 * Unit Tests for the ReplayingController class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class ReplayingControllerTests {
		
		GameObject gameObject;
		ReplayingController replayingController;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();
			replayingController = gameObject.AddComponent<ReplayingController>();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void IsReplayingShouldBeFalseWhenReplayingHasNotStarted() {
			Assert.False(replayingController.IsReplaying());
		}

		[Test]
		public void IsReplayingShouldBeTrueWhenReplayingHasStarted() {
			replayingController.StartReplaying();
			
			Assert.True(replayingController.IsReplaying());
		}

		[Test]
		public void IsReplayingShouldBeFalseWhenReplayingHasEnded() {
			replayingController.StartReplaying();
			
			bool stateBeforeEnd = replayingController.IsReplaying();
			replayingController.EndReplaying();
			bool stateAfterEnd = replayingController.IsReplaying();
			
			Assert.True(stateBeforeEnd);
			Assert.False(stateAfterEnd);
		}

		[Test]
		public void IsReplayingShouldBeFalseWhenReplayingIsPaused() {
			replayingController.StartReplaying();

			bool stateBeforePause = replayingController.IsReplaying();
			replayingController.PauseReplaying();
			bool stateAfterPause = replayingController.IsReplaying();
			
			Assert.True(stateBeforePause);
			Assert.False(stateAfterPause);
		}

		[Test]
		public void IsReplayingShouldBeTrueWhenReplayingIsContinued() {
			replayingController.StartReplaying();
			replayingController.PauseReplaying();

			bool stateBeforeContinue = replayingController.IsReplaying();
			replayingController.ContinueReplaying();
			bool stateAfterContinue = replayingController.IsReplaying();
			
			Assert.False(stateBeforeContinue);
			Assert.True(stateAfterContinue);
		}

		[Test]
		public void ContinuingWithoutPausingIsANoop() {
			replayingController.StartReplaying();

			bool stateBeforeContinue = replayingController.IsReplaying();
			replayingController.ContinueReplaying();
			bool stateAfterContinue = replayingController.IsReplaying();
			
			Assert.AreEqual(stateBeforeContinue, stateAfterContinue);
		}

		[Test]
		public void ContinuingAfterEndingDoesNotStartReplayingAgain() {
			replayingController.StartReplaying();
			replayingController.EndReplaying();

			bool stateBeforeContinue = replayingController.IsReplaying();
			replayingController.ContinueReplaying();
			bool stateAfterContinue = replayingController.IsReplaying();

			Assert.AreEqual(stateBeforeContinue, stateAfterContinue);
		}

	}
}
