using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {
	
	public UIHelper uihelp;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			uihelp.iGoTo ("PostGameMenu");
		}
	}
}
