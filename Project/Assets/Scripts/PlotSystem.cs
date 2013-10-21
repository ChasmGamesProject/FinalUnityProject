
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlotPointer { Begin, Intro, FirstFreeRoam, Argument, Key }; //flesh this out with all the plot points

public class PlotSystem : MonoBehaviour {

    private Dictionary<PlotPointer, bool> Plots;

    public Dictionary<string, PlotPointer> EnumConversion;

    private PlotPointer CurrentPlot;

    public PlotBehaviour Temp;

    private Dictionary<PlotPointer, List<PlotBehaviour>> PlotObjects;



    void Awake()
    {
        GlobalVars.plot_system = this;
        InitialiseEnumConversion();
        InitPlots();
    }

	// Use this for initialization
	void Start () 
    {

        PlotObjects = new Dictionary<PlotPointer, List<PlotBehaviour>>();

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj) //Used to populate the lists
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent(typeof(PlotBehaviour)) != null)
            {
                Temp = (PlotBehaviour)g.GetComponent(typeof(PlotBehaviour));
                for (int i = 0; i < Temp.PlotLinks.Length; i++)
                {
                    if (!PlotObjects.ContainsKey(Temp.PlotLinks[i]))
                    {
                        PlotObjects[Temp.PlotLinks[i]] = new List<PlotBehaviour>();
                        Debug.Log(i);
                        Debug.Log("Plot: " + (int)Temp.PlotLinks[i]);
                    }
                    Debug.Log("Plot: " + (int)Temp.PlotLinks[i]);
                    PlotObjects[Temp.PlotLinks[i]].Add(Temp);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangePlotStatus(PlotPointer PlotReached)
    {
       
        Plots[PlotReached] = true;
        CurrentPlot = PlotReached;
        AdvancePlot();
    }

    public PlotPointer CheckCurrentStatus()
    {
        return CurrentPlot;
    }

    public bool CheckPlotStatus(PlotPointer PlotEvent)
    {
        return Plots[PlotEvent];
    }

    private void AdvancePlot() //used to iterate through datastructure
    {
        if(PlotObjects.ContainsKey(CurrentPlot))
        {
            for(int i = 0; i < (PlotObjects[CurrentPlot]).Count; i++)
            {

                if (PlotObjects[CurrentPlot][i] != null)
                {
                    Debug.Log(PlotObjects[CurrentPlot].Count);
                    PlotObjects[CurrentPlot][i].ProgressPlot(CurrentPlot);
                }

            }
        }
    }

    public PlotPointer GetEnum(string S)
    {
        return EnumConversion[S];
    }

    private void InitialiseEnumConversion()
    {
        EnumConversion = new Dictionary<string, PlotPointer>();
        EnumConversion.Add("begin",PlotPointer.Begin);
        EnumConversion["argument"] = PlotPointer.Argument;
        EnumConversion["key"] = PlotPointer.Key;
        EnumConversion["firstfreeroam"] = PlotPointer.FirstFreeRoam;
        EnumConversion["intro"] = PlotPointer.Intro;
    }

    private void InitPlots()
    {
        Plots = new Dictionary<PlotPointer, bool>();
        Plots[PlotPointer.Begin] = false;
        Plots[PlotPointer.Intro] = false;
        Plots[PlotPointer.FirstFreeRoam] = false;
        Plots[PlotPointer.Argument] = false;
        Plots[PlotPointer.Key] = false;
    }
 
}

/*
 * 2.0  
 * 
 * Contains a function that allows for horizontal progression of the plot
 * When all horizontal plot points are met, progress the plot to the next major plot point
 * 
 * At startup iterates through all the game objects with the base script plotbehaviour attached, checks and sorts these objects into a map depending on the values stored in their plotpointer
 * When the plot is progressed iterate through the appropriate key value and call the progressplot function of all the stored objects for that key
 * 
 * 
 */


/*
 * Plot system
 * 
 * Tracks all the items and conversation points in the game
 * Maps these points to plot points
 * When a plot point is triggered the world changes
 * 
 * Handles the transition between plot points (checks database and adjusts accordingly)
 *
 * Conversations seperated into topics/groups, each group of conversation options are sub to plot progression
 * 
 * Objects and rooms have a locked/unlocked flag. Each of these specifies a different use(or non-use)/action of the object. 
 * 
 * When an item is collected that contains a plot flag it calls a function in the plot system
 * 
 * At compile time it builds up a database of things related to each plot point
 * 
 * Plot points 1 1.1 1.2 1.N -X.N
 * 
 * 
 */
