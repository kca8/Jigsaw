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
		if (currentHealth > maxHealth) currentHealth = maxHealth;		//currentHealth may not excede maxHealth
		if (currentHealth <= 0){
			Destroy(this.gameObject);									//when health reaches zero, object dies
		}
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///public method to alter health of object
	///Should be called whenever object should take damage or regain health
	///</summary>
	///
	// -------------------------------------------------------------------------
	public void AlterHealth(float change){
		currentHealth += change;
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///public getter method for health of object
	///</summary>
	///
	// -------------------------------------------------------------------------
	public float getHealthValue(){
		return currentHealth;
	}
}
