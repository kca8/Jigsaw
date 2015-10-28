using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float maxHealth = 100f;
	public float currentHealth = 100f; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth > maxHealth) currentHealth = maxHealth;
		if (currentHealth <= 0){
			Destroy(this.gameObject);
		}
	}

	public void AlterHealth(float change){
		currentHealth += change;
	}

	public float getHealthValue(){
		return currentHealth;
	}
}
