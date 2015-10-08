using UnityEngine;
using System.Collections;

public class avoid : MonoBehaviour {

	private Vector3 origin;
	public float sight = 5.0f;
	public float speed = 1.0f;
	public string tagToAvoid;
	public string dontRunFrom;
	public bool flee;

	// Use this for initialization
	void Start () {
		origin = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 away = observe (origin, sight, tagToAvoid);
		//Debug.Log(away.ToString("F3"));
		//transform.position = (Vector3.MoveTowards(origin, away, speed * Time.deltaTime));

		if (flee == true) {
			gameObject.renderer.material.color = Color.yellow;
		} else 
		{
			gameObject.renderer.material.color = Color.red;
		}
	}

	/**
	 * Looks at all objects within a sphere of a given radius, centered on object
	 * Considers all objects designated with specified reference Name
	 * Calculates and returns perfect vector to flee from specified objects
	 * 
	 * @param center - position of this object
	 * @param radius - size of sphere
	 * @param refTag - string label of objects to avoid
	 **/
	public Vector3 observe(Vector3 center, float radius, string refTag) {
		Vector3 detectTotal = center;
		Collider[] seenObjects = Physics.OverlapSphere(center, radius);
		for (int i = 0; i < seenObjects.Length; i++) {
			//It sees us!
			if(seenObjects[i].gameObject.transform.tag == refTag){
				Vector3 detectCurrent = (seenObjects[i].gameObject.transform.position + center);
				detectTotal = detectTotal + detectCurrent;
				flee = true;
				Debug.Log(seenObjects[i]);

			//	gameObject.renderer.material.color = Color.yellow;
			}

			else if (seenObjects[i].gameObject.transform.tag == dontRunFrom){
				flee = false;
				gameObject.renderer.material.color = Color.red;
			}

		}
		Vector3 away = new Vector3(detectTotal.x * -1, 0, detectTotal.z * -1).normalized;

		transform.Translate(away * Time.deltaTime);
		return away;
	}
}