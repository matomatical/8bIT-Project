/*
 * Unit Tests for the RecordingController class!
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.recording.tests {

	[TestFixture]
	public class RecordingControllerTests {
		
		GameObject gameObject;
		RecordingController recordingController;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject();
			recordingController = gameObject.AddComponent<RecordingController>();
		}
		
		[TearDown]
		public void TearDown() {
			GameObject.DestroyImmediate(gameObject);
		}

		[Test]
		public void ThereShouldBeOnlyBeARecordingWhenRecordingHasStarted() {
			Recording before = recordingController.GetRecording();
			recordingController.StartRecording();
			Recording after = recordingController.GetRecording();
			
			Assert.IsNull(before);
			Assert.IsNotNull(after);
		}

		[Test]
		public void IsRecordingShouldBeFalseWhenRecordingHasNotStarted() {
			Assert.False(recordingController.IsRecording());
		}

		[Test]
		public void IsRecordingShouldBeTrueWhenRecordingHasStarted() {
			recordingController.StartRecording();
			
			Assert.True(recordingController.IsRecording());
		}

		[Test]
		public void IsRecordingShouldBeFalseWhenRecordingHasEnded() {
			recordingController.StartRecording();
			
			bool stateBeforeEnd = recordingController.IsRecording();
			recordingController.EndRecording(0);
			bool stateAfterEnd = recordingController.IsRecording();
			
			Assert.True(stateBeforeEnd);
			Assert.False(stateAfterEnd);
		}

		[Test]
		public void IsRecordingShouldBeFalseWhenRecordingIsPaused() {
			recordingController.StartRecording();

			bool stateBeforePause = recordingController.IsRecording();
			recordingController.PauseRecording();
			bool stateAfterPause = recordingController.IsRecording();
			
			Assert.True(stateBeforePause);
			Assert.False(stateAfterPause);
		}

		[Test]
		public void IsRecordingShouldBeTrueWhenRecordingIsContinued() {
			recordingController.StartRecording();
			recordingController.PauseRecording();

			bool stateBeforeContinue = recordingController.IsRecording();
			recordingController.ContinueRecording();
			bool stateAfterContinue = recordingController.IsRecording();
			
			Assert.False(stateBeforeContinue);
			Assert.True(stateAfterContinue);
		}

		[Test]
		public void ContinuingWithoutPausingIsANoop() {
			recordingController.StartRecording();

			bool stateBeforeContinue = recordingController.IsRecording();
			recordingController.ContinueRecording();
			bool stateAfterContinue = recordingController.IsRecording();
			
			Assert.AreEqual(stateBeforeContinue, stateAfterContinue);
		}

		[Test]
		public void ContinuingAfterEndingDoesNotStartRecordingAgain() {
			recordingController.StartRecording();
			recordingController.EndRecording(0);

			bool stateBeforeContinue = recordingController.IsRecording();
			recordingController.ContinueRecording();
			bool stateAfterContinue = recordingController.IsRecording();
			
			Assert.AreEqual(stateBeforeContinue, stateAfterContinue);
		}

		[Test]
		public void TimePassedToEndRecodingIsSavedInTheRecording() {
			float timeValue = 100;

			recordingController.StartRecording();
			recordingController.EndRecording(timeValue);

			float storedTimeValue = recordingController.GetRecording().time;
			
			Assert.AreEqual(timeValue, storedTimeValue);
		}

	}
	
}