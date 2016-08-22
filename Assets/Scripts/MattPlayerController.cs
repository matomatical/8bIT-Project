using UnityEngine;
using System.Collections;

public class MattPlayerController : MonoBehaviour {

	public float speedX, speedY;

	bool forward = true;
	bool jumping = true;

	Animator animator;
	Rigidbody2D body;
	BoxCollider2D box;

	void Start () {
		animator = GetComponent<Animator> ();
		box = GetComponent<BoxCollider2D> ();
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 speed = body.velocity;

		bool jump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);

		if (jump && !jumping) {
			jumping = true;
			speed.y = speedY;
		}

		bool left  = Input.GetKey(KeyCode.LeftArrow);
		bool right = Input.GetKey(KeyCode.RightArrow);

		if (left == right) { // left and right or neither
			speed.x = 0;
		} else if (left) {
			speed.x = -speedX;
			forward = false;
		} else {
			speed.x = speedX;
			forward = true;
		}

		if (speed.x != 0 && jumping == false) {
			animator.SetInteger ("State", 1);
		} else if (speed.x == 0 && jumping == false) {
			animator.SetInteger ("State", 0);
		} else if (speed.y > 0) {
			animator.SetInteger ("State", 2);
		} else {
			animator.SetInteger ("State", 3);
		}
	

		if((forward && transform.localScale.x < 0) || (!forward && transform.localScale.x > 0)){
			Vector3 scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
		}


		body.velocity = speed;
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.CompareTag ("Ground")) {
			this.jumping = false;
		}
	}

	public void walkLeft(){
		// walk left
	}

	public void walkRight(){
		// walk right
	}
}
