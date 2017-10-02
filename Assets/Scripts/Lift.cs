using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
	public float SPEED = 0.05f;
	public Shaft shaft;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 currentPosition = this.transform.position;

		float targetCoordinate = shaft.GetTargetFloorCoordinates ();

		if (targetCoordinate > currentPosition.y) {
			currentPosition.y += SPEED;
		} else if (targetCoordinate < currentPosition.y) {
			currentPosition.y -= SPEED;
		}

		if (Mathf.Abs (targetCoordinate - currentPosition.y) < SPEED) {
			currentPosition.y = targetCoordinate;
		}

		this.transform.position = currentPosition;
	}
}
