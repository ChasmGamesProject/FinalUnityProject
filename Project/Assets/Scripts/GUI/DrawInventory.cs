/// Class	DrawInventory
/// Desc	Draws inventory on screen
/// Author	Cameron A. Gardner
/// Date	11/09/2013

// OBSOLETE sort of

using UnityEngine;
using System.Collections;

public class DrawInventory : MonoBehaviour
{
	private Inventory inv;
	private Database db;
	
	private bool isVisibile;
	
	//private string text;
	
	//private Rect textBox;
	
	
	// Full Screen Vars
	private Camera main_camera;
	
	private Rect rect_title;
	
	private Rect rect_slot;
	private int[] slot1_pos;
	private int slot_gap;
	private int slot_columns;
	private int slot_rows;
	
	private string[] slot_names;
	private Texture2D[] slot_textures;
	private Texture2D unknown_texture; // For objects whoes icon is missing
	private Texture2D slot_border;

	public void Start ()
	{
		main_camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		main_camera.backgroundColor = Color.gray;
		
		inv = GlobalVars.inventory;
		db = GlobalVars.database;
		
		//textBox = new Rect(0, 0, 256, Screen.height);
		
		rect_title = new Rect(Screen.width / 4, Screen.height / 8, Screen.width / 2, Screen.height / 8);
		
		int slot_size = Screen.height / 5;
		rect_slot = new Rect(0, 0, slot_size, slot_size);
		slot_columns = 5;
		slot_rows = 2;
		slot_gap = Screen.height / 10;
		slot1_pos = new int[2];
		slot1_pos[0] = (int)(Screen.width / 2 - (slot_columns / 2.0f) * slot_size - (slot_gap * (slot_columns - 1)) / 2);
		slot1_pos[1] = Screen.height / 3;
		
		slot_names = new string[Inventory.INVENTORY_SIZE];
		unknown_texture = (Texture2D)Resources.Load("textures/items/unknown"); // texture used for objects without icon
		slot_textures = new Texture2D[Inventory.INVENTORY_SIZE];
		slot_border = (Texture2D)Resources.Load("textures/items/slot_border");
		
		isVisibile = false;
		
		UpdateDisplay(); // Have to do this once at beginning
	}
	
	public void UpdateDisplay()
	{
		/*
		// Debugging inventory
		text = "[Inventory]";
		for(int i = 0; i < Inventory.INVENTORY_SIZE; i++)
		{
			text += "\nSlot #" + i + ": ";
			if(inv.GetObjectInSlot(i) == -1)
				text += "Empty";
			else
				text += db.GetObject(inv.GetObjectInSlot(i)).name;
		}
		text += "\n\nPress 'i' to Toggle Visibility";
		*/
		Collectable obj;
		for(int j = 0; j < Inventory.INVENTORY_SIZE; j++)
		{
			if(inv.GetObjectInSlot(j) == -1)
			{
				slot_names[j] = "empty";
				slot_textures[j] = null;
			}
			else
			{
				obj = (Collectable)db.GetObject(inv.GetObjectInSlot(j));
				slot_names[j] = obj.name;
				if(obj.icon != null)
					slot_textures[j] = obj.icon;
				else
					slot_textures[j] = unknown_texture;
			}
		}
	}
	
	
	void OnGUI()
	{
		GUI.depth = 2;
		//if(isVisibile)
			//GUI.Label(textBox, text);
		
		////////////////////////////////
		// Draw full screen inventory
		////////////////////////////////
		if(isVisibile)
		{
			// "Inventory" text label/title
			GUI.skin.label.fontSize = 48;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(rect_title, "INVENTORY");
			
			
			GUI.skin.label.fontSize = 16;
			GUI.skin.label.alignment = TextAnchor.LowerCenter;
			
			// Draw Cells
			int slot_index;
			for(int i = 0; i < slot_rows; i++)
			{
				for(int j = 0; j < slot_columns; j++)
				{
					rect_slot.x = slot1_pos[0] + j * (slot_gap + rect_slot.width);
					rect_slot.y = slot1_pos[1] + i * (slot_gap + rect_slot.width);
					
					slot_index = i * slot_columns + j;
					GUI.Label(rect_slot, slot_names[slot_index]);
					if(slot_textures[slot_index] != null)
						GUI.DrawTexture(rect_slot, slot_textures[slot_index]);
					
					GUI.DrawTexture(rect_slot, slot_border);
				}
			}
			
			// Exit/close button
		}
	}
	
	public void ToggleVisibility()
	{
		isVisibile = !isVisibile;
		if(isVisibile)
			main_camera.clearFlags = CameraClearFlags.Color;
		else
			main_camera.clearFlags = CameraClearFlags.Nothing;
	}
}
