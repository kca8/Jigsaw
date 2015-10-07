using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {
	public Transform target; //the enemy's target
	public float moveSpeed = 3; //move speed
	public float rotationSpeed = 2; //speed of turning
	
	public int currentWaypoint = 0;
	public Transform waypoint;
	public Transform[] waypointList;
	
	
	// Use this for initialization
	void Start () {
		//target = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		//float step = 1/2 * moveSpeed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		
		if(currentWaypoint < this.waypointList.Length)
		{
			if(waypoint == null)
				waypoint = waypointList[currentWaypoint];
			
			walk();
		}
		
		else
		{
			currentWaypoint = 0;
			print ("Reseting Waypoints!\n");
			waypoint = waypointList[currentWaypoint];
			print ("Current Waypoint = " + currentWaypoint + "\n");
		}
	}
	
	void walk(){
		
		// rotate towards the target
		transform.forward = Vector3.RotateTowards(transform.forward, waypoint.position - transform.position, moveSpeed*Time.deltaTime, 0.0f);
		
		// move towards the target
		transform.position = Vector3.MoveTowards(transform.position, waypoint.position, moveSpeed*Time.deltaTime);
		
		if(transform.position == waypoint.position)
		{
			currentWaypoint ++ ;
			waypoint = waypointList[currentWaypoint];
			print ("Reached waypoint " + currentWaypoint + "\n");
			print ("Waypoint Length = " + this.waypointList.Length + "\n");
		}
		
	} 
}
