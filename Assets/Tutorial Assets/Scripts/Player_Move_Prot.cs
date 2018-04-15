using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prot : MonoBehaviour {

	public int playerSpeed = 10;
	private bool facingRight = false;
	public int playerJumpPower = 1250;
	private float moveX;
	public bool isGrounded;
	public float distanceToBottomOfPlayer = 0.9f;

	
	// Update is called once per frame
	void Update () {
		PlayerMove();
		PlayerRaycast ();
	}

	void PlayerMove() {
		// CONTROLS
		moveX = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown ("Jump") && isGrounded == true) {
			Jump ();
		}
		// ANIMATIONS
		if (moveX != 0.0f) {
			GetComponent<Animator> ().SetBool ("isRunning", true);
		} else {
			GetComponent<Animator> ().SetBool ("isRunning", false);
		}

		// PLAYER DIRECTION
		if (moveX < 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = true;
		} else if (moveX > 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = false;
		}
		// PHYSICS
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void Jump() {
		// JUMPING CODE
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
		isGrounded = false;
	}

	void PlayerRaycast() {
		//TODO FIX THIS DISGUSTING CODE
		//Ray UP
		RaycastHit2D rayUp = Physics2D.Raycast (transform.position, Vector2.up);
		if (rayUp != null && rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.tag == "enemy") {
			Destroy (rayUp.collider.gameObject);
		}

		//Ray Down
		RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
		if (rayDown != null && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag == "enemy") {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1000);
			rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 200);


		}
		if (rayDown != null && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "enemy") {
			isGrounded = true;
		}
	}
}
