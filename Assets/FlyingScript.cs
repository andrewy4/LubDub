using UnityEngine;
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
	public GameObject gameObjectTodrag; //The object that is being dragged
	public Vector3 GOcenter; //game Object's center
	public Vector3 touchPosition; //touch position
	public Vector3 offset; //The vector between touchpoint to object center
	public Vector3 newGOCenter; //The new center of gameObject

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
				gameObjectTodrag = hit.collider.gameObject;
				GOcenter = gameObjectTodrag.transform.position;
				touchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				offset = touchPosition - GOcenter;
				draggingMode = true;
			}//end if(raycast)
		} //end if(mouse Button Down)

		//when the user is holding on touch
		if (Input.GetMouseButton(0)) 
		{
			if (draggingMode) 
			{
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newGOCenter = touchPosition - offset;
				gameObjectTodrag.transform.position = new Vector3(newGOCenter.x, newGOCenter.y, GOcenter.z);
			} //end if draggingMode
		}//end if holding mousing

		//when the touch is released
		if (Input.GetMouseButtonUp (0)) 
		{
			draggingMode = false;
			gameObjectTodrag = null;
		}//end if mouse release

	}//end update
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObjectTodrag == null || gameObjectTodrag.name != gameObject.name ) {

			/*Rotation the piece*/
			float rotAmount = degreesPerSec * Time.deltaTime;
			float curRot = transform.localRotation.eulerAngles.z;
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, curRot + rotAmount));
			/*End rotation*/

			/*Random Moving*/
			randomPositionUpdate ();
			transform.position = Vector3.MoveTowards (transform.position, randomPosition, Time.deltaTime * moveSpeed);
		}
	}


	/*Update the position of the next target place*/
	void randomPositionUpdate(){
		if (transform.position.x > randomPosition.x - 0.15 && transform.position.x < randomPosition.x + 0.15 &&
			transform.position.y > randomPosition.y - 0.15 && transform.position.y < randomPosition.y + 0.15) {
				randomPosition = new Vector3 (Random.Range (10, -10), Random.Range (6, -6));
		}
	}

		
}
