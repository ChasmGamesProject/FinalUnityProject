using UnityEngine;
using System.Collections;

public class ActionTextUI : MonoBehaviour
{
	private bool ShowActionText;
	private string ActionText;
	private Rect ActionTextBox;
	
	void Awake()
	{
		GlobalVars.action_text_ui = this;
	}
	
	void Start () 
	{
		ShowActionText = false;
		ActionText = "";
		ActionTextBox = new Rect(0, Screen.height - Screen.width / 32, Screen.width, Screen.width / 32);
	}
	
	public void SetText(string t)
	{
		ActionText = t;
		ShowActionText = true;
	}
	
	public void Hide()
	{
		ShowActionText = false;
	}
	
	public void OnGUI()
	{
		if(ShowActionText)
		{
			GUI.depth = 2;
			GUI.skin.label.alignment = TextAnchor.UpperCenter;
			GUI.skin.label.fontSize = 24;
			GUI.Label (ActionTextBox, ActionText);
		}
	}
}
