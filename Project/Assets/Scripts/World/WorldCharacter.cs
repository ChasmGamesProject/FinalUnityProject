/// Class	WorldCharacter
/// Desc	An interactive NPC (can start convos)
/// Author	Cameron A. Gardner
/// Date	1/10/2013

using UnityEngine;
using System.Collections;

public class WorldCharacter : MonoBehaviour
{
	public int CharacterId = 0;
	
	private InteractMode im;
	private DialogueManager dm;
	private CursorUI cui;
	
	private DescriptionUI dui;
	private ActionTextUI aui;
	
	private bool MouseOver;
	
	private CharacterData myData;
	
	public void Start()
	{
		im = GlobalVars.interact_mode;
		
		dm = GlobalVars.dialogue_manager;
		
		cui = GlobalVars.cursor_ui;
		
		dui = GlobalVars.description_ui;
		aui = GlobalVars.action_text_ui;
		
		if(GlobalVars.database == null || dm == null || im == null || GlobalVars.database == null)
			this.enabled = false; // dont run script if we are missing dependacies
		else
			myData = GlobalVars.database.GetCharacter(CharacterId);			
		
		MouseOver = false;
	}
	
	void Update()
	{
		if(MouseOver)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				string actionText;
				if(im.isUseWith())
				{
					actionText = "Use " + im.GetUseWithObject().name + " on " + myData.GetName();
					cui.ShowSymbol(CursorUI.CursorNames.Use);
				}
				else
				{
					actionText = "Talk To " + myData.GetName();
					cui.ShowSymbol(CursorUI.CursorNames.Talk);
				}
				aui.SetText(actionText);
			}
			else
			{
				cui.ShowSymbol(CursorUI.CursorNames.Identify);
				aui.SetText("Describe " + myData.GetName());
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
	
	public void OnMouseDown()
	{
		if(im.GetMode() == InteractMode.gMode.GM_WORLD)
		{
			if(WorldHelper.isPlayerInRange(gameObject.transform))
			{
				if(im.isUseWith())
				{
					if(GlobalVars.description_ui != null && GlobalVars.database != null)
						GlobalVars.description_ui.SetText(GlobalVars.database.GetUseWithFailMessage());
				}
				else
				{
					dm.StartConversation(CharacterId);
					MouseOut ();
				}
			}
			else
			{
				dui.SetText(myData.GetBio());
			}
		}
	}
}
