/*
 * A class to handle the reading and saving of the player's gamer tag.
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace {
	
	public static class GamerTagManager {

		// valid alphabet
		public const string VALID_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		// real path stores the player name
		const string filename = "gamertag";

		// the currently loaded in gamer tag
		static string gamerTag;
		
		/// gets the gamer tag, reading from file if necessary
		public static string GetGamerTag() {
			// for first time read, load from file
			if (gamerTag == null) {
				gamerTag = PersistentStorage.Read(filename);
				
				// discard if invalid
				if (!IsValid(gamerTag)) {
					gamerTag = null;
				}
			}
			return gamerTag;
		}
		
		/// sets (and saves to file) the given gamer tag
		public static bool SetGamerTag(string newGamerTag) {
			if (!IsValid(newGamerTag)) {
				return false;
			}
			gamerTag = newGamerTag;
			PersistentStorage.Write(filename, newGamerTag);
			return true;
		}
		
		/// return whether or not the given gamer tag is valid
		static bool IsValid(string gamerTag) {
			if (gamerTag == null) {
				return false;
			}

			// has to be 3 chars exactly
			if (gamerTag.Length != 3) {
				return false;
			}
			
			// all letters have to be in VALID_CHARS
			foreach (char c in gamerTag) {
				if (VALID_CHARS.IndexOf(c) == -1) {
					return false;
				}
			}

			// got this far? it's valid
			return true;
		}

	}

}