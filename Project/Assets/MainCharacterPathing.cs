﻿using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
public class MainCharacterPathing : MonoBehaviour {
    //The point to move to
	public AudioClip Walk;
    public Vector3 targetPosition;
	public Vector3 targetPosition2;
	public bool canmove;
    private Seeker seeker;
    public CharacterController controller;
    public Vector3 dir;
	public Vector3 LastPos;
    //The calculated path
    public Path path;
    public Vector3 LastPath;
    //The AI's speed per second
    public float speed = 20.0f;
	bool okay;
	float time;
	public float yheight;
	public Camera cam;
	int f;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 1;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
 
	void Awake()
	{
		audio.volume = 0.3f;
		audio.clip = Walk;
	    
		targetPosition = transform.position;
		targetPosition2 = transform.position;
		animation["Take 001"].speed = 3.2f;
		GlobalVars.player_transform = gameObject.transform;
		//this.enabled = false;
		canmove = true;
		bool okay = true;
		time = Time.time;
		f=30;
		LastPath.Set(0,0,0);
		
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
						
	}
	
	//Gets mouse click location and builds a a path to that location
	
	public bool CheckIfPathIsPossible(Vector3 PathStart,Vector3 PathEnd){
		
		Pathfinding.Node node1 = AstarPath.active.GetNearest(PathStart, NNConstraint.Default).node;
		Pathfinding.Node node2 = AstarPath.active.GetNearest(PathEnd, NNConstraint.Default).node;
		
		if (!Pathfinding.PathUtilities.IsPathPossible (node1,node2)) {	
			
			return(false);
		}
		else{
			
			return(true);
		}
	}
	
		
	void Update()
	{
		
		//transform.position.y = yheight;
		if(transform.position.y != yheight)
		{
		Vector3 temp = gameObject.transform.position;
		 
		temp.y = yheight;
		gameObject.transform.position = temp;
		}
		if(Time.time - time > 0.5)
		if(canmove)
		if(Input.GetMouseButton(1))
        {
			time = Time.time;
			//AstarPath.active.Scan();
		//Debug.Log("Clicking");
			
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		    RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit, 1000))
			{
				//Debug.Log(hit.point);	--hamish
				if((hit.point != LastPath))//&&(hit.point.y==100))
				{
							
				if(CheckIfPathIsPossible(controller.transform.position,hit.point))
					{
							
						if(true)//okay
						{
						//Debug.Log("this far");	--hamish
				okay = false;
				
				
				LastPath = targetPosition;
				targetPosition = hit.point;
				//targetPosition.y = yheight;//1.08f;
				if(targetPosition != transform.position)
								{
									Scanner();
								}
								else
								{
									okay = true;
								}
									
							//Debug.Log("Hello"); --hamish
						}
					}
				}
				
			}

			
        }
	}
	
	
    public void Start ()
	{
	        
    }
	
	public void Scanner()
	{		
		LastPos = gameObject.transform.position;
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}
    
    public void OnPathComplete (Path p) 
	{
        //Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error); --hamish
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
			targetPosition2 = targetPosition;
			okay = true;
			
			LastPos = transform.position;
        }
		else
		{
			path = null;
			okay = true;
			targetPosition = transform.position;
			targetPosition2= transform.position;
		}
    }
 
    public void FixedUpdate () 
	{
		
        if (path == null) {
            //We have no path to move after yet
            return;
        }
		//transform.position = new Vector3(transform.position.x,yheight,transform.position.z);
        
		//This function makes the object move the final distance to the target postion
        if (currentWaypoint >= path.vectorPath.Count) {
            //Debug.Log ("End Of Path Reached");
			if((Vector3.Distance(transform.position,targetPosition)>0.1f)&&(path != null))
			{
				
				//targetPosition2 = path.vectorPath[path.vectorPath.Count];
			dir = (targetPosition-transform.position).normalized;
        	dir *= speed * Time.fixedDeltaTime;
				//dir = new Vector3(dir.x,0,dir.z);
        	//controller.SimpleMove (dir);
			}
			else if((Vector3.Distance(transform.position,targetPosition)>0.01f)&&(path != null))
			{
				
				
				
				
				//transform.position = Vector3.Lerp(transform.position,targetPosition,0.1f);
				//dir = Vector3.zero;
			}
			else
			{
				transform.position = targetPosition;
			}
            return;
        }
        
        //Direction to the next waypoint
		
        dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
		
		dir = new Vector3(dir.x,0,dir.z);
		Quaternion rotation = Quaternion.LookRotation(dir);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime * 10);
		controller.SimpleMove (dir);
		//transform.Rotate(Vector3.up*270);
		
		if(!audio.isPlaying)
		{
			
		}
		if(!animation.IsPlaying("Take 001"))
		{
				animation.Play();
				if(Vector3.Distance(targetPosition,transform.position)>0.5)
				audio.Play();
		}
		if(!animation.IsPlaying("Take 001"))
		{
			audio.Stop();
		}
		
		if(Vector3.Distance(LastPos,transform.position)<0.001)
		{
			targetPosition = transform.position;
			targetPosition2 = transform.position;
			
			animation.Stop();
			audio.Stop();
		}
		
		
       
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
			LastPos = transform.position;
		//transform.position = new Vector3(transform.position.x,yheight,transform.position.z);
    }
}
