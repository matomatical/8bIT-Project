using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Text))]
public class UsernameDisplay : MonoBehaviour {

	void OnGUI () {
		GetComponent<Text>().text = "Logged in as " + Social.localUser.userName;
	}

}
