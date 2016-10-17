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
	public class PersistentStorageTests {
		
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

			PersistentStorage.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentStorage.Read (testFilename);


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

			PersistentStorage.Write (testFilename, testText1);

			// then overrite it with second string to same filename

			PersistentStorage.Write (testFilename, testText2);

			// try to read it back from the same file

			string actualText = PersistentStorage.Read (testFilename);


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

			PersistentStorage.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentStorage.Read (testFilename);


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

			PersistentStorage.Write (testFilename, testText);


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

			PersistentStorage.Write (testFilename, testText);


			//Act

			// does the file exist?

			bool actualExistence = FileExists(testFilename);


			//Assert

			// the file should have existed

			Assert.IsTrue(actualExistence);

		}

		[Test]
		public void ReadFromNonExistentFileReturnsAnEmptyString() {

			//Arrange

			string testFilename =
				Unique("Read/From/Non/Existent/File.txt");


			//Act

			string fileContents = PersistentStorage.Read(testFilename);
			

			//Assert
			
			// fileContents should be an empty string.
			Assert.AreEqual("", fileContents);

		}

		[Test]
		public void MixedPathSeparatorsAreHandled() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				Unique("Mixed\\Path/Separators\\Are/Handled.txt");

			// create the file

			PersistentStorage.Write (testFilename, testText);


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

			PersistentStorage.Write (testFilename, testText);


			//Act
			
			// save the existance status before deletion
			bool initialExistence = FileExists(testFilename);

			// delete the file

			PersistentStorage.Delete (testFilename);

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

			PersistentStorage.Write (testFilename, testText);


			//Act

			// delete the file multiple times

			PersistentStorage.Delete (testFilename);
			PersistentStorage.Delete (testFilename);
			PersistentStorage.Delete (testFilename);


			//Assert
			
			// If execution reaches this point, no error occured.

			Assert.Pass();
			
		}

		[Test]
		public void ListingShouldReturnAllFilesAndIgnoreFolders() {

			//Arrange
			
			PersistentStorage.Write (Unique("A"), "");
			PersistentStorage.Write (Unique("B"), "");
			PersistentStorage.Write (Unique("C"), "");
			PersistentStorage.Write (Unique("subfolder/D"), "");


			//Act

			string[] results = PersistentStorage.ListFiles(Unique(""));


			//Assert

			// the files A, B and C should be returned

			CollectionAssert.AreEquivalent(
				new string[] {"A", "B", "C"}, results);

		}

		[Test]
		public void ListingShouldReturnPathsRelativeToTheGivenPath() {

			//Arrange
			
			PersistentStorage.Write (Unique("highly/nested/sub/folder/A"), "");
			PersistentStorage.Write (Unique("highly/nested/sub/folder/B"), "");
			PersistentStorage.Write (Unique("highly/nested/sub/folder/C"), "");


			//Act

			string[] results = PersistentStorage.ListFiles(Unique("highly/nested/sub/folder"));


			//Assert

			// the files A, B and C should be returned

			CollectionAssert.AreEquivalent(
				new string[] {"A", "B", "C"}, results);

		}

		[Test]
		public void WhenListingEndingSlashShouldNotAffectResults() {

			//Arrange
			
			PersistentStorage.Write (Unique("subfolder/A"), "");
			PersistentStorage.Write (Unique("subfolder/B"), "");
			PersistentStorage.Write (Unique("subfolder/C"), "");


			//Act

			string[] results1 = PersistentStorage.ListFiles(Unique("subfolder"));
			string[] results2 = PersistentStorage.ListFiles(Unique("subfolder/"));
			string[] results3 = PersistentStorage.ListFiles(Unique("subfolder\\"));


			//Assert

			// the files A, B and C should be returned in each case

			string[] expected = new string[] {"A", "B", "C"};
			CollectionAssert.AreEquivalent(expected, results1);
			CollectionAssert.AreEquivalent(expected, results2);
			CollectionAssert.AreEquivalent(expected, results3);

		}

		[Test]
		public void ListingAnEmptyFolderShouldReturnAnEmptyArray() {

			//Arrange

			// Create nothing

			//Act

			string[] results = PersistentStorage.ListFiles(Unique(""));


			//Assert

			// should be empty
			CollectionAssert.IsEmpty(results);

		}

		[Test]
		public void RecursiveListingReturnsRelativePaths() {

			//Arrange

			PersistentStorage.Write (Unique("A"), "");
			PersistentStorage.Write (Unique("sub/B"), "");
			PersistentStorage.Write (Unique("sub/sub/C"), "");


			//Act

			string[] results = PersistentStorage.ListFiles(Unique(""), true);


			//Assert

			CollectionAssert.AreEquivalent(
				new string[] {
					"A",
					PathJoin("sub", "B"),
					PathJoin("sub", "sub", "C")
				}, results);

		}
	
		[Test]
		public void ListingWithPatternsWorksWithRecursionOff() {

			//Arrange

			PersistentStorage.Write (Unique("A.ignore"), "");
			PersistentStorage.Write (Unique("A.return"), "");
			PersistentStorage.Write (Unique("A2.ignore"), "");
			PersistentStorage.Write (Unique("A2.return"), "");
			PersistentStorage.Write (Unique("sub/B.ignore"), "");
			PersistentStorage.Write (Unique("sub/B.return"), "");
			PersistentStorage.Write (Unique("sub/B2.ignore"), "");
			PersistentStorage.Write (Unique("sub/B2.return"), "");
			PersistentStorage.Write (Unique("sub/sub/C.ignore"), "");
			PersistentStorage.Write (Unique("sub/sub/C.return"), "");
			PersistentStorage.Write (Unique("sub/sub/C2.ignore"), "");
			PersistentStorage.Write (Unique("sub/sub/C2.return"), "");


			//Act

			string[] results = PersistentStorage.ListFiles(Unique(""), false, "*.return");


			//Assert

			CollectionAssert.AreEquivalent(
				new string[] {
					"A.return",
					"A2.return"
				}, results);

		}
	
		[Test]
		public void ListingWithPatternsWorksWithRecursionOn() {

			//Arrange

			PersistentStorage.Write (Unique("A.ignore"), "");
			PersistentStorage.Write (Unique("A.return"), "");
			PersistentStorage.Write (Unique("A2.ignore"), "");
			PersistentStorage.Write (Unique("A2.return"), "");
			PersistentStorage.Write (Unique("sub/B.ignore"), "");
			PersistentStorage.Write (Unique("sub/B.return"), "");
			PersistentStorage.Write (Unique("sub/B2.ignore"), "");
			PersistentStorage.Write (Unique("sub/B2.return"), "");
			PersistentStorage.Write (Unique("sub/sub/C.ignore"), "");
			PersistentStorage.Write (Unique("sub/sub/C.return"), "");
			PersistentStorage.Write (Unique("sub/sub/C2.ignore"), "");
			PersistentStorage.Write (Unique("sub/sub/C2.return"), "");


			//Act

			string[] results = PersistentStorage.ListFiles(Unique(""), true, "*.return");


			//Assert

			CollectionAssert.AreEquivalent(
				new string[] {
					"A.return",
					"A2.return",
					PathJoin("sub", "B.return"),
					PathJoin("sub", "B2.return"),
					PathJoin("sub", "sub", "C.return"),
					PathJoin("sub", "sub", "C2.return")
				}, results);

		}

		[Test]
		public void ListingWithAPatternWithNoMatchesShouldReturnAnEmptyArray() {

			//Arrange

			PersistentStorage.Write (Unique("A.txt"), "");
			PersistentStorage.Write (Unique("B.json"), "");
			PersistentStorage.Write (Unique("C.pdf"), "");
			PersistentStorage.Write (Unique("subfolder/D.xml"), "");

			//Act

			// pattern that won't match anything
			string[] results = PersistentStorage.ListFiles(Unique(""), false, "*.return");


			//Assert

			// should be empty
			CollectionAssert.IsEmpty(results);

		}
	}
}
