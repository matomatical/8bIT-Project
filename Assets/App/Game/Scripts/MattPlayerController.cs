using UnityEngine;
using System.Collections;

public class MattPlayerController : MonoBehaviour {

	public float speedX, speedY;

	bool forward = true;
	bool jumping = true;

	public Joystick joystick;

	bool  inputJump, inputLeft, inputRight;


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

		// gather input

		bool jump = InputJump();
		float walk = InputWalk();

		// update velocity

		body.velocity = UpdatedVelocity(walk, jump);


		// update animation state

		if (body.velocity.x != 0 && jumping == false) {
			animator.SetInteger("State", 1);
		} else if (body.velocity.x == 0 && jumping == false) {
			animator.SetInteger("State", 0);
		} else if (body.velocity.y > 0) {
			animator.SetInteger("State", 2);
		} else {
			animator.SetInteger("State", 3);
		}
		
		// reflect if we're facing a new direction

		if(walk > 0) {
			forward = true;
		} else if (walk < 0) {
			forward = false;
		}

		if(forward == (transform.localScale.x < 0)) {
			Vector3 scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
		}
	}

	private Vector2 UpdatedVelocity(float walk, bool jump) {

		Vector2 speed = body.velocity;

		if (jump && !jumping) {
			jumping = true;
			speed.y = speedY;
		}

		speed.x = walk * speedX;
		
		return speed;
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.CompareTag ("Ground")) {
			this.jumping = false;
		}
	}

	private float InputWalk() {
		
		bool left =  inputLeft  || Input.GetKey(KeyCode.LeftArrow);
		bool right = inputRight || Input.GetKey(KeyCode.RightArrow);

		float keyInput = 0;

		if (left == right) { // left and right or neither
			keyInput = 0;
		} else if (left) {
			keyInput = -1;
		} else {
			keyInput = 1;
		}

		return Mathf.Clamp(joystick.Input() + keyInput, -1, 1);
	}

	private bool InputJump() {
		return inputJump  || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);
	}

	public void SetInputLeft() {   inputLeft = true; }
	public void UnsetInputLeft() { inputLeft = false; }

	public void SetInputRight() {   inputRight = true; }
	public void UnsetInputRight() { inputRight = false; }

	public void SetInputJump() {   inputJump = true; }
	public void UnsetInputJump() { inputJump = false; }
}
