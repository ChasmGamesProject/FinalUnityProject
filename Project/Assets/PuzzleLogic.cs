using UnityEngine;
using System.Collections;

public class PuzzleLogic : MonoBehaviour {
	public PinLogic[] Pins;
	public GUIscriptlockpicking guibehaviour;
	public PickLogic picklogic;
	int lockstatus;
	bool UnlockBeenPlayed;
	public AudioClip UnlockSound;
	public GameObject DoorToPick;
	private WorldDoor DoorToPickScript;
	
	
	// Use this for initialization
	public void EndLockPuzzle()
	{
		GlobalVars.interact_mode.SetMode(InteractMode.gMode.GM_WORLD);
		GlobalVars.room_manager.ToggleRoom();
		DoorToPickScript.Unlock();

		picklogic.enabled = false;
		for(int i = 0; i < Pins.Length; i++)
		{
			Pins[i].enabled = false;
		}
		guibehaviour.enabled = false;
		this.enabled = false;
	}
	
	public void StartLockPuzzle()
	{
		GlobalVars.interact_mode.SetMode(InteractMode.gMode.GM_PUZZLE);
		GlobalVars.room_manager.ToggleRoom();
		this.enabled = true;
		picklogic.enabled = true;
		for(int i = 0; i < Pins.Length; i++)
		{
			Pins[i].enabled = true;
		}
		guibehaviour.enabled = true;
	}
	
	
	
	void Start () {
		
		DoorToPickScript = DoorToPick.GetComponent<WorldDoor>();
		picklogic.enabled = false;
		for(int i = 0; i < Pins.Length; i++)
		{
			Pins[i].enabled = false;
		}
		guibehaviour.enabled = false;
		lockstatus = 0;
		UnlockBeenPlayed = false;
		this.enabled = false;
		//StartLockPuzzle();
	}
	
	// Update is called once per frame
	void Update () {
		lockstatus = 0;
		for(int i = 0; i < Pins.Length; i++)
			{
				if(Pins[i].unlocked)
					lockstatus++;
			}
		if(lockstatus == Pins.Length)
			{
				if(!UnlockBeenPlayed)
				{
					AudioSource.PlayClipAtPoint(UnlockSound, transform.position);
					guibehaviour.gamewon = true;	
					UnlockBeenPlayed = true;
				}
			}
	}
}
