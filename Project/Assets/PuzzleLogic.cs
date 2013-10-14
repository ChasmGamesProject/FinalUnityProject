using UnityEngine;
using System.Collections;

public class PuzzleLogic : MonoBehaviour {
	public PinLogic[] Pins;
	int lockstatus;
	bool UnlockBeenPlayed;
	public AudioClip UnlockSound;
	
	// Use this for initialization
	void Start () {
	lockstatus = 0;
		UnlockBeenPlayed = false;
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
			UnlockBeenPlayed = true;
			}
		}
	}
}
