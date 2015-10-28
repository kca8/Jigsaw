using UnityEngine;
using System.Collections;

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
		type = NPC_Type;
	}
}
