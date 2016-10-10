/*
 * Unit tests for all leaderboard server communication code.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.leaderboard {

	[TestFixture]
	public class LeaderboardTests {

		Leaderboards lb;
		string uniqueLevelName;

		// use to block until an asynchronous callback is finished
		AutoResetEvent callbackDone = new AutoResetEvent(false);

		[SetUp]
		public void Setup() {
			lb = new Leaderboards();

			callbackDone.Reset();

			// to make sure that tests don't interfere they should use unique
			// level names (even across multiple test runs)
			// so each test should use the uniqueLevelName helper
			uniqueLevelName = TestContext.CurrentContext.Test.Name
				+ Guid.NewGuid();
		}

		[Test]
		public void _PLEASE_MAKE_SURE_A_LOCAL_SERVER_INSTANCE_IS_RUNNING() {
			// A reminder that appears at the top of the leaderboards test list
		}

		/*
		 * Score object tests
		 */

		[Test]
		public void ScoreEquals() {
			Assert.AreEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "aaa", "bbb"));

			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(10, "aaa", "bbb"));
			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "ccc", "bbb"));
			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "aaa", "ccc"));
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreZeroTime() {
			new Score(0, "aaa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreNegativeTime() {
			new Score(-1, "aaa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreMissingPlayer1Name() {
			new Score(1, null, "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreMissingPlayer2Name() {
			new Score(1, "aaa", null);
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreToLongPlayer1Name() {
			new Score(1, "aaaa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreToShortPlayer1Name() {
			new Score(1, "aa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreToLongPlayer2Name() {
			new Score(1, "aaa", "bbbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreToShortPlayer2Name() {
			new Score(1, "aaa", "bbbb");
		}

		/*
		 * Synchronous communication tests
		 */

		[Test]
		public void SyncRequestScoresTest() {
			// Test that requesting scores work properly
			ScoresResponse r = lb.RequestScores(uniqueLevelName);

			// Confirm the server returns at least 1 score
			Assert.GreaterOrEqual(r.leaders.Length, 1);
		}

		[Test]
		public void SyncSubmittingTest() {
			// Test that submitting a score works properly
			Score submittedScore = new Score(1, "abc", "xyz");
			lb.SubmitScore(uniqueLevelName, submittedScore);

			// Confirm the top score is the one just submitted
			ScoresResponse r = lb.RequestScores(uniqueLevelName);
			Assert.AreEqual(submittedScore, r.leaders[0]);
		}

		[Test]
		public void SyncSubmitPositionTest() {
			// Test that submitting a score returns proper position
			SubmissionResponse r1 = lb.SubmitScore(uniqueLevelName, new Score(1, "abc", "xyz"));
			SubmissionResponse r2 = lb.SubmitScore(uniqueLevelName, new Score(3, "abc", "xyz"));
			SubmissionResponse r3 = lb.SubmitScore(uniqueLevelName, new Score(2, "abc", "xyz"));

			// Confirm the returned positions
			Assert.AreEqual(r1.position, 1);
			Assert.AreEqual(r2.position, 2);
			Assert.AreEqual(r3.position, 2);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void SyncUnableToConnectToServer() {
			// Test that if the server is unreachable, an exception is thrown
			lb = new Leaderboards("localhost", 9999);
			lb.RequestScores(uniqueLevelName);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void SyncInvalidServerHost() {
			// Test that if the server host is invalid, an exception is thrown
			lb = new Leaderboards("gibberish" + Guid.NewGuid(), 2693);
			lb.RequestScores(uniqueLevelName);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void SyncInvalidServerPort() {
			// Test that if the server port is invalid, an exception is thrown
			lb = new Leaderboards("localhost", -1);
			lb.RequestScores(uniqueLevelName);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SyncNullLevelNameRequest() {
			// Test that passing a null level name to RequestScores,
			// raises a ArgumentNullException
			lb.RequestScores(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SyncNullLevelNameSubmission() {
			// Test that passing a null level name to SubmitScore,
			// raises a ArgumentNullException
			lb.SubmitScore(null, new Score(1, "abc", "abc"));
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void SyncInvalidScoreSubmission() {
			// Test that passing an invalid score to SubmitScore,
			// raises a InvalidScoreException
			lb.SubmitScore(uniqueLevelName, new Score());
		}

		/*
		 * Asynchronous communication tests
		 */

		[Test]
		public void AsyncRequestScoresTest() {
			// Test that requesting scores work properly
			ScoresResponse? r = null;

			lb.RequestScoresAsync(uniqueLevelName,
				delegate (ScoresResponse response, ServerException error) {
					r = response;
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			// Confirm the server returns at least 1 score
			Assert.IsTrue(r.HasValue);
			Assert.GreaterOrEqual(r.Value.leaders.Length, 1);
		}

		[Test]
		public void AsyncSubmittingTest() {
			// Test that submitting a score works properly
			Score submittedScore = new Score(1, "abc", "xyz");

			lb.SubmitScoreAsync(uniqueLevelName, submittedScore,
				delegate (SubmissionResponse response, ServerException error) {
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			// Confirm the top score is the one just submitted
			// Note: uses synchronous RequestScores for simplicity
			ScoresResponse r = lb.RequestScores(uniqueLevelName);
			Assert.AreEqual(submittedScore, r.leaders[0]);
		}

		[Test]
		public void AsyncNullCallbackIsAllowed() {
			// Test that submitting a score works properly
			// Test that passing a null callback is okay
			Score submittedScore = new Score(1, "abc", "xyz");

			lb.SubmitScoreAsync(uniqueLevelName, submittedScore, null);
			Thread.Sleep(1000); // wait a second

			// Confirm the top score is the one just submitted
			// Note: uses synchronous RequestScores for simplicity
			ScoresResponse r = lb.RequestScores(uniqueLevelName);
			Assert.AreEqual(submittedScore, r.leaders[0]);
		}

		[Test]
		public void AsyncSubmitPositionTest() {
			// Test that submitting a score returns proper position
			SubmissionResponse? r1 = null;
			SubmissionResponse? r2 = null;
			SubmissionResponse? r3 = null;

			lb.SubmitScoreAsync(uniqueLevelName, new Score(1, "abc", "xyz"),
				delegate (SubmissionResponse response, ServerException error) {
					r1 = response;
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			lb.SubmitScoreAsync(uniqueLevelName, new Score(3, "abc", "xyz"),
				delegate (SubmissionResponse response, ServerException error) {
					r2 = response;
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			lb.SubmitScoreAsync(uniqueLevelName, new Score(2, "abc", "xyz"),
				delegate (SubmissionResponse response, ServerException error) {
					r3 = response;
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			// Confirm the returned positions
			Assert.AreEqual(1, r1.Value.position);
			Assert.AreEqual(2, r2.Value.position);
			Assert.AreEqual(2, r3.Value.position);
		}

		[Test]
		public void AsyncUnableToConnectToServer() {
			// Test that if the server is unreachable, an error returned
			// And make sure that even if the request fails, the response is
			// still valid
			lb = new Leaderboards("localhost", 9999);

			ServerException ex = null;
			ScoresResponse? r = null;

			lb.RequestScoresAsync(uniqueLevelName,
				delegate (ScoresResponse response, ServerException error) {
					r = response;
					ex = error;
					callbackDone.Set();
				});
			callbackDone.WaitOne();

			// make sure an error was returned
			Assert.NotNull(ex);

			// make sure the response is valid
			Assert.IsTrue(r.HasValue);
			Assert.AreEqual(r.Value.level, uniqueLevelName);
			Assert.IsNull(r.Value.leaders);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void AsyncInvalidServerHost() {
			// Test that if the server host is invalid, an exception is thrown
			lb = new Leaderboards("gibberish" + Guid.NewGuid(), 2693);
			lb.RequestScoresAsync(uniqueLevelName, null);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void AsyncInvalidServerPort() {
			// Test that if the server port is invalid, an exception is thrown
			lb = new Leaderboards("localhost", -1);
			lb.RequestScoresAsync(uniqueLevelName, null);
		}

		[Test]
		[ExpectedException(typeof(ServerException))]
		public void AsyncInvalidServerPort2() {
			// Test that if the server port is invalid, an exception is thrown
			lb = new Leaderboards("localhost", (int)Math.Pow(2, 16));
			lb.RequestScoresAsync(uniqueLevelName, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AsyncNullLevelNameRequest() {
			// Test that passing a null level name to RequestScores,
			// raises a ArgumentNullException
			lb.RequestScoresAsync(null, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AsyncNullLevelNameSubmission() {
			// Test that passing a null level name to SubmitScore,
			// raises a ArgumentNullException
			lb.SubmitScoreAsync(null, new Score(1, "abc", "abc"), null);
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void AsyncInvalidScoreSubmission() {
			// Test that passing an invalid score to SubmitScore,
			// raises a InvalidScoreException
			lb.SubmitScoreAsync(uniqueLevelName, new Score(), null);
		}

	}

}
