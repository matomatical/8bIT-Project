using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			UIHelper.GoTo ("PostGameMenu");
		}
	}
}
