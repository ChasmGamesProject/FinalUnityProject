using UnityEngine;
using System.Collections;

public class LockStatusChecker : MonoBehaviour {
	public WorldDoor door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver()
	{
				
			if(GlobalVars.plot_system.CheckPlotStatus(PlotPointer.Key))
			{
				door.Unlock();
			}
		
	}
}
