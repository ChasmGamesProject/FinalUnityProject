﻿using UnityEngine;
using System.Collections;

public class PinLogic : MonoBehaviour {
	public bool unlocked;
	public Vector3 UpwardsPush;
	public Vector3 DefaultPos;
	public GUIscriptlockpicking guistatus;
	public float HeightMax;
	public float HeightMin;
	public AudioClip picksound;
	float timepassed;

	
	void Start () {
		unlocked = false;
		UpwardsPush = new Vector3(0,1.5f,0);
		DefaultPos.x =transform.position.x;
		DefaultPos.y =transform.position.y;
		DefaultPos.z =transform.position.z;
		
	
	}
	
	void OnCollisionEnter()
	{
		if(Time.time - timepassed > picksound.length)
		{
			timepassed = Time.time;
			AudioSource.PlayClipAtPoint(picksound,transform.position,0.2f);
		}
	}
	
	void Update () {
		
		unlocked = false;
		rigidbody.mass = 1;
		if((transform.position.y < HeightMax))
		{
			rigidbody.velocity += UpwardsPush * Time.deltaTime;
		}
		
		if(transform.position.y > HeightMax)
		{
			rigidbody.velocity = Vector3.zero;
			//transform.position = DefaultPos;
		}
		if((transform.position.y <= HeightMin)&&(guistatus.BarSize>10))
		{
			
			transform.position = new Vector3(transform.position.x,HeightMin,transform.position.z);
			rigidbody.velocity = Vector3.zero;
			rigidbody.mass = 1000;
			unlocked = true;
			if(guistatus.BarSize<10)
			{
				unlocked = false;
				rigidbody.mass = 1;
				transform.position = new Vector3(transform.position.x,HeightMin+0.01f,transform.position.z);
			}
		}
		else if((transform.position.y <= HeightMin)&&(guistatus.BarSize<10))
		{
			if(rigidbody.velocity.y < 0)
			{
				rigidbody.velocity = Vector3.zero;
			}
		}
		else
		{
			unlocked = false;
		}
		
		
	
	}
}
