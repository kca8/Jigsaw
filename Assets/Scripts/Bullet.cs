using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 1f;
	public float timeToLive = 5f;
	public float damage = -10;
	public bool penetration = false;
	public string[] hostileTags;
	public Vector3 origin;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, timeToLive);
	}
	
	// Update is called once per frame
	void Update () {
		float amountToMove = (bulletSpeed * Time.deltaTime);
		transform.Translate (Vector3.forward * amountToMove);
	}

	void OnTriggerEnter(Collider otherObject){
		if(hostileTags.Contains(otherObject.gameObject.tag)){
			Health targetHealth = otherObject.gameObject.GetComponent<Health>();
			targetHealth.AlterHealth(damage);
		}

		//destroy the bullet on first collision if bullet penetration is off
		if(!penetration){
			Destroy (this.gameObject);
		}
	}
}
