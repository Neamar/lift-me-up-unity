using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
	public float SPEED = 0.05f;

	public Floor destination;
	public Lift lift = null;
	public Floor floor = null;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 currentPosition = this.transform.position;
		if (lift != null && floor == null) {

			float targetCoordinate = lift.transform.position.x;

			if (targetCoordinate > currentPosition.x) {
				currentPosition.x += SPEED;
			} else if (targetCoordinate < currentPosition.x) {
				currentPosition.x -= SPEED;
			}

			if (Mathf.Abs (targetCoordinate - currentPosition.x) < SPEED) {
				currentPosition.x = targetCoordinate;
			}

			if (currentPosition.x == targetCoordinate) {
				currentPosition.z += SPEED;
			}

			if (currentPosition.z < 0 && currentPosition.x == targetCoordinate) {
				currentPosition.z = 0;
				lift.UserHasBoarded (this);
				lift = null;
			}
		}

		if (floor != null) {
			currentPosition.z -= SPEED;

			if (currentPosition.z < -1) {
				currentPosition.z = -1;
				lift.UserHasUnBoarded (this);
				lift = null;
				floor = null;
			}
		}

		this.transform.position = currentPosition;
	}

	public void assignLift (Lift lift)
	{
		this.lift = lift;
	}

	public void assignFloor (Floor floor, Lift lift)
	{
		this.floor = floor;
		this.lift = lift;
	}
}
