using UnityEngine;
using System.Collections;

public class FlyingScript : MonoBehaviour {
	public Vector3 randomPosition;
	private Rigidbody2D heart;
	public float rotateSpeed;
	public float moveSpeed;
	private bool startMoving = false;

 
	// Use this for initialization
	void Start () {
		heart = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		if ( Input.GetMouseButtonDown (0)) {
			startMoving = true;
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (startMoving) {
			heart.MoveRotation (heart.rotation + rotateSpeed * Time.deltaTime);
			randomPositionUpdate ();

			transform.position = Vector3.MoveTowards (transform.position, randomPosition, Time.deltaTime * moveSpeed);
		}
	}

	void randomPositionUpdate(){
		if (transform.position.x > randomPosition.x - 0.15 && transform.position.x < randomPosition.x + 0.15 &&
			transform.position.y > randomPosition.y - 0.15 && transform.position.y < randomPosition.y + 0.15) {
				randomPosition = new Vector3 (Random.Range (10, -10), Random.Range (6, -6));
		}
	}

		
}
