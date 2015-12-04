using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sentry : MonoBehaviour {

	public Transform target; //the enemy's target
	private Transform goal; //the goal to reach
	public float sight = 5.0f; //Sight radius of the agent
	public float moveSpeed = 3; //move speed
	public float rotationSpeed = 2; //speed of turning

	public int currentWaypoint = 0; //waypointList[] counter
	public int currentPath = 0; //waypointList[] counter
	public Transform waypointParent; //The object containing a set of waypoint_goto's
	Transform waypoint_goto; //The current waypoint objective
	List<Transform> waypointList = new List<Transform>(); //An array of waypoint_goto's
	List<Transform> waypointPath = new List<Transform>(); //An array of waypoint_goto's
	
	public bool searchForObject = false;
	private LevelManager levelManager;
	private Vector3 origin;
	public string findThis;
	public Transform previousWaypoint;
	public float distanceFrom = 0;
	public float totalDistance = 0;

	public AgentData data;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		foreach (Transform child in waypointParent){
			waypointList.Add(child.transform);
		}
		goal = waypointList.Last();
		previousWaypoint = waypointList.First ();
	}
	
	// Update is called once per frame
	void Update () {
		origin = transform.position;
		
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
		
		if (Input.GetKey(KeyCode.F))
		{
			findPath(origin, sight, findThis);
			
			sortPathByDist();
			
			if(currentWaypoint < this.waypointPath.Count)
			{
				if(waypoint_goto == null)
					waypoint_goto = waypointPath[currentPath];
				
				followPath();
			}
			
			else
			{
				currentPath = 0;
				waypoint_goto = waypointPath[currentPath];
			}
			
			waypointPath.Clear();
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
		
		currentPath = 0;
		waypointPath.Clear();
			
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
		
		distanceFrom = Mathf.Abs(transform.position.x - previousWaypoint.position.x + transform.position.z - previousWaypoint.position.z);
		
		if(transform.position == waypoint_goto.position)
		{
			currentWaypoint++;
			previousWaypoint = waypointList[currentWaypoint - 1];
			totalDistance += distanceFrom;
			distanceFrom = 0;
			if (waypoint_goto == goal){
				levelManager.LoadNextLevel(gameObject.name);
			}
			waypoint_goto = waypointList[currentWaypoint];
		}
	}


	void OnDestroy(){
		totalDistance += distanceFrom;
		data.setDistance(totalDistance);


		GameObject.FindGameObjectWithTag("AIDirector").GetComponent<AIDirector>().addToDeathList(data);
	}
	
	void findPath(Vector3 center, float radius, string refTag) {
		center.y = 0f;
		
		Collider[] seenObjects = Physics.OverlapSphere(center, radius); 
		for (int i = 0; i < seenObjects.Length; i++) {
			if(seenObjects[i].gameObject.transform.tag == refTag){ 
				print("Item detected!");
				waypointPath.Add(seenObjects[i].gameObject.transform);
			}
		}
		
		print("Found objects = " + waypointPath.Count + "\n");
	}
	
	void sortPathByDist() {
		waypointPath.Sort(SortByDist);
	}
	
	void followPath() {
		
		// rotate towards the target
		transform.forward = Vector3.RotateTowards(transform.forward, waypointPath[currentPath].position - transform.position, moveSpeed*Time.deltaTime, 0.0f);
		
		// move towards the target
		transform.position = Vector3.MoveTowards(transform.position, waypointPath[currentPath].position, moveSpeed*Time.deltaTime);
		
		if(transform.position == waypoint_goto.position)
		{
			currentPath++;
			waypoint_goto = waypointPath[currentPath];
		}
		
	}
	
	static int SortByDist(Transform p1, Transform p2)
	{
		return p1.GetComponent<Waypoint>().getDist().CompareTo(p2.GetComponent<Waypoint>().getDist());
	}
	
	public void addNewWaypointParent(Transform newParent){
		waypointParent = newParent;
		
		waypointList.Clear();
		
		foreach (Transform child in waypointParent){
			waypointList.Add(child.transform);
		}
	}
	
	public AgentData getAgentData(){
		return data;
	}

	public void setAgentData(AgentData newData){
		data = newData;
	}
}