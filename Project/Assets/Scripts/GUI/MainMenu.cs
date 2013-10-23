using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	Rect button;
	
	MainMenu()
	{
		int width = Screen.width / 6;
		button = new Rect(Screen.width / 2 +width/7, 0, width, Screen.width /20); // centered, 0, 256, 64
	}
	
	void Start()
	{
		Screen.showCursor = true;
	}
	
	void OnGUI()
	{
		button.y = (int)(Screen.height * 0.35f);
		if(GUI.Button(button, ""))
			Application.LoadLevel("Final Scene");
		
		button.y += Screen.width / 10;
		GUI.Button(button, "");
		
		button.y += Screen.width / 10;
		if(GUI.Button(button, ""))
			Application.Quit();
		
		// TEMP BUTTON
		/*
		button.y += Screen.width / 40;
		if(GUI.Button(button, "Dialogue Test"))
			Application.LoadLevel("dialogue_test");
			*/
	}
}
