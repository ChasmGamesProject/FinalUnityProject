using UnityEngine;
using System.Collections;

public class PickLogic : MonoBehaviour {
	
	bool LeapEnabled;
	public Vector3 angle; 
	Quaternion deltarotation;
	bool moving;
	float timer;
	int direction;
	Vector3 Left;
	Vector3 Right;
	Vector3 Up;
	Vector3 Down;
	public GUIscriptlockpicking gui;
	public float speed;
	public float offset;
	public float offsety;
	float ExtraSpeed;
	public Vector3 CenterOfMass;
	public Vector3 Mouse;
	public Camera MyCam;
	public float LeapHandPos;
	public float Leapx;
	public float Leapy;
	public bool LeapRotation;
	float LeapActionTimer;
	//Ray ray;
	// Use this for initialization
	
	
	void Start () {
		LeapEnabled = false;
		CenterOfMass = new Vector3(2, 0, 0);
		rigidbody.centerOfMass = CenterOfMass;
		ExtraSpeed = 1;
		offset = 0.8f;
		offsety = -0.0f;
		speed = 30;
		direction = 0;
		angle = new Vector3(0,-2,0);
		moving  = false;
		Left = new Vector3(-1,0,0);
		Right = new Vector3(1,0,0);
		Up = new Vector3(0,1,0);
		Down = new Vector3(0,-1,0);
		LeapActionTimer = Time.time;
		
	
	}
	
	void leaping()
	{
		gui.BarSize = 100;
		Leapx = pxsLeapInput.GetHandAxis("Horizontal");
		Leapy = pxsLeapInput.GetHandAxis("Sphere");
		LeapRotation = pxsLeapInput.GetHandGesture("Fire1");
		
		
	}
	// Update is called once per frame
	void Update () {
		
		//leaping ();
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		
		
		deltarotation = Quaternion.Euler(angle*Time.deltaTime);
		
		if(LeapEnabled)
		{
			leaping();
			if((LeapRotation)&&(Time.time - LeapActionTimer>1))
			{
				LeapActionTimer = Time.time;
				direction = 0;
				moving = true;
			}
		}
		else
		if(Input.GetMouseButtonDown(0))
		{
			direction = 0;
			timer = Time.time;
			moving = true;
			
		}
		else if(Input.GetMouseButtonDown(1))
		{
			//direction = 1;
			//timer = Time.time;
			//moving = true;
		}
		
		if(Time.time - timer > 0.3)
		{
			timer = Time.time;
			moving = false;
		}
		if(moving)
		{
		//rigidbody.rotation =+ deltarotation;
			
			
			//rigidbody.constraints = RigidbodyConstraints.None;
			//rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
			
			
			if(direction == 0)
			{
				rigidbody.angularVelocity = new Vector3(0,0,0.5f);
				//rigidbody.AddTorque(0,-1,0);
				angle = new Vector3(0,-20,0);
				deltarotation = Quaternion.Euler(angle*Time.deltaTime);		
				//rigidbody.MoveRotation(rigidbody.rotation*deltarotation);
			}
			else if(direction == 1)
			{
			 	//transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
				//rigidbody.angularVelocity = new Vector3(0,0,0.5f);
				//rigidbody.AddTorque(0,1,0);
				angle = new Vector3(0,20,0);
				
				deltarotation = Quaternion.Euler(angle*Time.deltaTime);		
				//rigidbody.MoveRotation(rigidbody.rotation*deltarotation);
			}
		}
		else if((transform.rotation.eulerAngles.x != 270))
		{
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
			rigidbody.angularVelocity = new Vector3(0,0,-0.1f);
		}
		else
		{
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
			rigidbody.angularVelocity = new Vector3(0,0,0);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(270,1.7f,0)),Time.deltaTime);
		}
	deltarotation = Quaternion.Euler(angle*Time.deltaTime);
		
		if((!Input.GetMouseButtonDown(1))||(!Input.GetMouseButtonDown(0)))
		{
		Ray ray = MyCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		
		
		
		if((Physics.Raycast(ray, out hit, 100))&&(!LeapEnabled))
		{
			Debug.Log(hit.point);
			Mouse = hit.point;
			if((hit.point.x < transform.position.x + offset)&&(transform.position.x + offset - hit.point.x > 0.01))
			{
				ExtraSpeed = transform.position.x+offset - hit.point.x - 0.01f;
				ExtraSpeed = ExtraSpeed;
				rigidbody.velocity = new Vector3 (Left.x*speed*Time.deltaTime*ExtraSpeed,rigidbody.velocity.y,0);
			}
			if((hit.point.x > transform.position.x+offset)&&(hit.point.x - (transform.position.x+offset)> 0.01))
			{
				ExtraSpeed = hit.point.x - (transform.position.x+offset) - 0.01f;
				ExtraSpeed = ExtraSpeed;
				rigidbody.velocity = new Vector3 (Right.x*speed*Time.deltaTime*ExtraSpeed,rigidbody.velocity.y,0);
			}
			if((hit.point.y < transform.position.y + offsety)&&(transform.position.y + offsety - hit.point.y > 0.01))
			{
				ExtraSpeed = transform.position.y+offsety - hit.point.y - 0.01f;
				ExtraSpeed = ExtraSpeed;
				rigidbody.velocity = new Vector3(rigidbody.velocity.x,Down.y*speed*Time.deltaTime*ExtraSpeed,0);
			}
			if((hit.point.y > transform.position.y + offsety)&&(hit.point.y - transform.position.y + offsety> 0.01))
			{
				ExtraSpeed = hit.point.y - transform.position.y+offsety - 0.01f;
				ExtraSpeed = ExtraSpeed;
				rigidbody.velocity = new Vector3(rigidbody.velocity.x,Up.y*speed*Time.deltaTime*ExtraSpeed,0);
			}
			
			
			
		}
			else if((LeapEnabled)&&(!moving)&&(!LeapRotation))
			{
				rigidbody.velocity = Vector3.zero;
				if(Leapx > 20.0f)
				{
					rigidbody.velocity = new Vector3(0.2f,rigidbody.velocity.y,0);
				}
					
				if(Leapx < -20.0f)
				{
					rigidbody.velocity = new Vector3(-0.2f,rigidbody.velocity.y,0);
				}
					
				if(Leapy > 160.0f)
				{
					rigidbody.velocity = new Vector3(rigidbody.velocity.x,0.1f,0);
				}
				if(Leapy < 110.0f)
				{
					rigidbody.velocity = new Vector3(rigidbody.velocity.x,-0.1f,0);
				}
				
				if((rigidbody.position.x>103)&&(rigidbody.velocity.x>0))
				{
					rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
				}
			}
	}
	}
}
