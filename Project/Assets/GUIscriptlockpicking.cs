using UnityEngine;
using System.Collections;

public class GUIscriptlockpicking : MonoBehaviour {
	
	public Texture Bar;
	Rect Tooltip;
	Rect TooltipClose;
	Rect Tooltiptext;
	Rect TensionBar;
	bool Hide;
	public float BarSize;
	float TooltipHeight;
	float TooltipWidth;
	public bool gamewon;
	void Start()
	{
		BarSize = 0;
		gamewon = false;	
		TooltipHeight = Screen.height/2-Screen.height/8;
		TooltipWidth = Screen.width/2-Screen.width/10;
		
		
		Hide = false;
		TensionBar = new Rect(Screen.width/20,Screen.height/20,Screen.width/Screen.width+BarSize,Screen.height/20);
		Tooltip = new Rect(Screen.width/2-Screen.width/10,Screen.height/2-Screen.height/8,Screen.width/4,Screen.height/4);
		TooltipClose = new Rect(TooltipWidth+Screen.width/8-Screen.width/24,TooltipHeight+TooltipHeight/3,Screen.width/12,Screen.height/24);
		Tooltiptext= new Rect(TooltipWidth+Screen.width/8-Screen.width/10,TooltipHeight+TooltipHeight/16,Screen.width/5,Screen.height/10);
	}
	
	void Update()
	{
		if(Input.GetMouseButtonDown(1))
		{
			Debug.Log("test");
			if(BarSize<300)
			{
			BarSize+=5;
			}
			
		}
		if(BarSize > 0)
		BarSize -= 10*Time.deltaTime;
		TensionBar = new Rect(Screen.width/20,Screen.height/20,Screen.width/Screen.width+BarSize,Screen.height/20);
	}
	
	void OnGUI () {
		
		
		// Make a background box
		
		if(!Hide)
		{
			
		//GUI.DrawTexture()
		GUI.Box(Tooltip, "How to play");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(TooltipClose, "Close window")) {
			Hide = true;
		}

		// Make the second button.
		GUI.Label(Tooltiptext,"To pick the lock use the mouse to position the tip of the pick with a pin and then press the left button on your mouse to push down the pin." +
		"while doing this press the right mouse button to fill the tension bar, if the bar gets too low the pins will not lock in place." +" Once all pins are pushed down the lock will open");
		}
		
		if(gamewon)
		{
			//win screen;
		}
		else{
			GUI.DrawTexture(TensionBar, Bar);
		}
	}
}
