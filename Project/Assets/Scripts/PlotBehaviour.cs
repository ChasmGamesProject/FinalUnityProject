using UnityEngine;
using System.Collections;

public class PlotBehaviour : MonoBehaviour 
{
    public PlotPointer[] PlotLinks;

    protected Database db;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CheckPlotLinks(PlotPointer PlotToCheck)
    {
        for (int i = 0; i < PlotLinks.Length; i++)
        {
            if (PlotLinks[i] == PlotToCheck)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void ProgressPlot(PlotPointer PlotToProgress)
    {

    }

    public void TempFunc()
    {
        Debug.Log("Printing me");
    }
}

