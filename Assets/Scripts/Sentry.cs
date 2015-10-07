using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sentry : MonoBehaviour {

	public Transform target; //the enemy's target
	public float moveSpeed = 3; //move speed
	public float rotationSpeed = 2; //speed of turning
	
	public int currentWaypoint = 0;
	public Transform waypointParent;
	public Transform waypoint_goto;
	public List<Transform> waypointList;
	
	// Use this for initialization
	void Start () {
		//Transform[] Path = waypoint.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in waypointParent){
			waypointList.Add(child.transform);
		}
		
		print("Size of waypointList = " + this.waypointList.Count);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(currentWaypoint < this.waypointList.Count)
		{
			if(waypoint_goto == null)
				waypoint_goto = waypointList[currentWaypoint];
			
			walk();
		}
		
		else
		{
			currentWaypoint = 0;
			print ("Reseting Waypoints!\n");
			waypoint_goto = waypointList[currentWaypoint];
			print ("Current Waypoint = " + currentWaypoint + "\n");
		}
	}
	
	void walk(){
		
		// rotate towards the target
		transform.forward = Vector3.RotateTowards(transform.forward, waypoint_goto.position - transform.position, moveSpeed*Time.deltaTime, 0.0f);
		
		// move towards the target
		transform.position = Vector3.MoveTowards(transform.position, waypoint_goto.position, moveSpeed*Time.deltaTime);
		
		if(transform.position == waypoint_goto.position)
		{
			currentWaypoint ++;
			waypoint_goto = waypointList[currentWaypoint];
			print ("Reached waypoint " + currentWaypoint + "\n");
			//print ("Path Length = " + this.Path.Length + "\n");
		}
		
	}
}
