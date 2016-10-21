/*
 * This component determines the correct checkered pattern to
 * display depending on the location of its object! If the
 * object is in an (odd, odd) or (even, even) position, it will
 * use one pattern, and a complementary pattern otherwise.
 *
 * Matt Farrugia <farrugiam@student.unimelb.edu.au>
 *
 */

using UnityEngine;
using System.Collections;

public class CheckeredFlag : MonoBehaviour {

	// sprites to show
	public Sprite spriteOff, spriteOn;

	void Start () {

		// where are we?

		Vector2 position = transform.localPosition;
		int x = (int)position.x;
		int y = (int)position.y;

		// decide on the sprite

		Sprite sprite;
		
		if ((x % 2 != 0) == (y % 2 != 0)) {
			// odd odd or even even tile: sprite one!
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
