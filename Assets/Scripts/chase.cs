using UnityEngine;
using System.Collections;
using System.Linq;

public class chase : MonoBehaviour {

	private Vector3 origin;
	private GameObject myself;
	private float customT = 0.5f;

	public float sight = 5.0f;
	public float velocity = 1.0f;
	public float tDeviation = 0;
	public float newTDelay = 0;
	public string[] tagsToChase;

	// Use this for initialization
	void Start() {
		InvokeRepeating("newT", 0, newTDelay);						//initializes a random 't' value regenerator
		myself = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		origin = transform.position;								//sets origin as current position
		Vector3 towards = observeForChase(origin, sight, tagsToChase);	//gets flee vector 
		travel(myself, towards, velocity, customT);					//travels in best direction
	}
	
	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///Private utility function to calculate new "t" value randomly; used in 
	///conjunction with InvokeRepeating.
	///</summary>
	///
	// -------------------------------------------------------------------------
	void newT() {
		customT = randT (tDeviation);
	}

	// -------------------------------------------------------------------------
	///@Keegan, Kirby, James
	///@author Keegan Anderson
	///<summary>
	///Moves a gameObject "traveler" along a specified "path" vector at a specific
	///"speed" and linearly interpolates its direction according to an already 
	///calculated "t" value
	///If sentinel value vector is passed for "path", no traveling will occur.
	///Color of object changes on context in current version.
	///</summary>
	/// 
	///<param name="traveler">>> the game object to be moved</param>
	///<param name="path">>> optimal, normalized vector3</param>
	///<param name="speed">>> float determining speed of travel</param>
	///<param name="t">>> float value for random direction adjustment</param> 
	///
	// -------------------------------------------------------------------------
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

			traveler.transform.Translate(path * speed * Time.deltaTime);
		}
	}

	// -------------------------------------------------------------------------
	///@author Keegan Anderson
	///<summary>
	///Looks at all objects within a sphere of a given radius, centered on object
	///Considers all objects designated with specified reference tags
	///Calculates and returns perfect vector to chase nearest object of specified type
	///If no objects exist to be considered, it returns a zero vector.
	///</summary>
	/// 
	///<param name="center">>> position of this object</param>
	///<param name="radius">>> size of sphere </param>
	///<param name="refTag">>> string array of labels of objects to chase</param>
	///<returns> Vector3 representing best direction of travel to chase nearest
	///target or a vector of 0,0,0 if detecting none</returns>
	///
	// -------------------------------------------------------------------------
	public static Vector3 observeForChase(Vector3 center, float radius, string[] refTags) {
		center.y = 0f;
		Vector3 detectBest = Vector3.zero;	
		float distance = Mathf.Infinity;
		Vector3 chaseVector = Vector3.zero;
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
		}
		if (detected) {
			chaseVector = detectBest;
			return chaseVector.normalized;
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
