using UnityEngine;
using System.Collections;

public class GUIscriptlockpicking : MonoBehaviour {
	
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
		GUI.Label(Tooltiptext,"To pick the lock use the mouse to position the tip of the pick with a pin and then press the left button on your mouse to push down the pin." +
		"while doing this press the right mouse button to fill the tension bar, if the bar gets too low the pins will not lock in place." +" Once all pins are pushed down the lock will open");
		}
			GUI.Box(TensionBackGround, "");
			GUI.DrawTexture(TensionBar, Bar);
		}
	}
}
