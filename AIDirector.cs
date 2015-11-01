using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDirector : MonoBehaviour {

	public float player_health;
	public int NPC_health;
	public int NPC_Type;
	private bool strat;
	private int calculation;
	private int ans;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CalculateStrat(){
		calculation = NPC_health + (int)player_health;
		calculation = Random.Range (1, calculation);
		while (calculation > 101) {
			ans = calculation - 100;
			ans = Random.Range (1, ans);
			calculation = calculation - ans;
		}
		if (calculation >= 50) {
			strat = true;
		} else if (calculation < 49) {
			strat = false;
		}

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

		if (strat == true) {
			var objects = GameObject.FindGameObjectsWithTag("Hunter");
			print ("its a hunter");
			foreach(var npc in objects){
				npc.gameObject.GetComponent<Hunt>().addHuntTarget(huntThisItem);
			}
		}

		//List<Hunt> hunters = new List<Hunt>();
		

		
	}
}
