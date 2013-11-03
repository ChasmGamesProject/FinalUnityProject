using UnityEngine;
using System.Collections;

public class PuzzleLogic : MonoBehaviour {
	public PinLogic[] Pins;
	public GameObject[] PinObjects;
	public GUIscriptlockpicking guibehaviour;
	public PickLogic picklogic;
	int lockstatus;
	bool UnlockBeenPlayed;
	public AudioClip UnlockSound;
	public GameObject DoorToPick;
	private WorldDoor DoorToPickScript;
	public GameObject pick;
	
	
	// Use this for initialization
	public void EndLockPuzzle()
	{
		GlobalVars.interact_mode.SetMode(InteractMode.gMode.GM_WORLD);
		GlobalVars.room_manager.ToggleRoom();
		DoorToPickScript.Unlock();

		picklogic.enabled = false;
		pick.rigidbody.Sleep();
		//pick.audio.mute();
		//pick.transform = Vector3(10000,100000,100000);
		for(int i = 0; i < Pins.Length; i++)
		{
			PinObjects[i].rigidbody.Sleep();
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
					AudioSource.PlayClipAtPoint(UnlockSound, transform.position,0.5f);
					guibehaviour.gamewon = true;	
					UnlockBeenPlayed = true;
				}
			}
	}
}
