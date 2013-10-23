/// Class	Door
/// Desc	A door that can change the room
/// Author	Cameron A. Gardner
/// Date	19/09/2013

using UnityEngine;
using System.Collections;

public class WorldDoor : MonoBehaviour
{
	public LockPickActivater activater;
	public int room_opens_to = 0;
	
	public bool locked = false;
	public int lockPickId = 0;
	
	private RoomManager rm;
	private InteractMode im;
	private ActionTextUI aui;
	
	private CursorUI cui;

    private BoxCollider boxCollider;
	private bool MouseOver;
	
	void Start ()
	{
		rm = GlobalVars.room_manager;
		im = GlobalVars.interact_mode;
		aui = GlobalVars.action_text_ui;
		
		cui = GlobalVars.cursor_ui;
		
		MouseOver = false;
	}
	
	void Update()
	{
		if(MouseOver)
		{
			if (WorldHelper.isPlayerInRange(gameObject.transform))
            {
				string actionText;
				if(im.isUseWith())
					actionText = "Use " + im.GetUseWithObject().name + " on Door";
				else
					actionText = "Open Door";
				cui.ShowSymbol(CursorUI.CursorNames.Use);
				aui.SetText(actionText);
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
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
			MouseOver = true;
	}
	
	void OnMouseExit()
	{
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
		if(rm)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				if(im.isUseWith())
				{
					if(im.GetUseWithObjectId() == lockPickId && locked) // cant pick an unlocked door
					{
						StartLockPicking();
					}
					else
					{
						if(GlobalVars.description_ui != null && GlobalVars.database != null)
							GlobalVars.description_ui.SetText(GlobalVars.database.GetUseWithFailMessage());
					}
				}
				else if(!locked)
				{
					rm.ChangeRoom(room_opens_to);
					MouseOut();
				}
				else
				{
					GlobalVars.description_ui.SetText("It's Locked");
				}
			}
		}
	}
	
	public void Unlock()
	{
		locked = false;
		
		// Play unlock sound?
	}
	
	private void StartLockPicking()
	{
		// End use with mode
		im.EndUseWith();
		
		// Remove lock pick from inventory
		GlobalVars.inventory.Remove(lockPickId);
		activater.ActivateLockPickPuzzle();
		
		// Start lock picking puzzle
		//Unlock (); // << remove when it actually starts up lock picking puzzle
	}
}
