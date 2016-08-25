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

		Debug.Log ("L - B is " + (levelx - bgWidth) + " and L - C is " + (levelx - camera.pixelWidth));

		scalex = (float) ((levelx - bgWidth)  / (levelx - camera.pixelRect.x ));

		Debug.Log ("then for y, L - B is " + (levely - bgHeight) + " and L - C is " + (levely - camera.pixelHeight ));
		scaley = (float) ((levely - bgHeight) / (levely - camera.pixelRect.y));

		Debug.Log ("scalex " + scalex + " scaley " + scaley);
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
