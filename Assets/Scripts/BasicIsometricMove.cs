using UnityEngine;
using System.Collections;

//code fragment appropriated from xlar9or via unity3d forums
public class BasicIsometricMove : MonoBehaviour
{
	//modular scalar
	public int velocity = 2;
	
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