using UnityEngine;
using System.Collections;

public class avoid : MonoBehaviour {

	private Vector3 origin;
	public float sight = 5.0f;
	public float speed = 1.0f;
	public float tDeviation = 0;
	public string tagToAvoid;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 away = observeForFlee(origin, sight, tagToAvoid);

		//Did not observe anything to flee from
		if (away == Vector3.zero){
			gameObject.renderer.material.color = Color.red;
		} 
		//Found something to flee from and now will
		else {
			gameObject.renderer.material.color = Color.yellow;
			float randomValue = randT(tDeviation);
			//float chosenAngle = Mathf.LerpAngle(-45, 45, randT (tDeviation));
			//away = Quaternion.AngleAxis (chosenAngle, Vector3.up) * away;
		  	//transform.forward = Vector3.RotateTowards(transform.forward, away, speed * Time.deltaTime, 0.0f);
			//transform.Translate(away * speed * Time.deltaTime);
		}
	}

	// -------------------------------------------------------------------------
	///<summary>
	///Looks at all objects within a sphere of a given radius, centered on object
	///Considers all objects designated with specified reference Name
	///Calculates and returns perfect vector to flee from specified objects
	///If no objects exist to be considered, it returns a zero vector.
	///</summary>
	/// 
	///<param name="center">>> position of this object</param>
	///<param name="radius">>> size of sphere </param>
	///<param name="refTag">>> string label of objects to avoid</param>
	///<returns> Vector3 representing best direction of travel to avoid enemies
	///or a vector of 0,0,0 if detecting none</returns>
	///
	// -------------------------------------------------------------------------
	public static Vector3 observeForFlee(Vector3 center, float radius, string refTag) {
		center.y = 0f;
		Vector3 detectTotal = center;
		Vector3 away;
		Collider[] seenObjects = Physics.OverlapSphere(center, radius);
		bool detected = false;

		for (int i = 0; i < seenObjects.Length; i++) {
			if(seenObjects[i].gameObject.transform.tag == refTag){
				Vector3 detectCurrent = (seenObjects[i].gameObject.transform.position + center);
				detectCurrent.y = 0f;
				detectTotal = detectTotal + detectCurrent;
				detected = true;
			}
		}
		if (detected) {
			return new Vector3(detectTotal.x * -1, 0, detectTotal.z * -1).normalized;
		}
		else {
			 return Vector3.zero;
		}
	}

	// -------------------------------------------------------------------------
	///<summary>
	///Generates a random gaussian value between 0 and 1 with a mean of 0.5
	///based on a supplied standard deviation.
	///
	///stdDev = 0 will always return 0.5; higher values produces higher variance
	///</summary>
	/// 
	///<param name="stdDev">>> desired stdDev</param>
	///<returns> a random value for "t" to be used in LerpAngle between 0 and 1 
	///inclusively </returns>
	///
	// -------------------------------------------------------------------------
	public static float randT (float stdDev){
		//perfect value of t
		float mean = 0.5f;

		//these are uniform(0,1) random doubles
		float u1 = Random.value;
		float u2 = Random.value;

		//random normal(0,1)
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); 

		//random normal(mean,stdDev^2)
		float randNormal = mean + stdDev * randStdNormal; 
		Debug.Log (randNormal);
		//TODO: normalize values to fall between 0 and 1 inclusively
		//return normalize(randNormal);
		return randNormal;
	}
}