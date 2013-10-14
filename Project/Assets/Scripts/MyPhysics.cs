using UnityEngine;
using System.Collections;

public class MyPhysics : MonoBehaviour {
	
	Vector3 gravity;
	Vector3 MoveX;
	float timer;
	bool setter = false;
	// Use this for initialization
	void Start () {
		timer = Time.time;
		gravity.x = 0.0f;
		gravity.y = 0.0f;
		gravity.z = -9.0f;//-9.8f;
		MoveX.x = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - timer > 2)
		{
		if(setter)
			{
		setter = false;
		rigidbody.velocity = Vector3.zero;
			}
		else
			{
		setter = true;
		rigidbody.velocity = Vector3.zero;
			}
			
		timer = Time.time;
		}
		
		if(setter)
		{
		rigidbody.velocity += gravity * Time.deltaTime;
		rigidbody.velocity += MoveX * Time.deltaTime;
		}
		else
		{
		rigidbody.velocity -= gravity * Time.deltaTime;
		}
	}
}

