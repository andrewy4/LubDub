﻿using UnityEngine;
using System.Collections;


public class FlyingScript : MonoBehaviour {

	/**********
	 *Rotation*
	 **********/
	public Vector3 randomPosition;
	public float degreesPerSec = 360f;

	/********
	 *Moving*
	 ********/
	public float moveSpeed;

	/**************
	 * Drag Object*
	 **************/
	public GameObject gameObjectToDrag; //The object that is being dragged
	public Vector3 GoCenter; //game Object's center
	public Vector3 touchPosition; //touch position
	public Vector3 offset; //The vector between touchpoint to object center
	public Vector3 newGoCenter; //The new center of gameObject

	RaycastHit hit; //Store the hit information
	public bool draggingMode = false;




	// Use this for initialization
	void Start () {
	}
	void Update(){
		if (Input.GetMouseButtonDown (0))
		{
			//Convert Mouse into a ray
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			//if a ray hit a Collider
			if (Physics.Raycast (ray, out hit)) 
			{
				gameObjectToDrag = hit.collider.gameObject;
				GoCenter = gameObjectToDrag.transform.position;
				touchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				offset = touchPosition - GoCenter;
				draggingMode = true;
			}//end if(raycast)
		} //end if(mouse Button Down)
		
		//when the user is holding on touch
		if (Input.GetMouseButton(0)) 
		{
			
			if (draggingMode) 
			{
				
				Debug.Log (gameObjectToDrag.name);
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newGoCenter = touchPosition - offset;
				gameObjectToDrag.transform.position = new Vector3(newGoCenter.x, newGoCenter.y, GoCenter.z);
				Debug.Log (gameObjectToDrag.transform.position.x + " , " + gameObjectToDrag.transform.position.y);
			} //end if draggingMode
		}//end if holding mousing

		//when the touch is released
		if (Input.GetMouseButtonUp (0)) 
		{
			draggingMode = false;
			if (gameObjectToDrag != null) {
				if (gameObjectToDrag.name == "A") {
					if (gameObjectToDrag.transform.position.x < -1 && gameObjectToDrag.transform.position.x > -2 &&
					    gameObjectToDrag.transform.position.y < 1.5 && gameObjectToDrag.transform.position.y > 0.5) {

						Destroy (GameObject.FindWithTag ("positionA"));
						Destroy (GameObject.FindWithTag ("A"));
					}
				}
				
				if (gameObjectToDrag.name == "B") {
					if (gameObjectToDrag.transform.position.x < -0.75 && gameObjectToDrag.transform.position.x > -1.25 &&
						gameObjectToDrag.transform.position.y < -1.75 && gameObjectToDrag.transform.position.y > -2.5) {

						Destroy (GameObject.FindWithTag("positionB"));
						Destroy (GameObject.FindWithTag ("B"));
					}
				}
				if (gameObjectToDrag.name == "C") {
					if (gameObjectToDrag.transform.position.x < 1.75 && gameObjectToDrag.transform.position.x > 0.75 &&
						gameObjectToDrag.transform.position.y < 0.75 && gameObjectToDrag.transform.position.y > -0.25) {

						Destroy (GameObject.FindWithTag("positionC"));
						Destroy (GameObject.FindWithTag ("C"));
					}
				}
				gameObjectToDrag = null;
			}

		
		}//end if mouse release

	}//end update
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObjectToDrag == null || gameObjectToDrag.name != gameObject.name ) {
			
			randomPositionUpdate ();
			move ();
		}
	}

	/*Moving*/
	void move(){


		/*Rotation the piece*/
		float rotAmount = degreesPerSec * Time.deltaTime;
		float curRot = transform.localRotation.eulerAngles.z;
		transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, curRot + rotAmount));
		/*End rotation*/

		transform.position = Vector3.MoveTowards (transform.position, randomPosition, Time.deltaTime * moveSpeed);


	}

	/*Update the position of the next target place*/
	void randomPositionUpdate(){
		if (transform.position.x > randomPosition.x - 0.15 && transform.position.x < randomPosition.x + 0.15 &&
			transform.position.y > randomPosition.y - 0.15 && transform.position.y < randomPosition.y + 0.15) {
			randomPosition = new Vector3 (Random.Range (10, -10), Random.Range (6, -6));
		}


	}

		
}
