using UnityEngine;
using System.Collections;

public class CheckeredFlag : MonoBehaviour {

	// sprites to show
	public Sprite spriteOff, spriteOn;

	void Start () {

		// where are we?

		Vector2 position = transform.position;
		int x = (int)position.x;
		int y = (int)position.y;

		// decide on the sprite

		Sprite sprite;
		
		if ((x % 2 != 0) == (y % 2 != 0)) {
			// odd odd or even evn tile: sprite one!
			sprite = spriteOn;
		} else {
			// odd even or even odd tile: sprite two!
			sprite = spriteOff;
		}

		// apply the sprite

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprite;
	}

}
