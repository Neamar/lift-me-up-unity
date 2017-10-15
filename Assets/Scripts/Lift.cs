using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
	public static int STATE_IDLE = 0;
	public static int STATE_MOVING = 1;
	public static int STATE_WAITING = 2;

	public float SPEED = 0.05f;
	public Shaft shaft;

	public int currentState = STATE_IDLE;
	private Animator animator;

	private User[] waitingForUsers;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (currentState == STATE_IDLE || currentState == STATE_MOVING) {
			
			Vector3 currentPosition = this.transform.position;

			float targetCoordinate = shaft.GetTargetFloorCoordinates ();

			if (targetCoordinate != currentPosition.y) {
				animator.SetTrigger ("hasTargetFloor");
				currentState = STATE_MOVING;
			}

			if (targetCoordinate > currentPosition.y) {
				currentPosition.y += SPEED;
			} else if (targetCoordinate < currentPosition.y) {
				currentPosition.y -= SPEED;
			}

			if (Mathf.Abs (targetCoordinate - currentPosition.y) < SPEED) {
				currentPosition.y = targetCoordinate;
				reachedTargetFloor ();
			}

			this.transform.position = currentPosition;
		}
	}

	private void reachedTargetFloor ()
	{
		Debug.Log ("Reached target floor");
		animator.SetTrigger ("atTargetFloor");

		Floor floor = shaft.GetTargetFloor ();

		if (floor.UsersWaiting.Length > 0) {
			for (int i = 0; i < floor.UsersWaiting.Length; i++) {
				floor.UsersWaiting [i].assignLift (this);
			}
			waitingForUsers = floor.UsersWaiting;
			floor.UsersWaiting = new User[0];
			currentState = STATE_WAITING;
		}
	}
}
