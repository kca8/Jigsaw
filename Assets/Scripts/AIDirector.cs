using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDirector : MonoBehaviour {

	public float player_health;
	public int NPC_health;
	public int NPC_Type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPlayerHealth(float health){
		player_health = health;
	}

	public void getNPCHealth(int health){
		NPC_health = health;
	}

	public void getNPCType(int type){
		NPC_Type = type;
	}
	
	public void setTargetForNPCs(string huntThisItem){
		var objects = GameObject.FindGameObjectsWithTag("Hunter");
		//List<Hunt> hunters = new List<Hunt>();
		
		foreach(var npc in objects){
			npc.gameObject.GetComponent<Hunt>().addHuntTarget(huntThisItem);
		}
	}
}
