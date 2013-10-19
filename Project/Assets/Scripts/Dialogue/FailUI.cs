using UnityEngine;
using System.Collections;

public class FailUI : MonoBehaviour
{
	private DialogueManager dm;
	
	private bool isVisible;
	private Rect BoxRect;
	private Rect LabelRect;
	private Rect ButtonRect;
	
	void Start()
	{
		dm = GlobalVars.dialogue_manager;
		
		isVisible = false;
		
		int[] BoxSize = new int[2]{Screen.width / 5, Screen.width / 14};
		BoxRect = new Rect(Screen.width / 2 - BoxSize[0] / 2, Screen.height / 2 - BoxSize[1] / 2, BoxSize[0], BoxSize[1]);
		
		int[] LabelSize = new int[2]{Screen.width / 8, Screen.width / 32};
		LabelRect = new Rect(Screen.width / 2 - LabelSize[0] / 2, Screen.height / 2 - LabelSize[1] / 2 - BoxSize[1] / 3, LabelSize[0], LabelSize[1]);
		
		int[] ButtonSize = new int[2]{Screen.width / 16, Screen.width / 64};
		ButtonRect = new Rect(Screen.width / 2 - ButtonSize[0] / 2, Screen.height / 2 - ButtonSize[1] / 2 + BoxSize[1] / 3, ButtonSize[0], ButtonSize[1]);
	}
	
	public void Show()
	{
		isVisible = true;
	}

	void OnGUI()
	{
		if(isVisible)
		{
			GUI.depth = 2;
			GUI.skin.box.fontSize = 14;
			GUI.skin.label.fontSize = 18;
			
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box (BoxRect, "You have failed the conversation.\nPlease try again.");
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(LabelRect, "FAIL");
			if(GUI.Button(ButtonRect, "OK"))
			{
				dm.ResetTopic();
				isVisible = false;
			}
		}
	}
}
