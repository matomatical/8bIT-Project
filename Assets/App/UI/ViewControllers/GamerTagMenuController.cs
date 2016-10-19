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
using xyz._8bITProject.cooperace.multiplayer;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.ui {
	public class GamerTagMenuController : MonoBehaviour {
		
		// flag to indicate use of first time ui
		static bool isFirstTime_ = false;
		public static void IsFirstTime() {
			isFirstTime_ = true;
		}

		// valid alphabet
		public const string VALID_CHARS = GamerTagManager.VALID_CHARS;

		// indices of the current characters
		int[] charIndices;

		// UI elements
		public Text[] textUI;
		public GameObject backButton;
		public GameObject confirmButton;
		public GameObject logoutButton;
		public GameObject normalTitle;
		public GameObject firstTimeTitle;

		void OnEnable() {
			if (isFirstTime_) {
				EnableFirstTimeUI();
			} else {
				// show/hide logout button based on the player's login status
				logoutButton.SetActive(Authentication.IsLoggedIn());
			}

			charIndices = new int[3];

			// read in existing name if any
			string tag = GamerTagManager.GetGamerTag();
			if (tag != null) {
				for (int i = 0; i < 3; i++) {
					charIndices[i] = VALID_CHARS.IndexOf(tag[i]);
				}
			}

			// update three UI text elements at one go
			UpdateTextUI();
		}
		
		// Change the ui for the first time case
		void EnableFirstTimeUI() {
			// disable normal ui
			backButton.SetActive(false);
			normalTitle.SetActive(false);
			logoutButton.SetActive(false);

			// enable first time ui
			confirmButton.SetActive(true);
			firstTimeTitle.SetActive(true);
		}
		
		// Undo first time ui changes
		void DisableFirstTimeUI() {
			// enable normal ui
			backButton.SetActive(true);
			normalTitle.SetActive(true);
			logoutButton.SetActive(true);

			// disable first time ui
			confirmButton.SetActive(false);
			firstTimeTitle.SetActive(false);
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

		// public method to handle confirm button behaviour
		public void ConfirmButtonHandler() {
			isFirstTime_ = false;
			DisableFirstTimeUI();
			BackButtonHandler();
		}

		// public method to handle logout button behaviour
		public void LogoutButtonHandler() {
			Authentication.Logout();
			BackButtonHandler();
		}

		// public method to handle back button behaviour
		public void BackButtonHandler() {
			// save the current gamer tag
			GamerTagManager.SetGamerTag(GetName());

			UIStateMachine.instance.GoTo(UIState.MainMenu);
		}

	}
}
