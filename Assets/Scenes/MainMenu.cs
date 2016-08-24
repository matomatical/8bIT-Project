using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject UsernameText;

	void OnGUI () {
		UsernameText.GetComponent<Text>().text = "Logged in as " + Social.localUser.userName;
	}

}
