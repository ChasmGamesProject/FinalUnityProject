/// Class	Inventory
/// Desc	Keeps track of player inventory
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;


public class Inventory : MonoBehaviour
{
	public static readonly int INVENTORY_SIZE = 10;
	
	private int[] slots;
	
	//private DrawInventory di;
	
	Inventory()
	{
		slots = new int[INVENTORY_SIZE];
		for(int i = 0; i < INVENTORY_SIZE; i++)
			slots[i] = -1;
	}
	
	public void Awake()
	{
		GlobalVars.inventory = this;
	}
	
	public void Start()
	{
		/*
		GameObject camera = GameObject.Find ("Main Camera");
		if(camera)
			di = camera.GetComponent<DrawInventory>();
		else
			di = null;
			*/
	}
	
	public bool Add(int obj_id)
	{
		int slot_index = -1;
		
		for(int i = 0; i < INVENTORY_SIZE; i++)
		{
			if(slots[i] == -1)
			{
				slot_index = i;
				break;
			}
		}
		
		if(slot_index == -1)
		{
			print("Inventory Full"); // Replace with proper ingame message, well assuming we want it possible...
				// Thisll be bad if you get item via dialogue...
			return false;
		}
		else
		{
			slots[slot_index] = obj_id;
			//UpdateHUD();
			return true;
		}
	}
	
	public bool Contains(int obj_id)
	{
		for(int i = 0; i < INVENTORY_SIZE; i++)
			if(slots[i] == obj_id)
				return true;
		return false;
	}
	
	public int GetObjectInSlot(int slot_num)
	{
		if(slot_num < 0 || slot_num > INVENTORY_SIZE)
			return -1;
		else
			return(slots[slot_num]);
	}
	
	public void Remove(int obj_id)
	{
		for(int i = 0; i < INVENTORY_SIZE; i++)
		{
			if(slots[i] == obj_id)
			{
				slots[i] = -1;
				//UpdateHUD();
				break;
			}
		}
	}
	
	/*
	private void UpdateHUD() // Tell the script that draws inventory to update its content
	{
		if(di)
			di.UpdateDisplay();
	}
	*/
}
