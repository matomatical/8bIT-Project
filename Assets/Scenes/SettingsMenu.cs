using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

	public GameObject UsernameText;

	void OnGUI () {
		UsernameText.GetComponent<Text>().text = "Logged in as " + Social.localUser.userName;
	}

	public void Logout () {
		UIHelper.Logout();
		UIHelper.GoTo("TitleScreen");
	}

}
