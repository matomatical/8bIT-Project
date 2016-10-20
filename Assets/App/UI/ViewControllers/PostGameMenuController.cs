/*
 * Post game menu logic.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;
using xyz._8bITProject.cooperace.recording;
using xyz._8bITProject.cooperace.multiplayer;

namespace xyz._8bITProject.cooperace.ui {
	public class PostGameMenuController : MonoBehaviour {

		// the actual menu objects
		public GameObject scoreSubmissionScreen;
		public GameObject replayingFinishedScreen;

		void Start(){

			// make sure screens start disabled

			scoreSubmissionScreen.SetActive (false);
			replayingFinishedScreen.SetActive (false);


			// which type of postgame menu are we loading?

			if (SceneManager.outs.opts.type == GameType.REWATCH) {
				replayingFinishedScreen.SetActive (true);
			} else if (SceneManager.outs.opts.type == GameType.GHOST
			           || SceneManager.outs.opts.type == GameType.MULTI) {
				scoreSubmissionScreen.SetActive (true);
			}
		}
	}	
}
