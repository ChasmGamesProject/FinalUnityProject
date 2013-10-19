/// Class	PlayerInput
/// Desc	Handles inputs
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private InteractMode im;
	private RoomManager rm;
	private DescriptionUI dia;
	private DialogueManager dm;
	
	void Start ()
	{
		im = GlobalVars.interact_mode;
		rm = GlobalVars.room_manager;
		dia = GlobalVars.description_ui;
		dm = GlobalVars.dialogue_manager;
	}
	
	void Update ()
	{
		// Right mouse changes interaction mode
		if(Input.GetMouseButtonDown(1))
			if(im)
				if(im.isUseWith())
					im.EndUseWith();
		
		/*
		// 'I' key opens inventory
		if(Input.GetKeyDown(KeyCode.I))
			ToggleInventory();
		*/
		
		// Escape key returns player to main menu
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("menu");
		
		// Space bar displays next line if in conversation
		if(Input.GetKeyDown(KeyCode.Space))
			if(dm)
				dm.Next ();
	}
}
			
