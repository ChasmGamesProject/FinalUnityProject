using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private Transform myTransform;
	
	private float MoveSpeed;
	
	private InteractMode im;
	
	void Awake()
	{
		myTransform = gameObject.transform;
		GlobalVars.player_transform = myTransform;
	}
	
	void Start ()
	{
		im = GlobalVars.interact_mode;
		
		MoveSpeed = 0.125f;
	}
	
	void Update ()
	{
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
		{
			if(Input.GetKey(KeyCode.UpArrow))
				myTransform.Translate(new Vector3(0, 0, MoveSpeed));
	
			if(Input.GetKey(KeyCode.DownArrow))
				myTransform.Translate(new Vector3(0, 0, -MoveSpeed));
			
			if(Input.GetKey(KeyCode.LeftArrow))
				myTransform.Translate(new Vector3(-MoveSpeed, 0, 0));
			if(Input.GetKey(KeyCode.RightArrow))
				myTransform.Translate(new Vector3(MoveSpeed, 0, 0));
		}
	}
}
