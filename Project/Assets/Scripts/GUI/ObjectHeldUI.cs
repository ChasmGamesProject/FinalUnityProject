using UnityEngine;
using System.Collections;

public class ObjectHeldUI : MonoBehaviour
{
	private InteractMode im;
	
	private Rect Box;
	private Rect Icon;
	
	void Start()
	{
		im = GlobalVars.interact_mode;
		if(im == null)
			this.enabled = false;
		
		int BoxWidth = Screen.width / 18;
		Box = new Rect(Screen.width - BoxWidth * 2, Screen.height - BoxWidth * 2, BoxWidth, BoxWidth);
		
		int IconWidth = Screen.width / 22;
		int dif = (BoxWidth - IconWidth) / 2;
		Icon = new Rect(Box.x + dif, Box.y + dif, IconWidth, IconWidth);
	}

	void OnGUI()
	{
		if(im.GetMode () == InteractMode.gMode.GM_WORLD)
		{
			GUI.depth = 2;
			
			if(GUI.Button (Box, ""))
				im.EndUseWith();
			
			if(im.isUseWith())
				GUI.DrawTexture(Icon, im.GetUseWithObject().icon);
		}
	}
}
