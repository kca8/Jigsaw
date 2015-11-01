using UnityEngine;
using System.Collections;
using System.Linq;

public class Ranged : MonoBehaviour {

	private Vector3 origin;
	private GameObject myself;
	private float timeOfLastShot = 0f;
	private AIDirector ai;
	
	public float sight = 15f;
	public float range = 15f;
	public float rotationSpeed = 10f;
	public float reloadSpeed = 0.5f;
	public float projSpeed = 25f;
	public float timeToLive = 4f;
	public bool directed = false;
	public string[] tagsToTarget;
	public Rigidbody projectile;


	// Use this for initialization
	void Start () {
		ai = GameObject.FindObjectOfType<AIDirector>() as AIDirector;

		origin = transform.position;
		myself = gameObject;
		if (range > sight) range = sight;
	}
	
	// Update is called once per frame
	void Update () {
		origin = transform.position;
		Vector3 directionOfTarget = observeForTarget(origin, sight, tagsToTarget);

		float tempTime = fire (myself, directionOfTarget, projectile, reloadSpeed, timeOfLastShot, rotationSpeed, projSpeed, timeToLive, tagsToTarget);
		if (tempTime >= 0) timeOfLastShot = tempTime;
	}

	public void GiveInfo(Collider seenObject){
		if(directed){
			if (seenObject.gameObject.transform.CompareTag ("Player")) {
				Health targetHealth = seenObject.gameObject.GetComponent<Health>();
				float health = targetHealth.getHealthValue ();
				ai.setPlayerHealth(health);
				print ("player spotted");
			}
		}
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///Looks at all objects within a sphere of a given radius, centered on object
	///Considers all objects designated with specified reference tags
	///Calculates and returns perfect vector to target nearest object of specified type
	///If no objects exist to be considered, it returns a zero vector.
	/// 
	///GivesInfo of all objects encountered to AIDirector
	///</summary>
	/// 
	///<param name="center">>> position of this object</param>
	///<param name="radius">>> size of sphere </param>
	///<param name="refTag">>> string array of labels of objects to target</param>
	///<returns> Vector3 representing best trajectory to hit nearest
	///target or a vector of 0,0,0 if detecting none</returns>
	///
	// -------------------------------------------------------------------------
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

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	/// If supplied vector is nonzero, the GameObject "self" orients itself in the 
	/// direction of the target at "rotSpeed". If the time passed since "lastShot" 
	/// is greater than the "reload" delay, it will fire one "bullet" at 
	/// "shotSpeed" with a time to live of "ttl" and set its tags to damage to 
	/// "hostileTags". It then returns the current time to keep track of the time
	/// since last shot.
	/// 
	/// If supplied vector is zero, or not enough time has expired, returns a 
	/// sentinel value of -1
	///</summary>
	/// 
	///<param name="self">>> GameObject to fire from and rotate</param>
	///<param name="directionOfTarget">>> Vector3 orientation for projectile</param>
	///<param name="bullet">>> Rigidbody of projectile to fire; should have a 
	/// 	bullet.cs component</param>
	///<param name="reload">>> delay between shots</param>
	///<param name="lastShot">>> Time.time at last shot</param>
	///<param name="rotSpeed">>> Speed at which object should rotate</param>
	///<param name="shotSpeed">>> Speed at which bullet should travel</param>
	///<param name="ttl">>> Time to live for bullet before self destroying</param> 
	///<param name="hostileTags">>> array of strings to damage if collided with </param>
	///<returns> Time.time if a shot if fired, -1 if shot isn't</returns>
	///
	// -------------------------------------------------------------------------
	public static float fire (GameObject self, Vector3 directionOfTarget, Rigidbody bullet, float reload, float lastShot, float rotSpeed, float shotSpeed, float ttl, string[] hostileTags) {
		if(directionOfTarget != Vector3.zero){

			self.transform.forward = Vector3.RotateTowards(self.transform.forward, directionOfTarget, rotSpeed * Time.deltaTime, 0.0f);

			if(Time.time > reload + lastShot){
				Rigidbody tempProjectile = Instantiate(bullet, self.transform.position, self.transform.rotation) as Rigidbody;
				Physics.IgnoreCollision(tempProjectile.collider, self.gameObject.collider);

				tempProjectile.AddForce(tempProjectile.transform.forward * shotSpeed, ForceMode.Impulse);
				tempProjectile.gameObject.GetComponent<Bullet>().setTags(hostileTags);
				Destroy (tempProjectile.gameObject, ttl);

				return Time.time;
			}
			
		}
		return -1;
	}
}
