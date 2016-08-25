using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class TitleScreen : MonoBehaviour {

    void Start () {
		PlayGamesPlatform.Activate ();
	}

	void Update() {
        // directly go to the main menu if already logged in
		if (Social.localUser.authenticated) {
            UIHelper.GoTo("MainMenu");
        }
    }

    public void Authenticate () {
		UIHelper.Login((bool success) => {
			if (success) {
	        	UIHelper.GoTo("MainMenu");
			} else {
				// unhandled ;)
			}
		});
    }

}
