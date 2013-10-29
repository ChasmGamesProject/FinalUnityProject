
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlotPointer { Begin, Intro, FirstFreeRoam, Argument, TownHistory, NiceLesson, ConflictResolved, Key, Book, Escape, Sundial, Items }; //flesh this out with all the plot points

public class PlotSystem : MonoBehaviour {

    private Dictionary<PlotPointer, bool> Plots;

    public Dictionary<string, PlotPointer> EnumConversion;

    private PlotPointer CurrentPlot;

    public PlotBehaviour Temp;

    private Dictionary<PlotPointer, List<PlotBehaviour>> PlotObjects;

    private Dictionary<PlotPointer, List<PlotPointer>> PlotDependancies;

    private List<PlotPointer> QueuedPlots;

    private List<PlotPointer> tempRemoval;
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
        tempRemoval = new List<PlotPointer>();
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
        bool queueRemoval = false;
        tempRemoval.Clear();
        int x = QueuedPlots.Count;
        Debug.Log(x);
        for (int i = 0; i < x; i++)
        {
            Debug.Log(i);
            flag = true;
            if (PlotDependancies.ContainsKey(QueuedPlots[i]))
            {
                Debug.Log("Test");

                for (int j = 0; j < PlotDependancies[QueuedPlots[i]].Count; j++)
                {
                    if (Plots[PlotDependancies[QueuedPlots[i]][j]] == false)
                    {
                        flag = false;
                        Debug.Log("Test1");
                    }
                }
                if (flag == true)
                {
                    //----
                    Debug.Log("Test2");
                    if (PlotObjects.ContainsKey(QueuedPlots[i]))
                    {
                        Debug.Log("Test3");
                        for (int k = 0; k < (PlotObjects[QueuedPlots[i]]).Count; k++)
                        {
                            if (PlotObjects[QueuedPlots[i]][k] != null)
                            {
                                Debug.Log("Test4");
                                Debug.Log(PlotObjects[QueuedPlots[i]].Count);
                                PlotObjects[QueuedPlots[i]][k].ProgressPlot(QueuedPlots[i]);
                                Debug.Log("Test5");

                            }
                        }

                    }
                    //---
                    queueRemoval = true;
                    tempRemoval.Add(QueuedPlots[i]);
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
                            PlotObjects[QueuedPlots[i]][k].ProgressPlot(QueuedPlots[i]);

                        }
                    }
                }
                queueRemoval = true;
                tempRemoval.Add(QueuedPlots[i]);
 
            }

        }
        int y = tempRemoval.Count;
        for (int g = 0; g < y; g++)
        {
            QueuedPlots.Remove(tempRemoval[g]);
        }
        Debug.Log("Test6");
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
        EnumConversion["book"] = PlotPointer.Book;
        EnumConversion["townhistory"] = PlotPointer.TownHistory;
        EnumConversion["escape"] = PlotPointer.Escape;
        EnumConversion["sundial"] = PlotPointer.Sundial;
        EnumConversion["items"] = PlotPointer.Items;
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
        Plots[PlotPointer.Book] = false;
        Plots[PlotPointer.TownHistory] = false;
        Plots[PlotPointer.Escape] = false;
        Plots[PlotPointer.Sundial] = false;
        Plots[PlotPointer.Items] = false;
    }

    private void InitDependancies()
    {
        PlotDependancies = new Dictionary<PlotPointer, List<PlotPointer>>();
        List<PlotPointer> Temp = new List<PlotPointer>();
        Temp.Add(PlotPointer.Argument);
        Temp.Add(PlotPointer.NiceLesson);
        PlotDependancies[PlotPointer.TownHistory] = Temp;
    }
 
}