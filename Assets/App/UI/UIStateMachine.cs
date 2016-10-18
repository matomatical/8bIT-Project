/*
 * State machine controlling all the ui screens.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.ui {
	public class UIStateMachine : MonoBehaviour {

		// Singleton instance
		static UIStateMachine instance_;
		public static UIStateMachine instance {
			get {
				if (instance_ == null) {
					instance_ = FindObjectOfType<UIStateMachine>();
				}
				return instance_;
			}
		}

		// All the actual menu game objects
		public GameObject mainMenu;
		public GameObject leaderboardsMenu;
		public GameObject gamerTagMenu;
		public GameObject recordingsMenu;
		public GameObject levelSelectMenu;

		UIState currentState = UIState.MainMenu;

		void Start () {
			// TODO: google play login
			
			// TODO: check gamer tag existance and prompt 
			if (false) {
				currentState = UIState.GamerTagMenu;
			}
			
			// Open the initial state
			Open(currentState);
		}
		
		/// Method to switch the currently visible screen
		public void GoTo(UIState newState) {
			Close(currentState);
			Open(newState);
			currentState = newState;
		}
		
		// Open the given state
		void Open(UIState state) {
			GetGameObject(state).SetActive(true);
		}
		
		// Close the given state
		void Close(UIState state) {
			GetGameObject(state).SetActive(false);
		}
		
		// From UIState to actual GameObject.
		GameObject GetGameObject(UIState state) {
			switch (state) {
				case UIState.MainMenu:
					return mainMenu;
				case UIState.LeaderboardsMenu:
					return leaderboardsMenu;
				case UIState.GamerTagMenu:
					return gamerTagMenu;
				case UIState.RecordingsMenu:
					return recordingsMenu;
				case UIState.LevelSelect:
					return levelSelectMenu;
				default: // keeps compiler happy
					return null;
			}
		} 

	}
}