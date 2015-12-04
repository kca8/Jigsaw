using UnityEngine;
using System.Collections;

public class AgentData {

	// Struct that holds the agent that spawned which you can access its id
	// And its total distance
//	public struct agentData
//	{
//		public GameObject agent;
//		public int otherAgents;
//		public float distance;
//	};
	
//	agentData Agent = new agentData();
	
	
	private GameObject agent;
	private int otherAgents;
	private float distance;
	
	// Use this for initialization
	void Start () {
		agent = null;
		otherAgents = 0;
		distance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject getAgent(){
	
		return agent;
	}
	
	public float getDistance(){
		return distance;
	}
	
	public void setAgent(GameObject newAgent){
		agent = newAgent;
	}
	
	public void setDistance(float newDistance){
		distance = newDistance;
	}
	
	public string ToString(){
		return "" + agent;
	}
}
