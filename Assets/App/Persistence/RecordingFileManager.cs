/*
 * Helper class to provide loading and saving functionality
 * specifically for Recordings
 * 
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.persistence{
	public class RecordingFileManager {

		private const string directory = "Recordings/";
		private const string extension = ".crr";

		public static string[] ListRecordings(){

			string[] ls = PersistentStorage.ListFiles (directory, false, "*" + extension);
			for (int i = 0; i < ls.Length; i++) {
				ls [i] = ls [i].Remove (ls [i].Length - extension.Length);
			}

			return ls;
		}


		/// throws RecordingFormatException and PersisntentSotrageException
		public static void WriteRecording(Recording recording){

			string name = recording.level; // TODO: add more info

			string recordingString = RecordingToString (recording);
			PersistentStorage.Write(Path(name), recordingString);
		}

		/// throws RecordingFormatException and PersisntentSotrageException
		public static Recording TryReadRecording(string name){
			string recordingString = PersistentStorage.Read(Path(name));
			return RecordingFromString(recordingString);
		}

		public static void DeleteRecording(string name){
			PersistentStorage.Delete (Path(name));
		}


		static string Path(string name){
			return directory + name + extension;
		}

		static Recording RecordingFromString(string text){
			try{
				return JsonUtility.FromJson<Recording>(text);
			} catch (System.Exception e){
				// something's gone wrong (not sure what type
				// of exceptions this throws because no docs)
				throw new RecordingFormatException (e.Message);
			}
		}

		static string RecordingToString(Recording recording){
			return JsonUtility.ToJson (recording);
		}

	}
}
