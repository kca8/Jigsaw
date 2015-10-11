using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sentry : MonoBehaviour {

	public Transform target; //the enemy's target
	public float sight = 5.0f; //Sight radius of the agent
	public float moveSpeed = 3; //move speed
	public float rotationSpeed = 2; //speed of turning

	public int currentWaypoint = 0; //waypointList[] counter
	public Transform waypointParent; //The object containing a set of waypoint_goto's
	Transform waypoint_goto; //The current waypoint objective
	List<Transform> waypointList = new List<Transform>(); //An array of waypoint_goto's
	//public Waypoint thisWaypoint;
	
	public bool searchForObject = false;
	
	Vector3 origin;
	
	
	// Use this for initialization
	void Start () {
		
		foreach (Transform child in waypointParent){
			waypointList.Add(child.transform);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(searchForObject == false)
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
		
		if (Input.GetKey(KeyCode.H))
		{
			foreach (Transform child in waypointParent) {
				print(child.gameObject.GetComponent<Waypoint>().calcDistance(child.transform));
			}
		}
		
		if (Input.GetKey(KeyCode.J))
		{
			foreach (Transform child in waypointParent) {
				print(child.gameObject.GetComponent<Waypoint>().calcDistanceToWaypoint(this.transform, child.transform));
			}
		}
			//findPath(origin, sight, "Waypoint");
			
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
			currentWaypoint++;
			waypoint_goto = waypointList[currentWaypoint];
		}
		
	}
	
//	public static Vector3 findPath(Vector3 center, float radius, string refTag) {
//		center.y = 0f;
//		Vector3 detectTotal = Vector3.zero;	
//		Vector3 fleeVector = Vector3.zero;
//		bool detected = false;
//		
//		Transform[] seenObjects = Physics.OverlapSphere(center, radius); 
//		for (int i = 0; i < seenObjects.Length; i++) {
//			if(seenObjects[i].gameObject.transform.tag == refTag){ 
//				detected = true;
//				Vector3 detectCurrent = center - seenObjects[i].gameObject.transform.position; 
//				detectCurrent.y = 0f; 
//				detectTotal = detectCurrent + detectTotal;
//			}
//		}
//		if (detected) {
//			fleeVector = detectTotal;
//			return fleeVector.normalized;
//		}
//		else {
//			return Vector3.zero; 
//		}
//	}
}