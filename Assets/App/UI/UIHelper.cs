using UnityEngine;
using System.Collections;
using GooglePlayGames;

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

	// instance versions for use as button on click handlers (static methods don't work)

	public void iGoTo (string sceneName) {
		UIHelper.GoTo(sceneName);
	}

	public void iLogout () {
		UIHelper.Logout();
	}

	public void ToggleVisiblity(GameObject go) {
		go.SetActive(!go.activeSelf);
	}

}
