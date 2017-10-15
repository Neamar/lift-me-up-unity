using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaft : MonoBehaviour
{
	public Building Building;

	public int floorHeight = 2;
	public int targetFloor = 0;

	// Use this for initialization
	void Start ()
	{
		Vector3 localScale = this.transform.localScale;
		localScale.y = floorHeight * Building.floors.Length;
		this.transform.localScale = localScale;
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

	public Floor GetTargetFloor ()
	{
		return Building.floors [targetFloor];
	}
}
