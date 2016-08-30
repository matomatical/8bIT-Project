using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public SpriteRenderer level;
	public Camera camera;

	private float scalex, scaley;

	// Use this for initialization
	void Start () {
		
		SpriteRenderer background = GetComponent<SpriteRenderer> ();
		float bgx = background.bounds.size.x;
		float bgy = background.bounds.size.y;

		float levelx = level.bounds.size.x;
		float levely = level.bounds.size.y;

		float camerax = camera.orthographicSize * Screen.width / Screen.height * 2;
		float cameray = camera.orthographicSize * 2;

		scalex = (float) ((levelx - bgx )/ (levelx - camerax));
		scaley = (float) ((levely - bgy) / (levely - cameray));
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
