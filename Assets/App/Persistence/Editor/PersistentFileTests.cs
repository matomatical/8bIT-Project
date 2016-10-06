/*
 * Unit Tests for PersistentFile class!
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace xyz._8bITProject.cooperace.persistence.tests {

	[TestFixture]
	public class PersistentFileTests {

		[Test]
		public void WriteThenReadBackShouldGiveSameString() {

			//Arrange
			string testText =
				"Write Then Read Back Should Give Same String Text";
			string testFilename =
				"WriteThenReadBackShouldGiveSameStringFilename.txt";

			//Act

			// try to write this string to filename

			PersistentFile.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentFile.Read (testFilename);

			//Assert

			// the text read is the same as what we wrote

			Assert.AreEqual(testText, actualText);
		}
	}
}