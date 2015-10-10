using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sentry : MonoBehaviour {

	public Transform target; //the enemy's target
	public float moveSpeed = 3; //move speed
	public float rotationSpeed = 2; //speed of turning

	public int currentWaypoint = 0; //waypointList[] counter
	public Transform waypointParent; //The object containing a set of waypoint_goto's
	Transform waypoint_goto; //The current waypoint objective
	List<Transform> waypointList = new List<Transform>(); //An array of waypoint_goto's
	
	// Use this for initialization
	void Start () {
		
		foreach (Transform child in waypointParent){
			waypointList.Add(child.transform);
		}
		
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
			waypoint_goto = waypointList[currentWaypoint];
		}
		
	}
	
	//-----------------------------------------------------------------
	/// <summary>
	/// Walk this instance.
	/// </summary>
	//-----------------------------------------------------------------
	void walk(){
		
		// rotate towards the target
		transform.forward = Vector3.RotateTowards(transform.forward, waypoint_goto.position - transform.position, moveSpeed*Time.deltaTime, 0.0f);
		
		// move towards the target
		transform.position = Vector3.MoveTowards(transform.position, waypoint_goto.position, moveSpeed*Time.deltaTime);
		
		if(transform.position == waypoint_goto.position)
		{
			currentWaypoint ++;
			waypoint_goto = waypointList[currentWaypoint];
		}
		
	}
}