using UnityEngine;
using System.Collections;

public class ballScript : MonoBehaviour {
	private Rigidbody2D heartPiece;
	private float maxSpeed;
	private float randomSpeed;
	private int guess;
	// Use this for initialization
	void Start () {
		heartPiece = GetComponent<Rigidbody2D> ();
		randomSpeed = Random.Range (0.3f, 1f);
		while (guess == 0) {
			guess = Random.Range (-2, 2);
		}
		maxSpeed = Random.Range (10f, 15f);
		heartPiece.velocity = new Vector2 (guess * randomSpeed, guess * randomSpeed);


	}
	

	void FixedUpdate () {
		float xSpeed = heartPiece.velocity.x;
		float ySpeed = heartPiece.velocity.y;
		if (heartPiece.velocity.magnitude >= maxSpeed)
			heartPiece.velocity = heartPiece.velocity.normalized * maxSpeed;
		else {
			if (xSpeed >= 0) {
				heartPiece.AddForce (new Vector2 (randomSpeed, 0));
			} else {
				heartPiece.AddForce (new Vector2 (-1*randomSpeed, 0));
			}
			if (ySpeed >= 0) {
				heartPiece.AddForce (new Vector2 (0, randomSpeed));
			} else {
				heartPiece.AddForce (new Vector2 (0, -1*randomSpeed));
			}
		}
	}
}
