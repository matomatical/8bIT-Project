using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour {

	public float MaxSpeed = 8;
	public float Acceleration = 30;
	public float JumpSpeed = 10;
	public float MaxJumpDuration = 0.2f;

	public bool EnableDoubleJump = false;
	public bool wallHitDoubleJumpOverride = true;

	// internal checks

	public bool canDoubleJump = true;

	public float jumpDuration;

	public bool jumpKeyDown = false;
	public bool canVariableJump = false;

	BoxCollider2D collisionBox;
	Rigidbody2D rb2d;

	void Awake () {
		collisionBox = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D>();
	}


	void Update () {
	
		// horizontal movement
		float horizontal = Input.GetAxis("Horizontal");

		if (horizontal < -0.1f) { // player pressing left
			rb2d.velocity = new Vector2(-MaxSpeed, rb2d.velocity.y);
			/*if (rb2d.velocity.x > -MaxSpeed) {
				// if not yet at max force
				rb2d.AddForce(new Vector2(-Acceleration, 0.0f));
			} else {
				// otherwise, cap speed
				rb2d.velocity = new Vector2(-MaxSpeed, rb2d.velocity.y);
			}*/
		} else if (horizontal > 0.1f) { // player pressing right
			if (rb2d.velocity.x < MaxSpeed) {
				// if not yet at max force
				rb2d.AddForce(new Vector2(Acceleration, 0.0f));
			} else {
				// otherwise, cap speed
				rb2d.velocity = new Vector2(MaxSpeed, rb2d.velocity.y);
			}
		}

		// jumping
		bool onTheGround = isOnGround();

		if (onTheGround) {
			canDoubleJump = true;
		}

		if (Input.GetButton("Jump")) {
			if (!jumpKeyDown) { // 1st frame
				jumpKeyDown = true;

				if (onTheGround || (canDoubleJump && EnableDoubleJump) || wallHitDoubleJumpOverride) {
					bool wallHit = false;
					int wallHitDirection = 0;

					bool leftWallHit = isOnWallLeft();
					bool rightWallHit = isOnWallRight();

					if (horizontal != 0) {
						if (leftWallHit) {
							wallHit = true;
							wallHitDirection = 1;
						} else if (rightWallHit) {
							wallHit = true;
							wallHitDirection = -1;
						}
					}

					if (!wallHit) {
						if (onTheGround || (canDoubleJump && EnableDoubleJump)) {
							rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);

							jumpDuration = 0.0f;
							canVariableJump = true;
						}
					} else {
						rb2d.velocity = new Vector2(JumpSpeed * wallHitDirection, JumpSpeed);

						jumpDuration = 0.0f;
						canVariableJump = true;
					}

					if (!onTheGround && !wallHit) {
						canDoubleJump = false;
					}
				}
			} else if (canVariableJump) { // not 1st frame
				jumpDuration += Time.deltaTime;

				if (jumpDuration < MaxJumpDuration) {
					rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);
				}
			}
		} else { // player NOT pressing jump
			jumpKeyDown = false;
			canVariableJump = false;
		}
	}

	bool isOnGround() {
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.001f;

		Vector2 linestart = new Vector2 (
			transform.position.x,
			transform.position.y - collisionBox.bounds.extents.y - colliderThreshold);

		Vector2 vectorToSearch = new Vector2 (
			transform.position.x,
			linestart.y - lengthToSearch);

		RaycastHit2D hit = Physics2D.Linecast (linestart, vectorToSearch);

		return hit;
	}

	bool isOnWallLeft() {
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.01f;

		Vector2 linestart = new Vector2 (
			transform.position.x - collisionBox.bounds.extents.x - colliderThreshold,
			transform.position.y);

		Vector2 vectorToSearch = new Vector2 (
			linestart.x - lengthToSearch,
			transform.position.y);

		RaycastHit2D hit = Physics2D.Linecast (linestart, vectorToSearch);

		if (hit && hit.collider.CompareTag("NoSlideJump")) {
			return false;
		} else {
			return hit;
		}
	}

	bool isOnWallRight() {
		float lengthToSearch = 0.1f;
		float colliderThreshold = 0.01f;

		Vector2 linestart = new Vector2 (
			transform.position.x + collisionBox.bounds.extents.x + colliderThreshold,
			transform.position.y);

		Vector2 vectorToSearch = new Vector2 (
			linestart.x + lengthToSearch,
			transform.position.y);

		RaycastHit2D hit = Physics2D.Linecast (linestart, vectorToSearch);

		if (hit && hit.collider.CompareTag("NoSlideJump")) {
			return false;
		} else {
			return hit;
		}
	}

}