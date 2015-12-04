using UnityEngine;
using System.Collections;

public class AgentData{

	private int id;	
	private string initial;
	private float distance;
	//private int otherAgents;
	
	// Use this for initialization
	void Start () {
		id = -1;
		initial = "X";
		distance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getDistance(){
		return distance;
	}

	public int getID(){
		return id;
	}

	public string getInitial(){
		return initial;
	}

	public void setDistance(float newDistance){
		distance = newDistance;
	}

	public void setID(int newID){
		id = newID;
	}

	public void setInitial(string newInitial){
		initial = newInitial;
	}
	
//	public string ToString(){
//		return "" + agent;
//	}
}
