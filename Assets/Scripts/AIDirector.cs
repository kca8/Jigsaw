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
	
	private int agentAttempts = 0;
	
	public GameObject spawn;
	public GameObject Tank;
	public GameObject Bruiser;
	public GameObject Pipsqueak;
	
	public List<GameObject> SpawnList = new List<GameObject>();
	public List<AgentData> DeathList = new List<AgentData>();
	public List<AgentData> History = new List<AgentData>();
	
	private string[] emulatedPattern;
	private int index;
	private float bpsfVal; 
	
	// Use this for initialization
	void Start () {
		emulatedPattern = new string[4] {"P", "B", "T", "P"};
		bpsfVal = 61f;
		StartCoroutine(doThis ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator doThis() {
		//float tempTime = spawnAgent (timeOfLastSpawn, spawnRate);
		//if(tempTime >= 0) timeOfLastSpawn = tempTime;
		float timeToSpawn = 1.0f;
		
		while(timeOfLastSpawn >= 0){
			timeToSpawn = calcSpawnTime(decideAgentToSpawn());
			updateHistory();
			yield return new WaitForSeconds(timeToSpawn );
			spawnAgent();
		}
	}
	
	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Kirby Gagne
	///<summary>
	/// Takes the health of the player and NPC into account and calculates whether or not it should
	/// attack or run from the player based on probablity calculations.
	/// Sets either the fight or run local boolean variables to true when calling this function.
	///</summary>
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
	
	private int decideAgentToSpawn() {
		//0 is Tank
		//1 is Bruiser
		//2 is Pipsqueak
		float bestVal = 87f;
		float chance = bpsfVal / bestVal;
		float value = Random.value;
		int number = -1;
		if (value > chance) {
			//			Debug.Log ("random");
			index = 0;
			number = Random.Range (0, 3);
		}
		else {
			//			Debug.Log ("pattern");
			if (emulatedPattern[index] == "T"){
				number = 0;
			}
			else if (emulatedPattern[index] == "B"){
				number = 1;
			}
			else if (emulatedPattern[index] == "P"){
				number = 2;
			}
			else {
				Debug.Log("invalid Agent string in emulated pattern at index " + index + "; " + emulatedPattern[index]);
				number = -1;
			}
			index++;
			if(index >= emulatedPattern.GetLength(0)){
				index = 0;
				//				Debug.Log ("finished pattern");
			}
		}
		//		if(number == 0) Debug.Log ("T");
		//		else if(number == 1) Debug.Log ("B");
		//		else if(number == 2) Debug.Log ("P");
		return number;
	}
	
	private void updateHistory(){
		Debug.Log ("Updating History");
		if(DeathList.Count() != 0){
			for(int i = 0; i < DeathList.Count() - 1; i++){
				int deathID = DeathList[i].getID();
				Debug.Log ("Death ID " + i + " is : " + deathID + " and name is " + DeathList[i].getInitial());
				for(int j=0; j < History.Count() - 1; j++){
					if(History[j].getID() == deathID){
						Debug.Log ("Found ID at " + j + " of " + History[j].getID() + " with a name of " + History[j].getInitial());
						Debug.Log ("Death distance is " + DeathList[i].getDistance());
						History[j].setDistance(DeathList[i].getDistance());
						Debug.Log ("History has distance of " + History[j].getDistance());
					}
					else Debug.Log ("Death ID of " + deathID + "not matched to anything!!!!!");
				}
			}
		}
	}
	
	private float calcSpawnTime(int agentType){
		float returnTime = 0.0f;
		print ("In calcSpawnTime");
		
		if (agentType == 0) {
			print ("Adding Tank");
			SpawnList.Add (Tank);
		}
		
		else if(agentType == 1) {
			print ("Adding Bruiser");
			SpawnList.Add (Bruiser);
		}
		
		else {
			print ("Adding Pipsqueak");
			SpawnList.Add (Pipsqueak);
		}
		
		returnTime = SpawnList[0].GetComponent<Health>().getHealthValue()/100.0f;
		
		return returnTime;
	}

	private void spawnAgent(){
		Vector3 spawnPos = GameObject.FindGameObjectWithTag("Spawn").transform.position;
		Quaternion rotate = GameObject.FindGameObjectWithTag("Spawn").transform.rotation;
		
		GameObject npc = Instantiate(SpawnList[0], spawnPos, rotate) as GameObject;
		AgentData tempData = new AgentData();
		Debug.Log ("ID from npc : " + npc.GetInstanceID());
		tempData.setID(npc.GetInstanceID());
		Debug.Log ("ID from data : " + tempData.getID());
		tempData.setOtherAgents(findOtherAgents());
		Debug.Log ("OtherAgents : " + tempData.getOtherAgents());
		Debug.Log ("Name from npc : " + npc.name);
		tempData.setInitial(npc.name);
		Debug.Log ("Name from data : " + tempData.getInitial());

		npc.GetComponent<Sentry>().setAgentData(tempData);
		Debug.Log (npc.GetComponent<Sentry>().getAgentData().getInitial());

		History.Add(tempData);
		if (History[agentAttempts] == null) Debug.Log (agentAttempts + " was null");
		Debug.Log ("History ID : " + History[agentAttempts].getInitial());
		Debug.Log ("History: " + History.Count());
		agentAttempts++;
		SpawnList.RemoveAt(0);
		setWaypointsForNPCs();
	}
	
	public int getAgentAttempts(){
		return agentAttempts;
	}
	
	public void addToDeathList(AgentData data){
		DeathList.Add (data);
	}
	
	private int findOtherAgents(){
		int otherAgents = 0;
		var objects = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(var npc in objects){
			otherAgents++;
		}
		
		return otherAgents-1;
	}
}
