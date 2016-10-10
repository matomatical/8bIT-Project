/*
 * Unit Tests for PersistentFile class!
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.persistence.tests {

	[TestFixture]
	public class PersistentFileTests {
		
		// The root folder for the current test
		string uniqueRoot;
		
		// cross-platform helper to combine paths
		// takes any number of path segments and joins them with the platform
		// specific path separator character
		string PathJoin(params string[] pathSegments) {
			string result = pathSegments[0];
			for (int i = 1; i < pathSegments.Length; i++) {
				result = Path.Combine(result, pathSegments[i]);
			}
			return result;
		}
		
		// helper to check file existence
		// takes a path (including the unique prefix) and returns whether it exists or not
		bool FileExists(string relPath) {
			return File.Exists(Path.Combine(Application.persistentDataPath, relPath));
		}
		
		[SetUp]
		public void SetUp() {
			// create a unique folder for each test
			uniqueRoot = TestContext.CurrentContext.Test.Name + Guid.NewGuid();
			Directory.CreateDirectory(Path.Combine(
				Application.persistentDataPath, uniqueRoot));
		}
		
		[TearDown]
		public void TearDown() {
			// delete the unique folder after the test
			Directory.Delete(Path.Combine(
				Application.persistentDataPath, uniqueRoot), true);
		}
		
		// All tests should use this method to create paths instead of using
		// their own paths to make sure clashes don't happen.
		// While at the same time leaving any other items in
		// `Application.persistentDataPath` untouched.
		//
		// As a bonus, tests no longer have to handle clean up.
		private string Unique(params string[] pathSegments) {
			return Path.Combine(uniqueRoot, PathJoin(pathSegments));
		}

		[Test]
		public void WriteThenReadBackShouldGiveSameString() {

			//Arrange

			string testText =
				"Write Then Read Back Should Give Same String Text";
			string testFilename =
				Unique("WriteThenReadBackShouldGiveSameStringFilename.txt");


			//Act

			// try to write this string to filename

			PersistentFile.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentFile.Read (testFilename);


			//Assert

			// the text read is the same as what we wrote

			Assert.AreEqual(testText, actualText);

		}

		[Test]
		public void WriteToExistingFilenameShouldOverwrite() {

			//Arrange

			string testText1 =
				"Write Then Read Back Should Give Same String Text 1";
			string testText2 = "Not the same as the first text";
			string testFilename =
				Unique("WriteToExistingFilenameShouldOverwrite.txt");


			//Act

			// try to write first string to filename

			PersistentFile.Write (testFilename, testText1);

			// then overrite it with second string to same filename

			PersistentFile.Write (testFilename, testText2);

			// try to read it back from the same file

			string actualText = PersistentFile.Read (testFilename);


			//Assert

			// the read text should be the second string

			Assert.AreEqual(testText2, actualText);

		}

		[Test]
		public void WriteIncludingNewlinesShouldAllReadInOneGo() {

			//Arrange

			string testText =
				"This \n Text\n  Contains\n Newlines!\n";
			string testFilename =
				Unique("WriteIncludingNewlinesShouldAllReadInOneGo.txt");


			//Act

			// try and write the multi-line string to file

			PersistentFile.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentFile.Read (testFilename);


			//Assert

			// the read text should be the whole string incl newlines

			Assert.AreEqual(testText, actualText);

		}

		[Test]
		public void WriteCreatesMissingFile() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("WriteCreatesMissingFile.txt");

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// does the file exist?

			bool actualExistence = FileExists(testFilename);


			//Assert

			// the file should have existed

			Assert.IsTrue(actualExistence);
			
		}

		[Test]
		public void WriteCreatesMissingParentFolders() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("Write/Creates/Missing/Parent/Folders.txt");

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// does the file exist?

			bool actualExistence = FileExists(testFilename);


			//Assert

			// the file should have existed

			Assert.IsTrue(actualExistence);

		}

		[Test]
		public void MixedPathSeparatorsAreHandled() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("Mixed\\Path/Separators\\Are/Handled.txt");

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// does the file exist?

			bool actualExistence = FileExists(testFilename);


			//Assert

			// the file should have existed

			Assert.IsTrue(actualExistence);
			
		}

		[Test]
		public void DeleteActuallyDeletesAFile() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("DeleteActuallyDeletesAFile.txt");

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act
			
			// save the existance status before deletion
			bool initialExistence = FileExists(testFilename);

			// delete the file

			PersistentFile.Delete (testFilename);

			// does the file exist?
			bool actualExistence = FileExists(testFilename);


			//Assert
			
			// the file should have existed initially

			Assert.IsTrue(initialExistence);

			// the file should no longer exist

			Assert.IsFalse(actualExistence);
			
		}

		[Test]
		public void DeletingANonExistingFileDoesntCauseAnError() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("DeletingANonExistingFileDoesntCauseAnError.txt");

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// delete the file multiple times

			PersistentFile.Delete (testFilename);
			PersistentFile.Delete (testFilename);
			PersistentFile.Delete (testFilename);


			//Assert
			
			// If execution reaches this point, no error occured.

			Assert.Pass();
			
		}

		[Test]
		public void ListingShouldReturnAllFilesAndIgnoreFolders() {

			//Arrange
			
			PersistentFile.Write (Unique("A"), "");
			PersistentFile.Write (Unique("B"), "");
			PersistentFile.Write (Unique("C"), "");
			PersistentFile.Write (Unique("subfolder/D"), "");


			//Act

			string[] results = PersistentFile.List(Unique(""));


			//Assert

			// the files A, B and C should be returned

			CollectionAssert.AreEquivalent(
				new string[] {"A", "B", "C"}, results);

		}

		[Test]
		public void RecursiveListingReturnsRelativePaths() {

			//Arrange

			PersistentFile.Write (Unique("A"), "");
			PersistentFile.Write (Unique("sub/B"), "");
			PersistentFile.Write (Unique("sub/sub/C"), "");


			//Act

			string[] results = PersistentFile.List(Unique(""), true);


			//Assert

			CollectionAssert.AreEquivalent(
				new string[] {
					"A",
					PathJoin("sub", "B"),
					PathJoin("sub", "sub", "C")
				}, results);

		}
	
		[Test]
		public void ListingWithPatternsWorks() {

			//Arrange

			PersistentFile.Write (Unique("A.ignore"), "");
			PersistentFile.Write (Unique("A.return"), "");
			PersistentFile.Write (Unique("sub/B.ignore"), "");
			PersistentFile.Write (Unique("sub/B.return"), "");
			PersistentFile.Write (Unique("sub/sub/C.ignore"), "");
			PersistentFile.Write (Unique("sub/sub/C.return"), "");


			//Act

			string[] results = PersistentFile.List(Unique(""), true, "*.return");


			//Assert

			CollectionAssert.AreEquivalent(
				new string[] {
					"A.return",
					PathJoin("sub", "B.return"),
					PathJoin("sub", "sub", "C.return")
				}, results);

		}
	}
}
