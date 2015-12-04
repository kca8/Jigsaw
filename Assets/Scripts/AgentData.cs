using UnityEngine;
using System.Collections;

public class AgentData : MonoBehaviour {

	// Struct that holds the agent that spawned which you can access its id
	// And its total distance
	public struct agentData
	{
		public GameObject agent;
		public float distance;
	};
	
	agentData Agent = new agentData();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject getAgent(){
	
		return Agent.agent;
	}
	
	public float getDistance(){
		return Agent.distance;
	}
	
	public void setAgent(GameObject agent){
		Agent.agent = agent;
	}
	
	public void setDistance(float distance){
		Agent.distance = distance;
	}
}
