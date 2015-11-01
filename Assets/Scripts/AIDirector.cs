using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDirector : MonoBehaviour {

	public float player_health;
	public float NPC_health;
	public string NPC_Type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Kirby Gagne
	///<summary>
	///The health of the Player (or player controlled items) is sent to the AI Director
	///for record keeping and for use in determining NPC strategies
	///</summary>
	/// 
	///<param name="health">>> the health of the player
	///
	// -------------------------------------------------------------------------

	public void setPlayerHealth(float health){
		player_health = health;
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Kirby Gagne
	///<summary>
	///The health of a particular NPC is passed to the AI Director to give the AI Director
	///knowledge on the current status of Director-controlled NPCs
	///</summary>
	/// 
	///<param name="health">>> the health of the NPC
	///
	// -------------------------------------------------------------------------
	public void getNPCHealth(float health){
		NPC_health = health;
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author James Rossel
	///<summary>
	///An NPCs particular type is sent to the AI Director for use in calulating strategies
	///and tactics
	///</summary>
	/// 
	///<param name="type">>> the NPC's type
	///
	// -------------------------------------------------------------------------
	public void getNPCType(string type){
		type = NPC_Type;
	}
	
	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author James Rossel
	///<summary>
	///This function is receiving information from Director-controlled NPCs indicating all new visible
	///Game Objects that the NPCs can react to. Given this information, the AI Director will decide on whether it would
	///be better to pursue the new Game Object
	///</summary>
	/// 
	///<param name="huntThisItem">>> a new target for the NPCs to hunt on the map
	///
	// -------------------------------------------------------------------------
	public void setTargetForNPCs(string huntThisItem){
		var objects = GameObject.FindGameObjectsWithTag("Hunter");
		
		foreach(var npc in objects){
			npc.gameObject.GetComponent<Hunt>().addHuntTarget(huntThisItem);
		}
	}
}
