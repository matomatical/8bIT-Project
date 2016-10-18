using UnityEngine;
using System.Collections;
using GooglePlayGames;
using xyz._8bITProject.cooperace;

public class UIHelper : MonoBehaviour {

	public static void GoTo (string sceneName) {
		xyz._8bITProject.cooperace.SceneManager.Load(sceneName);
	}

	public static void Login (System.Action<bool> callback, bool silent=false) {
		if (Social.localUser.authenticated) return;

		PlayGamesPlatform.Instance.Authenticate(callback, silent);
	}

	public static void Logout () {
		PlayGamesPlatform.Instance.SignOut();
	}

	public static void LeaveRoom() {
		UILogger.Log("LeavingRoom");
		PlayGamesPlatform.Instance.RealTime.LeaveRoom();
	}


	// instance versions for use as button on click handlers (static methods don't work)

	public void iGoTo (string sceneName) {
		UIHelper.GoTo(sceneName);
	}

	public void iLogout () {
		UIHelper.Logout();
	}

	public void iLeaveRoom() {
		UIHelper.LeaveRoom();
	}

	public void ToggleVisiblity(GameObject go) {
		go.SetActive(!go.activeSelf);
	}

	// If your partner leaves the game, it brings up the left game menu
	public static void LeftGameMenu() {
		GameObject gui = GameObject.Find("In-Game GUI");

		GameObject menu = gui.transform.FindChild("PlayerDisconnectedMenu").gameObject;
		GameObject controlls = gui.transform.FindChild("OnScreenControlls").gameObject;
		menu.SetActive(true);
		controlls.SetActive(false);
	}


}
