using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallScript : MonoBehaviour {

	public List<GameObject> goList = new List<GameObject>();
	string nameToAdd = "Wall";
	public GameObject Mypos;
	public int x = 5;
	public Vector3 bar;
	void Start()
	{
		foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
		{
			if(go.name == nameToAdd){
				//print (go.name);
				goList.Add (go);
				Vector3 temp = go.transform.localPosition;
		//		print (temp);
			}


		}
	//	foreach (GameObject wall in goList) {
		//	print ("second through");
		//	print(wall.transform.localPosition);

		//}

	}
}
