using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour {

	private string[] hostileTags;
	
	public float damage = -10;
	public bool penetration = false;
	public Vector3 origin;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider otherObject){
		print (otherObject);
		if(hostileTags.Contains(otherObject.gameObject.tag)){
			Health targetHealth = otherObject.gameObject.GetComponent<Health>();
			if (targetHealth != null) targetHealth.AlterHealth(damage);
		}
		//destroy the bullet on first collision if bullet penetration is off
		if(!penetration){
			Destroy (this.gameObject);
		}
	}

	public void setTags(string[] assignedTags){
		hostileTags = assignedTags;
	}
}
