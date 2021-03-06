﻿using UnityEngine;
using System.Collections;

public class boxpuzzlegui : MonoBehaviour {
	
	public bool secondtext;
	public Texture Bar;
	Rect Tooltip;
	Rect TooltipClose;
	Rect Tooltiptext;
	Rect TensionBar;
	Rect ExitScreen;
	Rect ExitText;
	Rect ExitButton;
	Rect TensionBackGround;
	bool Hide;
	public float BarSize;
	float TooltipHeight;
	float TooltipWidth;
	public bool gamewon;
	
	void Start()
	{
		//
		BarSize = 0;
		gamewon = false;	
		TooltipHeight = Screen.height/2-Screen.height/6;
		TooltipWidth = Screen.width/2-Screen.width/6;
		
		
		Hide = false;
		TensionBar = new Rect(Screen.width/20,Screen.height/20,Screen.width/Screen.width+BarSize,Screen.height/20);
		Tooltip = new Rect(Screen.width/2-Screen.width/6,Screen.height/2-Screen.height/6,Screen.width/3,Screen.height/3);
		TooltipClose = new Rect(TooltipWidth+Screen.width/8,TooltipHeight+TooltipHeight/2,Screen.width/12,Screen.height/24);
		Tooltiptext= new Rect(TooltipWidth,TooltipHeight+TooltipHeight/16,Screen.width/3,Screen.height/7);
		ExitScreen = new Rect(Screen.width/2-Screen.width/6,Screen.height/2-Screen.height/6,Screen.width/3,Screen.height/3);
		ExitText = new Rect(TooltipWidth,TooltipHeight+TooltipHeight/16,Screen.width/3,Screen.height/7);
		ExitButton = new Rect(TooltipWidth+Screen.width/8,TooltipHeight+TooltipHeight/2,Screen.width/12,Screen.height/24);
		TensionBackGround = new Rect(Screen.width/21,Screen.height/21,Screen.width/Screen.width+310.0f,Screen.height/18);
	}
	
	void Update()
	{
		
		
		
		if(Input.GetMouseButtonDown(1))
		{
			//Debug.Log("test");
			if(BarSize<300)
			{
			BarSize+=5;
			}
			
		}
		if(BarSize > 0)
		BarSize -= 10*Time.deltaTime*(BarSize/20);
		TensionBar = new Rect(Screen.width/20,Screen.height/20,Screen.width/Screen.width+BarSize,Screen.height/20);
	}
	
	void OnGUI () {
		
		
		
		
		GUI.skin.label.fontSize = 15;		
		GUI.skin.box.fontSize = 15;
		
		
		if(secondtext == true)
		{
			GUI.Box(Tooltip, "How to play");
			GUI.Label(Tooltiptext,"Congratulations, you have resolved the conflicts among your friends and in doing so solved the puzzle of the mysterious house.");
			if(GUI.Button(TooltipClose, "Close window")) {
			 Application.LoadLevel("Menu");
		}
			
			
			
		}
		else{
		
		
		if(gamewon)
		{
			GUI.Box (ExitScreen, "Well Done!");
			GUI.Label(ExitText, "Good work you have unlocked the door, close this window to return to the house and explore the room.");
			if(GUI.Button (ExitButton, "Exit"))
			{
				
			}
			
		}
		else
		{
			if(!Hide)
		{
			
		
		GUI.Box(Tooltip, "How to play");		
		if(GUI.Button(TooltipClose, "Close window")) {
			Hide = true;
		}

		// Make the second button.
			GUI.Label(Tooltiptext,"To complete this puzzle, you must reach the Lock Pick at the opposite corner of the room."
					+" To do this right click near the box you wish to move and then left click the box to push it or right click the box to pull it.");
		
		}
			
			//GUI.DrawTexture(TensionBar, Bar);
		}
	}
		}
}
