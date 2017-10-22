using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public GameObject Shaft;
	public GameObject Floor;
	public GameObject User;

	public List<Floor> floors = new List<Floor> ();

	// Use this for initialization
	void Start ()
	{
		MakeFloors (5);
		MakeShafts (2);
	}

	public void MakeFloors (int floorCount)
	{
		for (int i = 0; i < floorCount; i++) {
			GameObject floor = Instantiate (Floor, new Vector3 (), Quaternion.identity, this.transform);
			Vector3 p = floor.transform.position;
			p.y = -6.5f + (12 * ((float)i) / floorCount);
			Debug.Log ("i" + i + ", Y" + p.y);
			floor.transform.position = p;

			floors.Add (floor.GetComponent<Floor> ());
		}
	}

	public void MakeShafts (int shaftCount)
	{
		for (int i = 0; i < shaftCount; i++) {
			GameObject shaft = Instantiate (Shaft, new Vector3 (), Quaternion.identity, this.transform);
			Vector3 p = shaft.transform.position;
			p.x = -4f + 8 * i / shaftCount;
			p.y = -6.5f + 12 / 2;
			shaft.transform.position = p;

			Vector3 scale = shaft.transform.localScale;
			scale.y = 12;
			shaft.transform.localScale = scale;

			// Scale the lift
			Transform lift = shaft.GetComponentsInChildren<Transform> () [1];
			scale = lift.localScale;
			scale.y = 1f / floors.Count;
			lift.localScale = scale;

			Shaft shaftScript = shaft.GetComponent<Shaft> ();
			shaftScript.Building = this;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
