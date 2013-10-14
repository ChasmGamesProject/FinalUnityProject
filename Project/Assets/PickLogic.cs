using UnityEngine;
using System.Collections;

public class PickLogic : MonoBehaviour {
	public Vector3 angle; 
	Quaternion deltarotation;
	bool moving;
	float timer;
	int direction;
	// Use this for initialization
	void Start () {
		direction = 0;
		angle = new Vector3(0,-2,0);
		moving  = false;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		
		deltarotation = Quaternion.Euler(angle*Time.deltaTime);
		if(Input.GetMouseButtonDown(0))
		{
			direction = 0;
			timer = Time.time;
			moving = true;
			
		}
		else if(Input.GetMouseButtonDown(1))
		{
			direction = 1;
			timer = Time.time;
			moving = true;
		}
		
		if(Time.time - timer > 0.3)
		{
			timer = Time.time;
			moving = false;
		}
		if(moving)
		{
		//rigidbody.rotation =+ deltarotation;
			if(direction == 0)
			{
				angle = new Vector3(0,-20,0);
				deltarotation = Quaternion.Euler(angle*Time.deltaTime);		
				rigidbody.MoveRotation(rigidbody.rotation*deltarotation);
			}
			else if(direction == 1)
			{
				angle = new Vector3(0,20,0);
				deltarotation = Quaternion.Euler(angle*Time.deltaTime);		
				rigidbody.MoveRotation(rigidbody.rotation*deltarotation);
			}
		}
	deltarotation = Quaternion.Euler(angle*Time.deltaTime);
	}
}
