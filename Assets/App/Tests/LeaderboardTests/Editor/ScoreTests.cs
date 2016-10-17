/*
 * Unit tests for the Score class.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.leaderboard.tests {

	[TestFixture]
	public class ScoreTests {

		/*
		 * Comparison tests.
		 */

		[Test]
		public void ScoresAreEqualWhenIdentical() {
			Assert.AreEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "aaa", "bbb"));
		}

		[Test]
		public void ScoresAreNotEqualWhenTimeIsDifferent() {
			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(10, "aaa", "bbb"));
		}

		[Test]
		public void ScoresAreNotEqualWhenPlayer1NameIsDifferent() {
			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "ccc", "bbb"));
		}

		[Test]
		public void ScoresAreNotEqualWhenPlayer2NameIsDifferent() {
			Assert.AreNotEqual(
				new Score(1, "aaa", "bbb"),
				new Score(1, "aaa", "ccc"));
		}

		/*
		 * Invalid score exception tests.
		 */

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
		public void InvalidScoreTooLongPlayer1Name() {
			new Score(1, "aaaa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreTooShortPlayer1Name() {
			new Score(1, "aa", "bbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreTooLongPlayer2Name() {
			new Score(1, "aaa", "bbbb");
		}

		[Test]
		[ExpectedException(typeof(Score.InvalidScoreException))]
		public void InvalidScoreTooShortPlayer2Name() {
			new Score(1, "aaa", "bbbb");
		}

	}

}
