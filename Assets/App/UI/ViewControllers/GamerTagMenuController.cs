/*
 * Gamer Tag menu logic.
 *
 * Player can choose their name by picking up characters from
 * the alphabet.
 *
 * Li Cheng <lcheng3@student.unimelb.edu.au>
 * Athir Saleem <isaleem@student.unimelb.edu.au> 
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.ui {
	public class GamerTagMenuController : MonoBehaviour {

		// valid alphabet
		const string VALID_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		// indices of the current characters
		int[] charIndices;

		// UI text elements
		public Text[] textUI;

		// real path stores the player name
		const string filename = "playerName.txt";

		void OnEnable() {
			charIndices = new int[3];

			// read in existing name if any
			string name = PersistentStorage.Read(filename);
			
			// make sure it's a valid name
			// check if letter not in game alphabet 
			if (name.Length >= 3) {
				for (int i = 0; i < 3; i++) {
					int index = VALID_CHARS.IndexOf (name [i]);
					if (index == -1) {
						index = 0;
					}
					charIndices[i] = index;
				}
			}

			// update three UI text elements at one go
			UpdateTextUI();
		}

		/// <summary>
		/// Go around all valid characters. 
		/// When the last character is reached, starts from the beginning.
		/// </summary>
		/// <param name="index">index</param>
		void wrapAroundIndex(int index) {
			if (charIndices [index] >= VALID_CHARS.Length) {
				charIndices [index] = 0;
			} else if (charIndices[index] < 0) {
				charIndices [index] = VALID_CHARS.Length - 1;
			}
		}

		/// <summary>
		/// Convert 3 text UI elements to a string
		/// </summary>
		/// <returns>name of the player</returns>
		string GetName() {
			return textUI[0].text + textUI[1].text + textUI[2].text;
		}

		/// <summary>
		/// Updates the text UI to the most recent update
		/// </summary>
		void UpdateTextUI() {
			textUI[0].text = VALID_CHARS[charIndices[0]].ToString();
			textUI[1].text = VALID_CHARS[charIndices[1]].ToString();
			textUI[2].text = VALID_CHARS[charIndices[2]].ToString();
		}

		/// <summary>
		/// Increment the text UI element/box
		/// </summary>
		/// <param name="index">Index.</param>
		public void Increment(int index) {
			charIndices [index]++;
			wrapAroundIndex (index);
			UpdateTextUI ();
		}

		/// <summary>
		/// Decrement the text UI element/box
		/// </summary>
		/// <param name="index">Index.</param>
		public void Decrement(int index) {
			charIndices [index]--;
			wrapAroundIndex (index);
			UpdateTextUI ();
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			// save the current gamer tag
			PersistentStorage.Write (filename, GetName ());

			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}

	}
}
