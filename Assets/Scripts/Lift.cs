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

	public int currentState = STATE_MOVING;
	private Animator animator;

	public List<User> usersBoarding;
	public List<User> usersInLift = new List<User> ();
	public List<User> usersUnboarding = new List<User> ();

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

			if (Mathf.Abs (targetCoordinate - currentPosition.y) < SPEED && currentState == STATE_MOVING) {
				currentPosition.y = targetCoordinate;
				reachedTargetFloor ();
			}

			this.transform.position = currentPosition;
		}
	}

	/**
	 * Lift was called at target floor, and has finished moving
	 */
	private void reachedTargetFloor ()
	{
		Debug.Log ("Reached target floor");
		animator.SetTrigger ("atTargetFloor");

		Floor floor = shaft.GetTargetFloor ();

		// Do we need to onboard someone?
		if (floor.UsersWaiting.Length > 0) {
			for (int i = 0; i < floor.UsersWaiting.Length; i++) {
				floor.UsersWaiting [i].assignLift (this);
			}
			usersBoarding = new List<User> (floor.UsersWaiting);
			floor.UsersWaiting = new User[0];
			currentState = STATE_WAITING;
		}

		// Do we need to unboard someone?
		for (int i = 0; i < usersInLift.Count; i++) {
			if (usersInLift [i].destination == shaft.GetTargetFloor ()) {
				usersUnboarding.Add (usersInLift [i]);
				usersInLift [i].transform.parent = null; // shaft.Building.transform;
				usersInLift [i].assignFloor (shaft.GetTargetFloor (), this);
				currentState = STATE_WAITING;
			}
		}
		usersInLift.RemoveAll (u => usersUnboarding.Contains (u));

		if (currentState != STATE_WAITING) {
			currentState = STATE_IDLE;
		}
	}

	public void UserHasBoarded (User boardedUser)
	{
		usersBoarding.Remove (boardedUser);
		usersInLift.Add (boardedUser);
		PotentiallyBackToIdle ();

		boardedUser.transform.parent = this.transform;
	}

	public void UserHasUnBoarded (User unboardedUser)
	{
		usersUnboarding.Remove (unboardedUser);
		PotentiallyBackToIdle ();
	}

	public void PotentiallyBackToIdle ()
	{
		if (usersBoarding.Count == 0 && usersUnboarding.Count == 0) {
			currentState = STATE_IDLE;
		}
	}
}
