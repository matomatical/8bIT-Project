/*
 * Utility class to allow reading and writing files to a peristent
 * disk location of strings.
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using System;
using System.IO;
using UnityEngine;

namespace xyz._8bITProject.cooperace.persistence {

	/// All files and folder created with PersistentStorage will be stored in
	/// the same root folder. (unity's Application.persistentDataPath)
	/// The location of this root folder can differ from device to device, but
	/// is consistent on that same device.
	public class PersistentStorage {

		/// Helper method to get the absolute filepath given a relative one.
		/// Also makes sure that the proper path separator character is used.
		///
		/// Relative paths are relative to the root folder for persistent
		/// storage.
		/// Absolute paths are the full path with the device's entire folder
		/// hierarchy included.
		private static string GetAbsolute(string relPath) {
			// Note: Path.GetFullPath converts to the appropriate path separator
			return Path.GetFullPath(Path.Combine(Application.persistentDataPath, relPath));
		}
		
		/// Write the given text string into a file with the given relative
		/// path.
		/// If a file of this name exists already, it will be overwritten.
		///
		/// For any other error, a PersistentStorageException will be thrown.
		public static void Write(string relPath, string text) {
			string absPath = GetAbsolute(relPath);
			
			try {
				// Make sure that the necessary directories exist
				Directory.CreateDirectory(Path.GetDirectoryName(absPath));
				
				// Write to the actual file
				File.WriteAllText(absPath, text);
			// wrap any errors
			} catch (IOException e) {
				throw new PersistentStorageException(e);	
			} catch (UnauthorizedAccessException e) {
				throw new PersistentStorageException(e);	
			} catch (NotSupportedException e) {
				throw new PersistentStorageException(e);	
			} catch (System.Security.SecurityException e) {
				throw new PersistentStorageException(e);	
			}
		}

		/// Read a text string from a file with the given relative path.
		/// If the file does not exist then an empty string will be returned.
		///
		/// For any other error, a PersistentStorageException will be thrown.
		public static string Read(string relPath) {
			try {
				return File.ReadAllText(GetAbsolute(relPath));
			// if the file doesn't exist, return the empty string.
			} catch (DirectoryNotFoundException) {
				return "";
			} catch (FileNotFoundException) {
				return "";
			// wrap any other errors
			} catch (IOException e) {
				throw new PersistentStorageException(e);	
			} catch (UnauthorizedAccessException e) {
				throw new PersistentStorageException(e);	
			} catch (NotSupportedException e) {
				throw new PersistentStorageException(e);	
			} catch (System.Security.SecurityException e) {
				throw new PersistentStorageException(e);	
			}
		}

		/// Delete the given file.
		/// Nothing happens if the file doesn't exist.
		///
		/// For any other error, a PersistentStorageException will be thrown.
		public static void Delete(string relPath) {
			try {
				File.Delete(GetAbsolute(relPath));
			} catch (IOException e) {
				throw new PersistentStorageException(e);	
			} catch (NotSupportedException e) {
				throw new PersistentStorageException(e);	
			} catch (UnauthorizedAccessException e) {
				throw new PersistentStorageException(e);	
			}
		}
		
		/// Returns a list of all files inside the given folder.
		/// Note: Directories are not included in the results.
		///
		/// All results will be paths relative to the given folder.
		///
		/// Optionally recursive (searches subfolders as well).
		/// Optionally takes a file pattern (eg: "*.txt")
		///
		/// eg: ListFiles("folder",
		///       	true,    // recursive on
		///         "*.txt"  // pattern matching files with the extension txt
		///     )
		/// 	might return ["file", "subfolder/file"]
		///
		/// Returns an empty array if the folder doesn't exist.
		///
		/// For any other error, a PersistentStorageException will be thrown.
		public static string[] ListFiles(string relPath, bool recursive=false, string pattern=null) {
			// absolute path with ending slashes stripped
			string absPath = GetAbsolute(relPath).TrimEnd('/', '\\');

			string [] results;
			try {
				results = Directory.GetFiles(absPath,
					pattern == null ? "*" : pattern,
					recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			} catch (DirectoryNotFoundException) {
				return new string[0];
			} catch (IOException e) {
				throw new PersistentStorageException(e);	
			} catch (UnauthorizedAccessException e) {
				throw new PersistentStorageException(e);	
			} catch (ArgumentException e) {
				throw new PersistentStorageException(e);	
			}
			
			// Convert back to relative paths (relative to the given path).
			int startIndex = absPath.Length + 1; // plus 1 excludes slash
			for (int i = 0; i < results.Length; i++) {
				results[i] = results[i].Substring(startIndex);
			}
			
			return results;
		}

	}
}