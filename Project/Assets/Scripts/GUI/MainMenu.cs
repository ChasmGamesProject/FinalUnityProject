using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	Rect button;
    private Texture2D Starticon;

    private Texture2D Exiticon;
    private Texture2D ExiticonHover;

    private GUIStyle Menu = new GUIStyle();
	MainMenu()
	{
		int width = Screen.width / 6;
		button = new Rect(Screen.width / 2 +width/2, 0, width, Screen.width /20); // centered, 0, 256, 64
	}
	
	void Start()
	{
        Menu.stretchHeight = true;
        Menu.stretchWidth = true;
        Menu.padding.left = 2;
        //Menu.padding.right = 0;
        //Menu.padding.top = 1;
        Menu.padding.bottom = 1;
		Screen.showCursor = true;
        Starticon = (Texture2D)Resources.Load("textures/GUI/icons/start_button");
        Exiticon = (Texture2D)Resources.Load("textures/GUI/icons/exit_button");
        ExiticonHover = (Texture2D)Resources.Load("textures/GUI/icons/exit_buttonHover");

        Menu.normal.background = Exiticon;
        Menu.hover.background = ExiticonHover;

	}
	
	void OnGUI()
	{
		button.y = (int)(Screen.height * 0.35f);
        if (GUI.Button(button, Starticon, Menu))
			Application.LoadLevel("Final Scene");
		
		button.y += Screen.width / 10;
        GUI.Button(button, "");
		
		button.y += Screen.width / 10;
        if (GUI.Button(button, Exiticon, Menu))
			Application.Quit();
		
		// TEMP BUTTON
		/*
		button.y += Screen.width / 40;
		if(GUI.Button(button, "Dialogue Test"))
			Application.LoadLevel("dialogue_test");
			*/
	}
}
