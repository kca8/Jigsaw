using UnityEngine;
using System.Collections.Generic;

//code fragment appropriated from xlar9or via unity3d forums
public class BasicIsometricMove : MonoBehaviour
{
	//modular scalar
	public int velocity = 2;
	public WallScript ws; 
	public GameObject target;
	private GameObject player;

	void Start()
	{
		ws = target.GetComponent<WallScript>();
		player = GameObject.Find ("Player");
		print ("Use the 'WASD' keys to move the camera/player\n");
		
	}
	//public WallScript ws;
	//public GameObject target;
	//private avoid a;

	//called every frame
	void Update()
	{
		//uses assigned velocity
		int speed = velocity;
		//zeroes out a dummy Vector3
		Vector3 amount = Vector3.zero;


		//if sprinting, double speed
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) speed *= 2;
		
		if (Input.GetKey(KeyCode.W))
		{
			foreach(GameObject wall in ws.goList)
			{
				if(wall.transform.localPosition.x == player.transform.position.x)
				{
					print (wall.renderer.bounds.Contains(player.transform.position));
					print ("worked");
				}
				else{

			
					//print (player.transform.position.x);
				}
				
			}
			amount.x += speed;
			amount.z += speed;
			

		}
		
		if (Input.GetKey(KeyCode.S))
		{
			amount.x += -speed;
			amount.z += -speed;
		}

		if (Input.GetKey(KeyCode.A))
		{
			amount.x += -speed;
			amount.z += speed;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			amount.x += speed;
			amount.z += -speed;
		}



		transform.position = Vector3.Lerp(transform.position, transform.position + amount, 0.5f * Time.deltaTime);

	}
}