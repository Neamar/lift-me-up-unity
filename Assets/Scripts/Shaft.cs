using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaft : MonoBehaviour
{
	public int floorHeight = 2;
	private int numberOfFloors;

	public int targetFloor = 0;

	// Use this for initialization
	void Start ()
	{
		numberOfFloors = (int)this.transform.localScale.y / floorHeight;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnMouseUpAsButton ()
	{
		Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		targetFloor = (int)((worldCoordinates.y + transform.localScale.y / 2) / floorHeight);

		Debug.Log ("New target floor: " + targetFloor);
	}

	public float GetTargetFloorCoordinates ()
	{
		return targetFloor * floorHeight - transform.localScale.y / 2 + floorHeight / 2;
	}
}
