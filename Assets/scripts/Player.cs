using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public delegate void OnHitSpikeAction();
	public delegate void OnHitEnemyAction();
	public delegate void OnHitOrbAction();

	public OnHitSpikeAction onHitSpike;
	public OnHitEnemyAction onHitEnemy;
	public OnHitOrbAction onHitOrb;

	private Rigidbody2D rb;

	private float moveForce = 1000f;
	private float jumpForce = 50000;
	private float xClamp;
	private bool canJump;

	private void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	private void Update () {
		GetInput ();
	}

	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "solid") {
			canJump = true;
		}
		if (col.gameObject.tag == "spike") {
			if (onHitSpike != null) {
				onHitSpike ();
			}		
		} else if (col.gameObject.tag == "enemy") {
			if (transform.position.y > col.gameObject.transform.position.y + col.gameObject.GetComponent<BoxCollider2D> ().size.y / 2) {
				Destroy (col.gameObject);
				Jump (true);
				if (onHitEnemy != null) {
					onHitEnemy ();
				}
			} else {
				if (onHitSpike != null) {
					onHitSpike ();
				}
			}
		} else if (col.gameObject.tag == "orb") {
			if (onHitOrb != null) {
				onHitOrb ();
			}
		}
	}

	private void GetInput () {
		float xMove = Input.GetAxisRaw ("Horizontal");
		rb.AddForce (new Vector2 (xMove * moveForce * Time.deltaTime, 0));

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
	}

	private void Jump (bool force = false) {
		if (canJump || force) {
			canJump = false;
			rb.AddForce (Vector2.up * jumpForce * Time.deltaTime);
		}
	}
}
