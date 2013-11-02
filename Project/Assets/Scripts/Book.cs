using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour {

	// Use this for initialization

    private MainGUI mg;

	void Start () 
    {
        mg = GlobalVars.maingui;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GlobalVars.plot_system.ChangePlotStatus(PlotPointer.Book);
            mg.ChangeUI(mg.Hack());
        }
    }
}
