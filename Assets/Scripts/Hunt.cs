using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Hunt : MonoBehaviour {

	private Vector3 origin;
	private float customT = 0.5f;
	public float sight = 5.0f;
	public float velocity = 1.0f;
	public float tDeviation = 0;
	public float newTDelay = 0;
	public List<string> tagsToHunt = new List<string>();
	public GameObject stalker;
	
	public static GameObject director;
	
	void Start() {
		InvokeRepeating("newT", 0, newTDelay);
		director = GameObject.Find("AIDirector");
	}
	
	// Update is called once per frame
	void Update () {
		origin = transform.position;
		Vector3 away = observeForHunt(origin, sight, tagsToHunt);
		travel(stalker, away, velocity, customT);
	}
	
	void newT() {
		customT = randT (tDeviation);
	}

	public static void GiveInfo(Collider seenObject, bool detected){
		if(detected){
			if (seenObject.gameObject.transform.CompareTag ("Player")) {
				Health targetHealth = seenObject.gameObject.GetComponent<Health>();
				float health = targetHealth.getHealthValue ();
				director.gameObject.GetComponent<AIDirector>().setPlayerHealth(health);
				director.gameObject.GetComponent<AIDirector>().CalculateStrat ();
				print ("player spotted");
			}
		}
	}
	
	public static void travel(GameObject traveler, Vector3 path, float speed, float tDev)
	{
		//Did not observe anything to respond to
		if (path == Vector3.zero){
			traveler.renderer.material.color = Color.blue;
		} 
		//Found something to chase and now will
		else {
			traveler.renderer.material.color = Color.magenta;
			float chosenAngle = Mathf.LerpAngle(-45, 45, randT (tDev));
			path = Quaternion.AngleAxis (chosenAngle, Vector3.up) * path;
			
			//traveler.transform.forward = Vector3.RotateTowards(traveler.transform.forward, path, speed * Time.deltaTime, 0.0f);
			traveler.transform.Translate(path * speed * Time.deltaTime);
		}
	}
	
	public static Vector3 observeForHunt(Vector3 center, float radius, List<string> refTags) {
		center.y = 0f;
		Vector3 detectBest = Vector3.zero;	
		float distance = Mathf.Infinity;
		Vector3 chaseVector = Vector3.zero;
		bool detected = false;
		
		Collider[] seenObjects = Physics.OverlapSphere(center, radius);
		for (int i = 0; i < seenObjects.Length; i++) {
			if(refTags.Contains(seenObjects[i].gameObject.transform.tag)){
				detected = true;
				GiveInfo(seenObjects[i],true);
				Vector3 detectCurrent = seenObjects[i].gameObject.transform.position - center; 
				detectCurrent.y = 0f; 
				if(distance > detectCurrent.sqrMagnitude){
					detectBest = detectCurrent;
					distance = detectCurrent.sqrMagnitude;
				}
			}
			
			else if (seenObjects[i].gameObject.transform.tag != "Untagged" && seenObjects[i].gameObject.transform.tag != "Hunter"){
				detected = true;
				GiveInfo(seenObjects[i],true);
				//string newTarget = seenObjects[i].gameObject.transform.tag;
				tellDirector(seenObjects[i].gameObject.transform.tag); 
			}
		}
		if (detected) {
			chaseVector = detectBest;
			return chaseVector.normalized;
		}
		else {
			return Vector3.zero; 
		}
	}
	
	static void tellDirector(string newTarget){
		print ("In TellDirector");
		director.gameObject.GetComponent<AIDirector>().setTargetForNPCs(newTarget);

	}
	// -------------------------------------------------------------------------
	///@author Keegan Anderson
	///<summary>
	///Generates a random gaussian value between 0 and 1 with a mean of 0.5
	///based on a supplied standard deviation.
	///
	///stdDev = 0 will always return 0.5; higher values produces higher variance
	///</summary>
	/// 
	///<param name="stdDev">>> desired stdDev between 0 and 0.18 inclusive</param>
	///<returns> a random value for "t" to be used in LerpAngle between 0 and 1 
	///inclusively </returns>
	///
	// -------------------------------------------------------------------------
	public static float randT (float stdDev){
		//Limit acceptable values for stdDev
		if(stdDev > .18f) stdDev = 0.18f;
		else if (stdDev < 0.0f) stdDev = 0.0f;
		
		//perfect value of t
		float mean = 0.5f;
		
		//these are uniform(0,1) random doubles
		float u1 = Random.value;
		float u2 = Random.value;
		
		//random normal(0,1)
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); 
		
		//random normal(mean,stdDev^2)
		float randNormal = mean + stdDev * randStdNormal; 
		
		//TODO: normalize values to fall between 0 and 1 inclusively
		//return normalize(randNormal);
		return randNormal;
	}
	
	public void addHuntTarget(string newTarget){
	print("In addHuntTarget");
		tagsToHunt.Add(newTarget);
	}
}
