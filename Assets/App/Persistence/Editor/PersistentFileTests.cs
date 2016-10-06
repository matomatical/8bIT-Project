/*
 * Unit Tests for PersistentFile class!
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.IO;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using xyz._8bITProject.cooperace.persistence;

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


			// clean up

			PersistentFile.Delete (testFilename);
		}

		[Test]
		public void WriteToExistingFilenameShouldOverwrite() {

			//Arrange

			string testText1 =
				"Write Then Read Back Should Give Same String Text 1";
			string testText2 = "Not the same as the first text";
			string testFilename =
				"WriteToExistingFilenameShouldOverwrite.txt";


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


			// clean up

			PersistentFile.Delete (testFilename);
		}

		[Test]
		public void WriteIncludingNewlinesShouldAllReadInOneGo() {

			//Arrange

			string testText =
				"This \n Text\n  Contains\n Newlines!\n";
			string testFilename =
				"WriteIncludingNewlinesShouldAllReadInOneGo.txt";


			//Act

			// try and write the multi-line string to file

			PersistentFile.Write (testFilename, testText);

			// try to read it back from the same file

			string actualText = PersistentFile.Read (testFilename);


			//Assert

			// the read text should be the whole string incl newlines

			Assert.AreEqual(testText, actualText);


			// clean up

			PersistentFile.Delete (testFilename);
		}

		[Test]
		public void WriteCreatesMissingFile() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				"WriteCreatesMissingFile.txt";

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// does the file exist? use System.IO to find out

			FileInfo f = new FileInfo (
				Application.persistentDataPath + "//" + testFilename);

			bool actualExistence = f.Exists;


			//Assert

			// the file should have existed

			Assert.AreEqual(true, actualExistence);


			// clean up

			PersistentFile.Delete (testFilename);
		}

		[Test]
		public void DeleteActuallyDeletesAFile() {

			//Arrange

			string testText = "hello, world!\n";
			string testFilename =
				"DeleteActuallyDeletesAFile.txt";

			// create the file

			PersistentFile.Write (testFilename, testText);


			//Act

			// delete the file

			PersistentFile.Delete (testFilename);

			// does the file exist? use System.IO to find out

			FileInfo f = new FileInfo (
				Application.persistentDataPath + "//" + testFilename);

			bool actualExistence = f.Exists;


			//Assert

			// the file should not have existed

			Assert.AreEqual(false, actualExistence);


			// clean up

			PersistentFile.Delete (testFilename);
		}
	}
}