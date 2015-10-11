using UnityEngine;
using System.Collections;

public class WaypointList : MonoBehaviour {
	
	public Transform target; //the enemy's target
	public Transform thisWaypoint;
	public Transform waypointParent; //The object containing a set of waypoint_goto's
	
	// Use this for initialization
	void Start () {
		
		if(target != null)
			print("Please press 'C' to set a goal for all child waypoints\n");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.C)) {
			
			foreach(Transform child in waypointParent) {
				child.gameObject.GetComponent<Waypoint>().setGoal(target);
				child.gameObject.GetComponent<Waypoint>().calcDistance();
			}
			
			print("Goal set for all waypoints!\n");
		}
	}
}
