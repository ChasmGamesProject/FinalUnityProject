using UnityEngine;
using System.Collections;

public class BoxPuzzleActivator : MonoBehaviour {
	public boxpuzzlegui gui;
	public Transform player;
	public Transform MainPlayer;
	int toggle;
	// Use this for initialization
	void Start () {
	toggle = 0;
	}
	
	// Update is called once per frame
	void Update () {
				
		if((camera.enabled == true)&&(toggle == 0))
		{
			toggle = 1;
			GlobalVars.player_transform = player;
			gui.enabled = true;
		}
		else if((toggle == 1)&&(camera.enabled == false))
		{
			GlobalVars.player_transform = MainPlayer;
			gui.enabled = false;
		}
	
	}
}
