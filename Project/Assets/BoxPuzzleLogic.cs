using UnityEngine;
using System.Collections;

public class BoxPuzzleLogic : MonoBehaviour {
	
	Color initialColor;
	public Transform Player;
	public AstarAI AI;
	 private Vector3 target;
	public BoxManager Boxmanage;
	private float x;
	private float z;
	int i;
	public bool moving;
	string caseSwitch;
		// Use this for initialization
	void Start () {
		target = Vector3.zero;
		AI.targetPosition2 = new Vector3(146.0f,101.08f,116.0f);
		AI.targetPosition = new Vector3(146.0f,101.08f,116.0f);
		AI.Scanner();
	moving = false;
	initialColor = renderer.material.color;
		caseSwitch = "something";
	}
	
void SetXZ()
	{
		if(transform.localPosition.x>0)
			{
			x = transform.localPosition.x/2;
			}
			else{
				x = transform.localPosition.x;
			}
			if(transform.localPosition.z>0)
			{
			z = transform.localPosition.z/2;
			}
			else{
				z = transform.localPosition.z;
			}
	}
	

void OnMouseOver()
{
		renderer.material.color = Color.green;
		if(AI.canmove)
		{
		if(Input.GetMouseButtonDown(0))
		{
			AstarPath.active.Scan();
			SetXZ();
			Boxmanage.CheckFloor();
			
			if(Vector2.Distance(new Vector2(Player.position.x,Player.position.z),new Vector2(transform.position.x,transform.position.z))<=2)
			{
				Debug.Log("Clicking an object");
				//Player is left
				if((Player.position.x < transform.position.x)&&(Player.position.z >transform.position.z-0.5f)&&(Player.position.z <transform.position.z+0.5f))
				{
					renderer.material.color = Color.red;
					if(x<4)
					{
						if(Boxmanage.Floor[(int)x+1,(int)z] == "NotTaken")
						{
							caseSwitch = "right";
							//transform.localPosition = new Vector3(x*2+2,0,transform.localPosition.z);
							renderer.material.color = Color.green;
							//AstarPath.active.Scan();
						}
						
					}
					Debug.Log ("Player is left");
				}
				//Player is right
				else if((Player.position.x > transform.position.x)&&(Player.position.z >transform.position.z-0.5f)&&(Player.position.z <transform.position.z+0.5f))
				{
					renderer.material.color = Color.red;
					if(x>0)
					{
						if(Boxmanage.Floor[(int)x-1,(int)z] == "NotTaken")
						{
							caseSwitch = "left";
								//transform.localPosition = new Vector3(x*2-2,0,transform.localPosition.z);
							renderer.material.color = Color.green;
						}
					}
					
					Debug.Log ("Player is right");
				}
				//Player is Up
				else if((Player.position.x >transform.position.x-0.5f)&&(Player.position.x <transform.position.x+0.5f)&&(Player.position.z > transform.position.z))
				{
					renderer.material.color = Color.red;
					if(z>0)
					{
						if(Boxmanage.Floor[(int)x,(int)z-1] == "NotTaken")
						{
							caseSwitch = "down";	
							//transform.localPosition = new Vector3(transform.localPosition.x,0,z*2-2);
							renderer.material.color = Color.green;
						}
					}
					
					Debug.Log ("Player is Up");
				}
				//Player is Down
				else if((Player.position.z < transform.position.z-0.5)&&(Player.position.x >transform.position.x-0.5f)&&(Player.position.x <transform.position.x+0.5f))
				{
					renderer.material.color = Color.red;
					Debug.Log ("Player is Down");
					if(z<4)
					{
						Debug.Log(x);
						if(Boxmanage.Floor[(int)x,(int)z+1] == "NotTaken")
						{
							caseSwitch = "up";
								//transform.localPosition = new Vector3(transform.localPosition.x,0,z*2+2);
							renderer.material.color = Color.green;
							//AstarPath.active.Scan();
						}
					}
					
				}
				else
				{
					renderer.material.color = Color.red;
				}
			}
			else
				renderer.material.color = Color.red;
		}
		else if(Input.GetMouseButtonDown(1))
		{
				target = Vector3.zero;
			AstarPath.active.Scan();
			SetXZ();
			Boxmanage.CheckFloor();
			if(Vector2.Distance(new Vector2(Player.position.x,Player.position.z),new Vector2(transform.position.x,transform.position.z))<=2)
			{
				//Player is left
				if((Player.position.x < transform.position.x)&&(Player.position.z >transform.position.z-0.5f)&&(Player.position.z <transform.position.z+0.5f))
				{
					renderer.material.color = Color.red;
					if(x>1)
					{
						Debug.Log ("Player is left");
						if((Boxmanage.Floor[(int)x-1,(int)z] == "NotTaken")&&(Boxmanage.Floor[(int)x-2,(int)z] == "NotTaken"))
						{
							
							caseSwitch = "left";
							renderer.material.color = Color.green;
						}
					}
				}
				//Player is right
				else if((Player.position.x > transform.position.x)&&(Player.position.z >transform.position.z-0.5f)&&(Player.position.z <transform.position.z+0.5f))
				{
					renderer.material.color = Color.red;
					if(x<3)
					{
						if((Boxmanage.Floor[(int)x+1,(int)z] == "NotTaken")&&(Boxmanage.Floor[(int)x+2,(int)z] == "NotTaken"))
						{
							caseSwitch = "right";
							renderer.material.color = Color.green;
							//AstarPath.active.Scan();
						}
						
					}
					Debug.Log ("Player is right");
				}
				//Player is Up
				else if((Player.position.x >transform.position.x-0.5f)&&(Player.position.x <transform.position.x+0.5f)&&(Player.position.z > transform.position.z))
				{
					renderer.material.color = Color.red;
					if(z<3)
					{
						Debug.Log(x);
						if((Boxmanage.Floor[(int)x,(int)z+1] == "NotTaken")&&(Boxmanage.Floor[(int)x,(int)z+2] == "NotTaken"))
						{
							caseSwitch = "up";
							renderer.material.color = Color.green;
							//AstarPath.active.Scan();
						}
					}
					Debug.Log ("Player is Up");
				}
				//Player is Down
				else if((Player.position.z < transform.position.z-0.5)&&(Player.position.x >transform.position.x-0.5f)&&(Player.position.x <transform.position.x+0.5f))
				{
					renderer.material.color = Color.red;
					if(z>1)
					{
						if((Boxmanage.Floor[(int)x,(int)z-1] == "NotTaken")&&(Boxmanage.Floor[(int)x,(int)z-2] == "NotTaken"))
						{
							caseSwitch = "down";
							renderer.material.color = Color.green;
						}
					}
					Debug.Log ("Player is Down");
				}
			}
			
		}
		}
		
   		
}
void OnMouseExit()
{
   renderer.material.color = initialColor;
}
	
	// Update is called once per frame
	void Update () {
		Boxmanage.temp1 =rigidbody.position;
		if((Vector3.Distance(Boxmanage.temp1, Boxmanage.temp2)<0.01)&&(AI.canmove==false))
		{
			AstarPath.active.Scan();
			Debug.Log(rigidbody.position);
			Debug.Log(target);
			Debug.Log(Vector3.Distance(rigidbody.position, target));
			//AI.targetPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			rigidbody.velocity = Vector3.zero;
			rigidbody.position =target;
			AI.canmove = true;
			caseSwitch = "something";
		}
		
		
		switch(caseSwitch)
		{
		case "left":
		{
			float tempx = x;
			if(tempx>0)
				tempx*=2;
			//AI.controller.SimpleMove(new Vector3(-Mathf.Lerp(x*2,x*2-2,0.1f),0,0));
			target = new Vector3(rigidbody.position.x-2,rigidbody.position.y,rigidbody.position.z);
			
			if(Player.position.x<transform.position.x)
			{
				AI.targetPosition2 = new Vector3((146+transform.localPosition.x)-4,101.08f,transform.position.z);
				AI.targetPosition = new Vector3((146+transform.localPosition.x)-4,101.08f,transform.position.z);
			}
			else{
				AI.targetPosition2 = new Vector3((146+transform.localPosition.x),101.08f,transform.position.z);
				AI.targetPosition = new Vector3((146+transform.localPosition.x),101.08f,transform.position.z);
			}
			rigidbody.velocity = new Vector3(-0.9f,0,0);
			AI.canmove = false;
			caseSwitch = "something";
					//controller.SimpleMove(new Vector3(-Mathf.Lerp(x*2,x*2-2,0.1f),0,0));
			Boxmanage.temp1 = rigidbody.position;
			Boxmanage.temp2 = target;
			
			break;
		}
		case "right":
		{
			float tempx = x;
			if(tempx>0)
				tempx*=2;
			target = new Vector3(rigidbody.position.x+2,rigidbody.position.y,rigidbody.position.z);
			if(Player.position.x > transform.position.x)
			{
				AI.targetPosition2 = new Vector3((146+transform.localPosition.x)+4,101.08f,transform.position.z);
				AI.targetPosition = new Vector3((146+transform.localPosition.x)+4,101.08f,transform.position.z);
			}
			else{
				AI.targetPosition2 = new Vector3((146+transform.localPosition.x),101.08f,transform.position.z);
				AI.targetPosition = new Vector3((146+transform.localPosition.x),101.08f,transform.position.z);
			}
			rigidbody.velocity = new Vector3(0.9f,0,0);
			Debug.Log(rigidbody.position);
			Debug.Log(target);
			Debug.Log(Vector3.Distance(rigidbody.position, target));
			Boxmanage.temp1 = rigidbody.position;
			Boxmanage.temp2 = target;
			AI.canmove = false;
			caseSwitch = "something";
			break;
		}
		case "up":
		{
			target = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z+2);
			if(Player.position.z>transform.position.z)
			{
				AI.targetPosition2 = new Vector3(transform.position.x,101.08f,(116+z*2)+4);
				AI.targetPosition = new Vector3(transform.position.x,101.08f,(116+z*2)+4);
			}
			else{
				AI.targetPosition2 = new Vector3(transform.position.x,101.08f,(116+z*2));
				AI.targetPosition = new Vector3(transform.position.x,101.08f,(116+z*2));
			}
			rigidbody.velocity = new Vector3(0,0,0.9f);
			AI.canmove = false;
			caseSwitch = "something";
			Boxmanage.temp1 = rigidbody.position;
			Boxmanage.temp2 = target;
			break;
		}
		case "down":
		{
			target = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z-2);
			if(Player.position.z < transform.position.z)
			{
				AI.targetPosition2 = new Vector3(transform.position.x,101.08f,(116+z*2)-4);
				AI.targetPosition = new Vector3(transform.position.x,101.08f,(116+z*2)-4);
			}
			else{
				AI.targetPosition2 = new Vector3(transform.position.x,101.08f,(116+z*2));
				AI.targetPosition = new Vector3(transform.position.x,101.08f,(116+z*2));
				
			}
			rigidbody.velocity = new Vector3(0,0,-0.9f);
			AI.canmove = false;
			caseSwitch = "something";
			Boxmanage.temp1 = rigidbody.position;
			Boxmanage.temp2 = target;
			break;
		}
		default:
		{
			
		}
			break;
		}
		
	
	}
}



 
