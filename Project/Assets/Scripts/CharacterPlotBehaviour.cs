using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPlotBehaviour : PlotBehaviour 
{

    private int CharacterID;
    private WorldCharacter C;
   // private Database db;
    private CharacterData myOwner;
    private List<int> ConversationTopics;
	// Use this for initialization
	void Start () 
    {
        C = (WorldCharacter)this.gameObject.GetComponent(typeof(WorldCharacter));
        CharacterID = C.CharacterId;
        db = GlobalVars.database;
        myOwner = db.GetCharacter(CharacterID);
        ConversationTopics = new List<int>();
        //myOwner.AddAvaliableTopic(1);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void ProgressPlot(PlotPointer PlotToProgress)
    {
        ConversationTopics = myOwner.GetPlotTopics(PlotToProgress);

        for (int i = 0; i < ConversationTopics.Count; i++)
        {
            myOwner.AddAvaliableTopic(i);
        }
    }
}
