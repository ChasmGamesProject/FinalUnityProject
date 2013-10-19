/// Class	InteractMode
/// Desc	Tracks interation mode
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class InteractMode : MonoBehaviour
{
	public enum gMode
	{
		GM_NONE, // transtions and lose screen?
		GM_MENU, // including inventory
		GM_DIALOGUE,
		GM_WORLD, // interaction with world/moving around
		GM_PUZZLE
	}
	
	private bool UseWith;
	private Collectable UseWithObject;
	private int UseWithObjectId;
	
	private gMode ModeCurrent;
	
	public void Awake()
	{
		GlobalVars.interact_mode = this;
	}
	
	public void Start ()
	{
		ModeCurrent = gMode.GM_WORLD;
		
		UseWith = false;
		UseWithObject = null;
	}
	
	public void SetMode(gMode m)
	{
		ModeCurrent = m;
	}
	
	public gMode GetMode()
	{
		return ModeCurrent;
	}
	
	
	public void SetUseWith(int id)
	{
		UseWithObject = (Collectable)GlobalVars.database.GetObject(id);
		UseWithObjectId = id;
		if(UseWithObject != null)
			UseWith = true;
	}
	
	public bool isUseWith()
	{
		return UseWith;
	}
	
	public Collectable GetUseWithObject()
	{
		return UseWithObject;
	}
	
	public int GetUseWithObjectId()
	{
		return UseWithObjectId;
	}
	
	public void EndUseWith()
	{
		UseWith = false;
	}
}
