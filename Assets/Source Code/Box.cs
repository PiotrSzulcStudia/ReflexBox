using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	// Constants
	private const float DragDistanceActivation = 1.5f;
	private const float ForceFactor = 20.0f;


	private bool isFalling;
	private float fallingSpeed;
	private float minFallingSpeed;
	private float maxFallingSpeed;

	// Drag Section
	private bool isAbleToBeDragged;
	private const float dragZ = 10.0f;
	private Vector3 mousePosition;

	// Area Bounds
	private float leftBound;
	private float rightBound;
	private float bottomBound = -20.0f;

	private GameController gc;

	private GameObject leftBoundObject;
	private GameObject rightBoundObject;

	void OnMouseDrag() {
		if (!isAbleToBeDragged) return;

		// Geting Mouse Input
		mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragZ);
		Vector3 objWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		if (Mathf.Abs(objWorldPosition.x - transform.position.x) > DragDistanceActivation) {
			objWorldPosition.y = 0; // force vector is now only affecting x cordination;
			GetComponent<Rigidbody>().AddForce(objWorldPosition * ForceFactor);
			isAbleToBeDragged = false;
			isFalling = false;
		}
	}
	
	void Start () {
		gc = GameObject.FindGameObjectWithTag(Tags.GAME).GetComponent<GameController>();


		determineBounds();
		isAbleToBeDragged = true;
		isFalling = true;
		fallingSpeed = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {

		// checking if we are over bounds
		if (transform.position.x < leftBound) {
			if (this.transform.tag == Tags.LEFT) {
				gc.AddPoints();
			} else {
				gc.LoseLife();
			}
			Destroy(gameObject);
		}
		if (transform.position.x > rightBound) {
			if (this.transform.tag == Tags.RIGHT) {
				gc.AddPoints();
			} else {
				gc.LoseLife();
			}
			Destroy(gameObject);
		}

		if (transform.position.y < bottomBound) {
			gc.LoseLife();
			Destroy(gameObject);
		}

		if (isFalling) {
			transform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
		}
	
	}
	

	private void determineBounds() {
		leftBoundObject = GameObject.FindGameObjectWithTag(Tags.LEFT_BOUND);
		rightBoundObject = GameObject.FindGameObjectWithTag(Tags.RIGHT_BOUND);

		leftBound = leftBoundObject.transform.position.x;
		rightBound = rightBoundObject.transform.position.x;
	}
}
