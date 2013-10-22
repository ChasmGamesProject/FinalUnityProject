
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlotPointer { Begin, Intro, FirstFreeRoam, Argument, NiceLesson, ConflictResolved, Key }; //flesh this out with all the plot points

public class PlotSystem : MonoBehaviour {

    private Dictionary<PlotPointer, bool> Plots;

    public Dictionary<string, PlotPointer> EnumConversion;

    private PlotPointer CurrentPlot;

    public PlotBehaviour Temp;

    private Dictionary<PlotPointer, List<PlotBehaviour>> PlotObjects;

    private Dictionary<PlotPointer, List<PlotPointer>> PlotDependancies;

    private List<PlotPointer> QueuedPlots;

    void Awake()
    {
        GlobalVars.plot_system = this;
        InitialiseEnumConversion();
        InitPlots();
        InitDependancies();
    }

	// Use this for initialization
	void Start () 
    {
        PlotObjects = new Dictionary<PlotPointer, List<PlotBehaviour>>();
        QueuedPlots = new List<PlotPointer>();

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
        QueuedPlots.Add(PlotReached);
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
        bool flag;
        int x = QueuedPlots.Count;
        for (int i = 0; i < x; i++)
        {

            flag = true;
            if (PlotDependancies.ContainsKey(QueuedPlots[i]))
            {
                for (int j = 0; j < PlotDependancies[QueuedPlots[i]].Count; j++)
                {
                    if (Plots[PlotDependancies[QueuedPlots[i]][j]] == false)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    //----
                    if (PlotObjects.ContainsKey(QueuedPlots[i]))
                    {
                        for (int k = 0; k < (PlotObjects[QueuedPlots[i]]).Count; k++)
                        {
                            if (PlotObjects[QueuedPlots[i]][k] != null)
                            {
                                Debug.Log(PlotObjects[QueuedPlots[i]].Count);
                                PlotObjects[QueuedPlots[i]][k].ProgressPlot(QueuedPlots[i]);
                                //QueuedPlots.Remove(QueuedPlots[i]);
                            }
                        }

                    }
                    //---
                }
            }
            else //No dependancies
            {
                if (PlotObjects.ContainsKey(QueuedPlots[i]))
                {
                    for (int k = 0; k < (PlotObjects[QueuedPlots[i]]).Count; k++)
                    {
                        if (PlotObjects[QueuedPlots[i]][k] != null)
                        {
                            Debug.Log("Test5");
                            PlotObjects[QueuedPlots[i]][k].ProgressPlot(QueuedPlots[i]);
                            Debug.Log("Test6");
                        }
                    }
                }
            }
            QueuedPlots.Remove(QueuedPlots[i]);
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
        EnumConversion["nicelesson"] = PlotPointer.NiceLesson;
        EnumConversion["conflictresolved"] = PlotPointer.ConflictResolved;
    }

    private void InitPlots()
    {
        Plots = new Dictionary<PlotPointer, bool>();
        Plots[PlotPointer.Begin] = false;
        Plots[PlotPointer.Intro] = false;
        Plots[PlotPointer.FirstFreeRoam] = false;
        Plots[PlotPointer.Argument] = false;
        Plots[PlotPointer.Key] = false;
        Plots[PlotPointer.ConflictResolved] = false;
        Plots[PlotPointer.NiceLesson] = false;
    }

    private void InitDependancies()
    {
        PlotDependancies = new Dictionary<PlotPointer, List<PlotPointer>>();
        //PlotDependancies[PlotPointer.FirstFreeRoam] = 
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
