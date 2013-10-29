using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPlotBehaviour : PlotBehaviour 
{

    private int CharacterID;
    private WorldCharacter C;
    private Inventory inv;
   // private Database db;
    private CharacterData myOwner;
    private List<int> ConversationTopics;

	// Use this for initialization
	void Start () 
    {
        C = (WorldCharacter)this.gameObject.GetComponent(typeof(WorldCharacter));

        CharacterID = C.CharacterId;
        db = GlobalVars.database;
        inv = GlobalVars.inventory;
        myOwner = db.GetCharacter(CharacterID);
        ConversationTopics = new List<int>();
        //myOwner.AddAvaliableTopic(0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void ProgressPlot(PlotPointer PlotToProgress)
    {
        if(myOwner.CheckPlotTopics(PlotToProgress))
        {
            ConversationTopics = myOwner.GetPlotTopics(PlotToProgress);
            //myOwner.ClearAllAvaliableTopics();
        }

        for (int i = 0; i < ConversationTopics.Count; i++)
        {
            myOwner.AddAvaliableTopic(ConversationTopics[i]);
            ConversationTopics.Clear();
        }

        if (PlotToProgress == PlotPointer.Items)
        {
            if (inv == null)
            {
                inv = GlobalVars.inventory;
            }
            inv.Add(11);
            inv.Add(12);
            Debug.Log("Items");
        }
    }
}
