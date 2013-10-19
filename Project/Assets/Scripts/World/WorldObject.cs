/// Class	World_Object
/// Desc	Interactive world object
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public enum WorldObjectType
{
	OBJ_STANDARD,
	OBJ_NPC,
	OBJ_DOOR
}

public class WorldObject : MonoBehaviour
{
	public int object_id = 0; // zero is default, it means unknown?	
	
	private Database db;
	private InteractMode im;
	
	private ActionTextUI aui;
	private DescriptionUI di;
	
	private Inventory inv;
	
	private CursorUI cui;
	
	private bool MouseOver;
	
	private Base_Object myObjectData;
	
	void Start()
	{
		db = GlobalVars.database;
		if(db == null) // no database, means script cant do shit
			this.enabled = false;
		else
		{
			myObjectData = db.GetObject(object_id);
		
			if(myObjectData == null)
				this.enabled = false;
		}
		
		im = GlobalVars.interact_mode;
		
		aui = GlobalVars.action_text_ui;
		di = GlobalVars.description_ui;
		
		inv = GlobalVars.inventory;
		
		cui = GlobalVars.cursor_ui;
		
		MouseOver = false;
	}
	
	void Update()
	{
		if(MouseOver)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				cui.ShowSymbol(CursorUI.CursorNames.Use);
				string actionText;
				if(im.isUseWith())
					actionText = "Use " + im.GetUseWithObject().name + " with " + myObjectData.name;
				else
					actionText = "Pick Up " + myObjectData.name;
				aui.SetText(actionText);
			}
			else
			{
				cui.ShowSymbol(CursorUI.CursorNames.Identify);
				aui.SetText("Identify " + myObjectData.name);
			}
		}
	}
	
	void OnMouseEnter()
	{
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
			MouseOver = true;
	}
	
	void OnMouseExit()
	{
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
		{
			MouseOut();
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
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				if(im.isUseWith())
				{
					di.SetText(db.GetUseWithFailMessage());
				}
				else
				{
					if(myObjectData.type == Object_Type.OBJ_COLLECT)
					{
						if(inv != null)
							if(inv.Add(object_id))
							{
								di.SetText(db.GetPickUpSuccessMessage());
								MouseOut();
								Destroy(gameObject);
							}
					}
					else
					{
						di.SetText(db.GetPickUpFailMessage());
					}
				}
			}
			else
			{
				di.SetText(myObjectData.desc);
			}
		}
	}
}
