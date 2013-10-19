using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	Rect button;
	
	MainMenu()
	{
		int width = Screen.width / 5;
		button = new Rect(Screen.width / 2 - width / 2, 0, width, Screen.width / 40); // centered, 0, 256, 64
	}
	
	void Start()
	{
		Screen.showCursor = true;
	}
	
	void OnGUI()
	{
		button.y = (int)(Screen.height * 0.65f);
		if(GUI.Button(button, "Play"))
			Application.LoadLevel("room_test");
		
		button.y += Screen.width / 20;
		GUI.Button(button, "Options");
		
		button.y += Screen.width / 20;
		if(GUI.Button(button, "Exit"))
			Application.Quit();
		
		// TEMP BUTTON
		/*
		button.y += Screen.width / 40;
		if(GUI.Button(button, "Dialogue Test"))
			Application.LoadLevel("dialogue_test");
			*/
	}
}
