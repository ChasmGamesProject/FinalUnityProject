/// Class	DescriptionUI
/// Desc	Displays description text
/// Author	Cameron A. Gardner
/// Date	19/09/2013

using UnityEngine;
using System.Collections;

public class DescriptionUI : MonoBehaviour
{

	private bool show = false;
	
	private string desc;
	
	/*
	private Rect box_rect;
	private Rect label_rect;
	
	private Rect button_rect;
	*/
	
	private Rect world_label;
	private float show_timeout;
	private float show_timeout_max;
	private float show_timeout_fade;
	private Color label_color;
	
	//pu Font font_base;
	//public Font font_outline; // old attempt
	
	GUIStyle guiText;
	
	public void Awake()
	{
		GlobalVars.description_ui = this;
	}
	
	public void Start ()
	{
		world_label = new Rect(0, 0, 512, 128);
		show_timeout = 0.0f;
		show_timeout_max = 3.0f;
		show_timeout_fade = 1.0f;
		label_color = Color.black;
		
		guiText = new GUIStyle();
		guiText.fontSize = 26;
		guiText.font = (Font)Resources.Load ("fonts/description");
		guiText.wordWrap = true;
	}
	
	public void Update()
	{
		if(show_timeout > 0)
		{
			show_timeout -= Time.deltaTime;
			if(show_timeout <= 0)
			{
				show_timeout = 0.0f;
				show = false;
			}
		}
	}
	
	public void Hide()
	{
		show_timeout = 0.0f;
		show = false;
	}
	
	public void SetText(string d)
	{
		desc = d;
		
		show = true;
		
		Vector2 size = guiText.CalcSize(new GUIContent(d));
		
		bool done = false;
		
		if(size.x < world_label.width)
		{
			if(Input.mousePosition.x + size.x > Screen.width)
			{
				world_label.x = Input.mousePosition.x - world_label.width;
				guiText.alignment = TextAnchor.UpperRight;
				done = true;
			}
		}
		else if(Input.mousePosition.x + world_label.width > Screen.width)
		{
			world_label.x = Input.mousePosition.x - world_label.width;
			guiText.alignment = TextAnchor.UpperRight;
			done = true;
		}
		
		if(!done)
		{
			world_label.x = Input.mousePosition.x;
			guiText.alignment = TextAnchor.UpperLeft;
		}
		
		world_label.y = Screen.height - Input.mousePosition.y;
		show_timeout = show_timeout_max;
	}
	
	void OnGUI()
	{
		if(show)
		{
			GUI.depth = 2;
			if(show_timeout < show_timeout_fade)
				label_color.a = show_timeout;
			label_color.r = label_color.g = label_color.b = 0.0f;
			guiText.normal.textColor = label_color;
			
			//guiText.font = font_base;
			
			//GUI.Label(world_label, desc, guiText);
			world_label.x += 2;
			GUI.Label(world_label, desc, guiText);
			world_label.x -= 4;
			GUI.Label(world_label, desc, guiText);
			world_label.y += 2;
			GUI.Label(world_label, desc, guiText);
			world_label.y -= 4;
			GUI.Label(world_label, desc, guiText);
			
			
			world_label.x += 2;
			world_label.y += 2;
			
			//guiText.font = font_outline;
			label_color.r = label_color.g = label_color.b = 1.0f;
			guiText.normal.textColor = label_color;
			GUI.Label(world_label, desc, guiText);
			
			label_color.a = 1.0f;
		}
	}
}
