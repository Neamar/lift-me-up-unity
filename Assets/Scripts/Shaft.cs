using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaft : MonoBehaviour
{
	public Building Building;

	public float floorHeight = 2;
	public int targetFloor = 0;

	// Use this for initialization
	void Start ()
	{
		floorHeight = (float)Building.floors.Count / 12;
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
