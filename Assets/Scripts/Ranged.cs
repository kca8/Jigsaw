using UnityEngine;
using System.Collections;
using System.Linq;

public class Ranged : MonoBehaviour {

	private Vector3 origin;
	private float timeOfLastShot = 0f;
	
	public float sight = 15f;
	public float range = 15f;
	public float rotationSpeed = 4f;
	public float rateOfFire = 0.5f;
	public string[] tagsToTarget;
	public Rigidbody projectile;
	public GameObject ai;


	// Use this for initialization
	void Start () {
		origin = transform.position;
		if (range > sight) range = sight;
	}
	
	// Update is called once per frame
	void Update () {

		origin = transform.position;
		Vector3 directionOfTarget = observeForTarget(origin, sight, tagsToTarget);
		fire(directionOfTarget);
	
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///Looks at all objects within a sphere of a given radius, centered on object
	///Considers all objects designated with specified reference tags
	///Calculates and returns perfect vector to target nearest object of specified type
	///If no objects exist to be considered, it returns a zero vector.
	///</summary>
	/// 
	///<param name="center">>> position of this object</param>
	///<param name="radius">>> size of sphere </param>
	///<param name="refTag">>> string array of labels of objects to target</param>
	///<returns> Vector3 representing best trajectory to hit nearest
	///target or a vector of 0,0,0 if detecting none</returns>
	///
	// -------------------------------------------------------------------------

	public void GiveInfo(Collider seenObjects){
		AIDirector ai = gameObject.GetComponent<AIDirector>();
		if (seenObjects.gameObject.transform.CompareTag ("Player")) {
			Health targetHealth = seenObjects.gameObject.GetComponent<Health>();
			float health = targetHealth.getHealthValue ();
			ai.setPlayerHealth(health);
			print ("player spotted");
		}

	}

	public Vector3 observeForTarget(Vector3 center, float radius, string[] refTags) {
		center.y = 0f;
		Vector3 detectBest = Vector3.zero;	
		float distance = Mathf.Infinity;
		Vector3 targetVector = Vector3.zero;
		bool detected = false;
		
		Collider[] seenObjects = Physics.OverlapSphere(center, radius); 
		for (int i = 0; i < seenObjects.Length; i++) {
			if(refTags.Contains(seenObjects[i].gameObject.transform.tag)){ 
				detected = true;
				Vector3 detectCurrent = seenObjects[i].gameObject.transform.position - center; 
				detectCurrent.y = 0f; 
				if(distance > detectCurrent.sqrMagnitude){
					detectBest = detectCurrent;
					distance = detectCurrent.sqrMagnitude;
				}
			}

			Collider o = seenObjects[i];
			GiveInfo(o);

		}
		if (detected) {
			targetVector = detectBest;
			return targetVector.normalized;
		}
		else {
			return Vector3.zero; 
		}
	}

	void fire (Vector3 directionOfTarget) {
		if(directionOfTarget != Vector3.zero){

			transform.forward = Vector3.RotateTowards(transform.forward, directionOfTarget, rotationSpeed * Time.deltaTime, 0.0f);

			if(Time.time > rateOfFire + timeOfLastShot){
				Rigidbody tempProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
				Physics.IgnoreCollision(tempProjectile.collider, gameObject.collider);
				timeOfLastShot = Time.time;
			}
		}
	}
}
