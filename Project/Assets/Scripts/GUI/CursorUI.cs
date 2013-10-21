/// Class	MouseCursor
/// Desc	Displays mouse cursor
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class CursorUI : MonoBehaviour
{
	private Texture2D[] cursorTextures;
	
	public enum CursorNames
	{
		None,
		Identify,
		Use,
		Talk
	}
	
	private const int CURSOR_COUNT = 4;
	
	private InteractMode im;
	
	private int cursor_index;
	
	int cursor_x = 48; // size
	int cursor_y = 48;
	
	void Awake()
	{
		GlobalVars.cursor_ui = this;
	}
	
	void Start()
	{
		im = GlobalVars.interact_mode;
		
		Screen.showCursor = false;
		cursor_index = 0;
		
		cursorTextures = new Texture2D[CURSOR_COUNT];
		string base_path = "textures/GUI/cursor/";
		cursorTextures[0] = (Texture2D)Resources.Load(base_path + "blank");
		cursorTextures[1] = (Texture2D)Resources.Load(base_path + "identify");
		cursorTextures[2] = (Texture2D)Resources.Load(base_path + "use");
		cursorTextures[3] = (Texture2D)Resources.Load(base_path + "talk");
	}
	
	public void ShowSymbol(CursorNames c)
	{
		switch(c)
		{
		case CursorNames.None:
			cursor_index = 0;
			break;
		case CursorNames.Identify:
			cursor_index = 1;
			break;
		case CursorNames.Use:
			cursor_index = 2;
			break;
		case CursorNames.Talk:
			cursor_index = 3;
			break;
		}
	}
	
	public void HideSymbol()
	{
		cursor_index = 0;
	}
	
	void OnGUI()
	{
		GUI.depth = 1;
		GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursor_x, cursor_y), cursorTextures[cursor_index]);
	}
}
