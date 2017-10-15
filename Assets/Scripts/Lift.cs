using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
	public float SPEED = 0.05f;
	public Shaft shaft;

	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		Vector3 currentPosition = this.transform.position;

		float targetCoordinate = shaft.GetTargetFloorCoordinates ();

		if (targetCoordinate != currentPosition.y) {
			animator.SetTrigger ("hasTargetFloor");
		}

		if (targetCoordinate > currentPosition.y) {
			currentPosition.y += SPEED;
		} else if (targetCoordinate < currentPosition.y) {
			currentPosition.y -= SPEED;
		}

		if (Mathf.Abs (targetCoordinate - currentPosition.y) < SPEED) {
			currentPosition.y = targetCoordinate;
			animator.SetTrigger ("atTargetFloor");
		}

		this.transform.position = currentPosition;
	}
}
