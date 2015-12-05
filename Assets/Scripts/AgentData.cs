using UnityEngine;
using System.Collections;

public class AgentData : MonoBehaviour{

	private int id;	
	private string initial;
	private float distance;
	private int otherAgents;
	
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
	
	public int getOtherAgents(){
		return otherAgents;
	}

	public void setDistance(float newDistance){
		distance = newDistance;
	}

	public void setID(int newID){
		id = newID;
	}

	public void setInitial(string newInitial){
		initial = newInitial[0].ToString();
	}
	
	public void setOtherAgents(int newOther){
		otherAgents = newOther;
	}
	
//	public string ToString(){
//		return "" + agent;
//	}
}
