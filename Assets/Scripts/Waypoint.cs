using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
	
	public Transform goal;
	float distance = 0.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.G))
		{
			distance = calcDistance();
			printDistance();
		}
	}
	
	public void printDistance() {
		print("Distance from Goal: " + distance + "\n");
	}
	
	private float calcDistance() {
		float distance2 = Vector3.Distance(transform.position, goal.position);
		
		return distance2;
	}
	
	public float calcDistance(Transform origin) {
		print ("In calcDistance\n");
		float distance2 = Vector3.Distance(origin.position, goal.position);
		
		return distance2;
	}
	
	public float calcDistanceToWaypoint(Transform origin, Transform waypoint) {
		print ("In calcDistanceToWaypoint\n");
		float distance2 = Vector3.Distance(origin.position, waypoint.position);
		
		return distance2;
	}
}
