using UnityEngine;
using System.Collections;

public class BoxPuzzleActivator : MonoBehaviour {
	public boxpuzzlegui gui;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(camera.enabled == true)
		{
			gui.enabled = true;
		}
		else
		{
			gui.enabled = false;
		}
	
	}
}
