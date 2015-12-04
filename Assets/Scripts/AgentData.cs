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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject getAgent(){
		return agentData.agent;
	}
	
	public float getDistance(){
		return agentData.distance;
	}
	
	public void setAgent(GameObject agent){
		agentData.agent = agent;
	}
	
	public void setDistance(float distance){
		agentData.distance = distance;
	}
}
