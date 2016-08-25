using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	public ClockController clock;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			clock.StopTiming ();
		}
	}
}
