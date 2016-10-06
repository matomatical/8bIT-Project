/*
 * Utility class to allow reading and writing files to a peristent
 * disk location of strings.
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using System.IO;
using UnityEngine;

namespace xyz._8bITProject.cooperace.persistence {

	public class PersistentFile {
		
		/// Write a text string into a new file, which can be recalled later
		/// using the same filename. The file is stored in a location which 
		/// depends on the device, but it's consistent (using unity's 
		/// Application.persistentDataPath, which is a public directory on IOS/
		/// Android device, for example)
		/// If a file of this name exists already, it will be deleted
		public static void Write(string filename, string text) {
			
			FileInfo f = new FileInfo (
				Application.persistentDataPath + "//" + filename);

			if (f.Exists) {
				f.Delete ();
			}

			StreamWriter w = f.CreateText ();
			w.Write (text);
			w.Close ();
		}

		/// Read text string from a file named filename, stored earlier with
		/// Write("your text", filename).
		/// If the file does not exist, or some other error occurs, an exception
		/// is thrown
		public static string Read(string filename) {
			StreamReader r = File.OpenText(
				Application.persistentDataPath + "//" + filename);
			string data = r.ReadToEnd ();
			r.Close ();
			return data;
		}
	}
}