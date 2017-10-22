using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public GameObject Shaft;
	public GameObject Floor;
	public GameObject User;

	public Floor[] floors;

	// Use this for initialization
	void Start ()
	{
		GameObject ground = Instantiate (Floor);
		GameObject firstFloor = Instantiate (Floor);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
