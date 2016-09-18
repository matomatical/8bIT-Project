using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		KeyHolder holder = other.gameObject.GetComponent<KeyHolder>();
		if (holder != null && !holder.holdingKey) {
			holder.holdingKey = true;
			Destroy(gameObject);
		}
	}

}
