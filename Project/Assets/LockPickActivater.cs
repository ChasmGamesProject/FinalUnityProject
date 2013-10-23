using UnityEngine;
using System.Collections;

public class LockPickActivater : MonoBehaviour {
	
	public PuzzleLogic LockPickController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void ActivateLockPickPuzzle()
	{
		GlobalVars.room_manager.ChangeRoom(4);
		LockPickController.StartLockPuzzle();
	}
	
	public void DeActivateLockPickPuzzle()
	{
		LockPickController.EndLockPuzzle();
	}
}
