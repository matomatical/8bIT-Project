using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	// find a better way to do this after figuring tiled2unity out
	public int levelx, levely;

	public Camera camera;

	private float scalex, scaley;

	// Use this for initialization
	void Start () {
		SpriteRenderer background = GetComponent<SpriteRenderer> ();

		float bgWidth  = background.bounds.size.x * background.sprite.pixelsPerUnit;
		float bgHeight = background.bounds.size.y * background.sprite.pixelsPerUnit;

		float camWidth = 0; //	CAMERA VIEWPORT WIDTH IN IN-GAME PIXELS
		float camHeight = 0; // CAMERA VIEWPORT HIGHT IN IN-GAME PIXELS

		scalex = (float) ((levelx - bgWidth )  / (levelx - camWidth));
		scaley = (float) ((levely - bgHeight) / (levely - camHeight));
	}
	
	// Guaranteed to run after all Update processing
	void LateUpdate () {

		transform.position = new Vector3 (
			camera.transform.position.x * scalex,
			camera.transform.position.y * scaley,
			transform.position.z
		);
	}
}
