using UnityEngine;
using System.Collections;

public class WorldPlaceOnMe : MonoBehaviour
{
	public int ObjectId = 8;
	
	private InteractMode im;
	private ActionTextUI aui;
	private CursorUI cui;
	private Inventory inv;
	
	private bool MouseOver;

	void Start ()
	{
		im = GlobalVars.interact_mode;
		aui = GlobalVars.action_text_ui;
		cui = GlobalVars.cursor_ui;
		inv = GlobalVars.inventory;
	}
	
	void Update()
	{
		if(MouseOver)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				if(im.isUseWith())
				{
					if(im.GetUseWithObjectId() == ObjectId)
					{
						cui.ShowSymbol(CursorUI.CursorNames.Use);
						aui.SetText("Place " + im.GetUseWithObject().name + " Here");
					}
				}
			}
			else
			{
				cui.ShowSymbol(CursorUI.CursorNames.None);
				aui.Hide();
			}
		}
	}
	
	void OnMouseEnter()
	{
		if(this.enabled)
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
			MouseOver = true;
	}
	
	void OnMouseExit()
	{
		if(this.enabled)
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
		{
			MouseOut ();
		}
	}
	
	private void MouseOut()
	{
		MouseOver = false;
		cui.ShowSymbol(CursorUI.CursorNames.None);
		aui.Hide();
	}
	
	void OnMouseDown()
	{
		if(this.enabled)
		if(WorldHelper.isPlayerInRange(gameObject.transform))
		{
			if(im.isUseWith())
			{
				if(im.GetUseWithObjectId() == ObjectId)
                    Application.LoadLevel("Menu");
			}
		}
	}
	
	private void DoSomething()
	{
		im.EndUseWith();
		inv.Remove(ObjectId);
		
		// DO SOMETHING USEFUL
		print ("It was placed");
		
		MouseOut();
		this.enabled = false;
	}
}
