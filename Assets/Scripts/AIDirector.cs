using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIDirector : MonoBehaviour {

	public float player_health;
	public float NPC_health;
	public string NPC_Type;
	public int calc;
	public int calc_ans;
	public int ans;
	public bool fight;
	public bool run;
	
	public float timeOfLastSpawn = 0.0f;
	float spawnRate = 0.0f;
	
	public GameObject spawn;
	public Transform Tank;
	public Transform Bruiser;
	public Transform Pipsqueak;
	private Object instObj;
	public List<Object> SpawnList = new List<Object>();
	
	float spawnTime = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float tempTime = spawnAgent (timeOfLastSpawn, spawnRate);
		if(tempTime >= 0) timeOfLastSpawn = tempTime;
	}
	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Kirby Gagne
	///<summary>
	/// Takes the health of the player and NPC into account and calculates whether or not it should
	/// attack or run from the player based on probablity calculations.
	/// Sets either the fight or run local boolean variables to true when calling this function.
	///</summary>
	/// 
	///
	///
	// -------------------------------------------------------------------------
	public void CalculateStrat(){
		print ("inCalculate");
		fight = false;
		run = false;
		calc = (int)player_health + (int)NPC_health;
		print ("calc ans is\n");
		print (calc_ans);
		calc_ans = Random.Range (1, calc);
		while (calc_ans > 100) {
			ans = calc_ans - 100;
			calc_ans = Random.Range (1, calc_ans);
			ans = Random.Range (1, ans);
			calc_ans = calc_ans - ans;
		}
		if (calc_ans >= 50) {
			print ("over 50 fight should be true\n");
			print ("calc ans is\n");
			print (calc_ans);
			fight = true;
		} else {
			print ("under 50 run should be true\n");
			print ("calc ans is\n");
			print (calc_ans);
			run = true;
		}
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
	///be better to pursue the new Game Object. Calls the function CalculateStrat if the boolean fight is true then
	/// NPC's will persue the new Game Object.
	///</summary>
	/// 
	///<param name="huntThisItem">>> a new target for the NPCs to hunt on the map
	///
	// -------------------------------------------------------------------------
	public void setTargetForNPCs(string huntThisItem){
		print ("inSetTarget");
		CalculateStrat ();
		if (fight == true) {
			var objects = GameObject.FindGameObjectsWithTag("Hunter");
			
			foreach(var npc in objects){
				npc.gameObject.GetComponent<Hunt>().addHuntTarget(huntThisItem);
			}

		}
	}
	
	public void setWaypointsForNPCs(){
		print ("inSetWaypoint");
		var objects = GameObject.FindGameObjectsWithTag("Enemy");
		Transform waypointList = GameObject.FindGameObjectWithTag("WaypointParent").transform;
			
		foreach(var npc in objects){
			npc.gameObject.GetComponent<Sentry>().addNewWaypointParent(waypointList);
		}
	}

	private float spawnAgent(float lastSpawn, float rate){
		Vector3 spawnPos = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		Quaternion rotate = GameObject.FindGameObjectWithTag("Spawn").transform.rotation;
		float returnTime;
		
		if(Time.time > lastSpawn + rate) {
			int number = Random.Range (0, 3);
			
			if (number == 0) {
				returnTime = Time.time+5.0f;
				SpawnList.Add (Tank);
			}
			
			else if(number == 1) {
				returnTime = Time.time+3.0f;
				SpawnList.Add (Bruiser);
			}
			
			else {
				returnTime = Time.time+1.0f;
				SpawnList.Add (Pipsqueak);
			}
			
			instObj = Instantiate(SpawnList[0], spawnPos, rotate);
			SpawnList.RemoveAt (0);
			setWaypointsForNPCs();
			
			return returnTime;
		}
		
		else
			return -1.0f;
	}
}
