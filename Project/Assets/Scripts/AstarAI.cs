using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
public class AstarAI : MonoBehaviour {
    //The point to move to
    public Vector3 targetPosition;
	public Vector3 targetPosition2;
	public bool canmove;
    public Camera BoxC;
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
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 1;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
 
	void Awake()
	{canmove = true;
		bool okay = true;
		time = Time.time;
		//GlobalVars.player_transform = gameObject.transform;
		
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
		if(Time.time - time > 1)
		if(canmove)
		if(Input.GetMouseButton(0))
        {
			time = Time.time;
			//AstarPath.active.Scan();
		//Debug.Log("Clicking");
			
			Ray ray = BoxC.ScreenPointToRay (Input.mousePosition);
		    RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit, 100))
			{
				if((hit.point != LastPath)&&(hit.point.y==100))
				{
				if(CheckIfPathIsPossible(controller.transform.position,hit.point))
					{
						if(okay)
						{
				okay = false;
				targetPosition = hit.point;
				LastPath = targetPosition;
				targetPosition.y = yheight;//1.08f;
				Scanner();
							Debug.Log("Hello");
						}
					}
				}
				
			}

			
        }
	}
	
	
    public void Start () {
		
		
		LastPath.Set(0,0,0);
		
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
		//LastPos = gameObject.transform.position;
		//seeker.StartPath (transform.position,targetPosition, OnPathComplete);
        
		
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        
    }
	
	public void Scanner()
	{
		
		LastPos = gameObject.transform.position;
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
			targetPosition2 = targetPosition;
			okay = true;
        }
		else
		{
			path = null;
			okay = true;
			targetPosition = transform.position;
			targetPosition2= transform.position;
		}
    }
 
    public void FixedUpdate () {
		
        if (path == null) {
            //We have no path to move after yet
            return;
        }
        
		//This function makes the object move the final distance to the target postion
        if (currentWaypoint >= path.vectorPath.Count) {
            //Debug.Log ("End Of Path Reached");
			if((Vector3.Distance(transform.position,targetPosition2)>0.3f))
			{
				//targetPosition2 = path.vectorPath[path.vectorPath.Count];
			dir = (targetPosition2-transform.position).normalized;
        	dir *= speed * Time.fixedDeltaTime;
        	controller.SimpleMove (dir);
			}
			else
			{
				
				//dir = Vector3.zero;
			}
            return;
        }
        
        //Direction to the next waypoint
        dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
		/*if((Vector3.Distance(transform.position,targetPosition)<1.0f))
		{
			Debug.Log("WTF");
        transform.position = targetPosition;
		}
		else	*/
		controller.SimpleMove (dir);
       
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
			LastPos = transform.position;
		transform.position = new Vector3(transform.position.x,yheight,transform.position.z);
    }
}