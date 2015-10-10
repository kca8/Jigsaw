using UnityEngine;
using System.Collections;

public class chase : MonoBehaviour {
	private Vector3 origin;
	private float customT = 0.5f;
	public float sight = 5.0f;
	public float velocity = 1.0f;
	public float tDeviation = 0;
	public float newTDelay = 0;
	public string tagToChase;
	public GameObject stalker;
	
	void Start() {
		InvokeRepeating("newT", 0, newTDelay);
	}
	
	// Update is called once per frame
	void Update () {
		origin = transform.position;
		Vector3 away = observeForChase(origin, sight, tagToChase);
		travel(stalker, away, velocity, customT);
	}
	
	void newT() {
		customT = randT (tDeviation);
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

	// -------------------------------------------------------------------------
	///@author Keegan Anderson
	///<summary>
	///Looks at all objects within a sphere of a given radius, centered on object
	///Considers all objects designated with specified reference Name
	///Calculates and returns perfect vector to chase nearest object of specified type
	///If no objects exist to be considered, it returns a zero vector.
	///</summary>
	/// 
	///<param name="center">>> position of this object</param>
	///<param name="radius">>> size of sphere </param>
	///<param name="refTag">>> string label of objects to chase</param>
	///<returns> Vector3 representing best direction of travel to chase nearest
	///target or a vector of 0,0,0 if detecting none</returns>
	///
	// -------------------------------------------------------------------------
	public static Vector3 observeForChase(Vector3 center, float radius, string refTag) {
		center.y = 0f;
		Vector3 detectTotal = Vector3.zero;	
		Vector3 fleeVector = Vector3.zero;
		bool detected = false;
		
		Collider[] seenObjects = Physics.OverlapSphere(center, radius); 
		for (int i = 0; i < seenObjects.Length; i++) {
			if(seenObjects[i].gameObject.transform.tag == refTag){ 
				detected = true;
				Vector3 detectCurrent = seenObjects[i].gameObject.transform.position - center; 
				detectCurrent.y = 0f; 
				detectTotal = detectCurrent + detectTotal;
			}
		}
		if (detected) {
			fleeVector = detectTotal;
			return fleeVector.normalized;
		}
		else {
			return Vector3.zero; 
		}
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
}
