using UnityEngine;
using System.Collections;

public class Unlocker : MonoBehaviour {
	public WorldDoor door;
	
	// Use this for initialization
	void Start () {
		
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.U))
		{
			door.Unlock();
		}
	
	}
}
