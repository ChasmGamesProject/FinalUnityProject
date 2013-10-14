using UnityEngine;
using System.Collections;

public class PinLogic : MonoBehaviour {
	public bool unlocked;
	public Vector3 UpwardsPush;
	public Vector3 DefaultPos;
	public float HeightMax;
	public float HeightMin;

	// Use this for initialization
	void Start () {
		unlocked = false;
		UpwardsPush = new Vector3(0,1.5f,0);
		HeightMax = 100;
		HeightMin = 98;
		DefaultPos.x =transform.position.x;
		DefaultPos.y =transform.position.y;
		DefaultPos.z =transform.position.z;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if((transform.position.y > HeightMin)&&(transform.position.y < HeightMax))
		{
		rigidbody.velocity += UpwardsPush * Time.deltaTime;
		}
		if(transform.position.y > HeightMax)
		{
			rigidbody.velocity = Vector3.zero;
			//transform.position = DefaultPos;
		}
		if(transform.position.y <= HeightMin)
		{
			unlocked = true;
		}
		else
		{
			unlocked = false;
		}
	
	}
}
