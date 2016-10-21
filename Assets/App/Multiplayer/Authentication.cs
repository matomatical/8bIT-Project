/*
 * GooglePlayGames authentication methods.
 *
 * Athir Saleem <isaleem@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;
using GooglePlayGames;

namespace xyz._8bITProject.cooperace.multiplayer {
	public static class Authentication {

		public static void Login (System.Action<bool> callback) {
			if (Social.localUser.authenticated) return;

			PlayGamesPlatform.Instance.Authenticate(callback);
		}
		
		public static bool IsLoggedIn() {
			return PlayGamesPlatform.Instance.IsAuthenticated();
		}

		public static void Logout() {
			PlayGamesPlatform.Instance.SignOut();
		}

	}
}