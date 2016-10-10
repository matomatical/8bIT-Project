/*
 * Utility class to allow reading and writing files to a peristent
 * disk location of strings.
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using System.IO;
using UnityEngine;

namespace xyz._8bITProject.cooperace.persistence {

	public class PersistentFile {

		/// Helper method to get the absolute filepath given a relative one.
		/// Also makes sure that the proper path separator character is used.
		private static string GetAbsolute(string relPath) {
			// Note: Path.GetFullPath converts to the appropriate path separator
			return Path.GetFullPath(Path.Combine(Application.persistentDataPath, relPath));
		}
		
		/// Write a text string into a new file, which can be recalled later
		/// using the same filepath. The file is stored in a location which 
		/// depends on the device, but it's consistent (using unity's 
		/// Application.persistentDataPath, which is a public directory on IOS/
		/// Android device, for example)
		/// If a file of this name exists already, it will be overwritten
		public static void Write(string relPath, string text) {
			string absPath = GetAbsolute(relPath);
			
			// Make sure that the necessary directories exist
			Directory.CreateDirectory(Path.GetDirectoryName(absPath));
			
			// Write to the actual file
			File.WriteAllText(absPath, text);
		}

		/// Read text string from a file named filename, stored earlier with
		/// Write("your text", filename).
		/// If the file does not exist, or some other error occurs, an exception
		/// is thrown
		public static string Read(string relPath) {
			return File.ReadAllText(GetAbsolute(relPath));
		}

		/// Delete the given file.
		///
		/// Nothing happens if the file doesn't exist.
		public static void Delete(string relPath) {
			File.Delete(GetAbsolute(relPath));
		}
		
		/// Returns a list of all files inside the given folder.
		/// Note: Directories are not included in the results.
		///
		/// Optionally takes a file pattern (eg: "*.txt")
		/// Optionally searches subfolders as well.
		/// (In which case all results will be relative paths.)
		///
		/// Returns an empty array if the folder doesn't exist.
		public static string[] List(string relPath, bool recursive=false, string pattern=null) {
			string absPath = GetAbsolute(relPath);
			string [] results;
			try {
				results = Directory.GetFiles(absPath,
					pattern == null ? "*" : pattern,
					recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			} catch (DirectoryNotFoundException) {
				return new string[0];
			}
			
			// Convert back to relative paths
			int startIndex = absPath.Length + 1; // plus 1 excludes slash
			for (int i = 0; i < results.Length; i++) {
				results[i] = results[i].Substring(startIndex);
			}
			
			return results;
		}

	}
}