/// Class	DialogueManager
/// Desc	Manages a conversation
/// Author	Cameron A. Gardner
/// Date	01/10/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// IMPORTANT
// ** Topics that grant player item can only be used once, else inventory could be filled with dups
		// OR you could check if inventory has item then not give it...

public class DialogueManager : MonoBehaviour
{
	private enum DialogueState
	{
		INACTIVE,
		PICK_TOPIC,
		IN_TOPIC
	}
	
	private DialogueUI dui;
	private Inventory inv;
	private Database db;
	private InteractMode im;
	private FailUI fui;
    private PlotSystem ps;
	
	private DialogueState State;
	private TopicData TopicCurrent;
	private List<DialogueNode> NodeList;
	private int NodeCurrentId;
	private DialogueNode NodeCurrent;
	
	private CharacterData ConversationPartner;
	
	private int CurrentTopicId;
	
	public DialogueManager()
	{
		NodeCurrent = null;
		TopicCurrent = null;
		
		State = DialogueState.INACTIVE;
	}
	
	public void Awake()
	{
		GlobalVars.dialogue_manager = this;
	}
	
	public void Start()
	{
		dui = GlobalVars.dialogue_ui;
		inv = GlobalVars.inventory;
		db = GlobalVars.database;
		im = GlobalVars.interact_mode;
        ps = GlobalVars.plot_system;

		fui = gameObject.GetComponent<FailUI>();
	}
	
	public void StartConversation(int charId)
	{
		if(State == DialogueState.INACTIVE) // cant start a convo if we are already in one
		{
			ConversationPartner = db.GetCharacter(charId);
			if(ConversationPartner != null)
			{
				State = DialogueState.PICK_TOPIC;
				dui.Show ();
				UpdateGUI();
				
				im.SetMode(InteractMode.gMode.GM_DIALOGUE);
			}
		}
	}
	
	private void StartTopic(int id)
	{
		CurrentTopicId = id;
		TopicCurrent = ConversationPartner.GetTopic(id);
		NodeList = TopicCurrent.GetNodeList();
		State = DialogueState.IN_TOPIC;
		NodeCurrentId = 0;
		GotoNode();
	}
	
	public void ResetTopic()
	{
		State = DialogueState.PICK_TOPIC;
		dui.Show();
		UpdateGUI();
	}
	
	public void Next()
	{
		if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrent.GetType() != DialogueNode.NodeType.Choice)
			{
				NodeCurrentId++;
				GotoNode();
			}
		}
	}
	
	public void PickChoice(int c)
	{
		if(State == DialogueState.PICK_TOPIC)
		{
			List<int> li = ConversationPartner.GetAvaliableTopics();
			if(c < li.Count)
				StartTopic(li[c]);
			else
			{
				EndConvo(); // Since we tack the 'goodbye' option on the end its technically an invalid choice
			}
		}
		else if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrent.GetType() == DialogueNode.NodeType.Choice)
			{
				// execute action appropriate to choice
				DialogueChoice dc = (DialogueChoice)NodeCurrent;
				
				List<Choice> lc = dc.GetChoices();
				Choice ch = lc[c]; // i have no idea what array out of bounds does here, oh well it wont be possible
				List<ChoiceOutcome> lco = ch.GetOutcomes();
				bool jumped = false;
				foreach(ChoiceOutcome co in lco)
				{
					DoDialogueAction(co);
					if(co.GetType() == ChoiceOutcome.OutcomeType.JumpToNode)
						jumped = true;
				}
				
				if(!jumped) // Move to next line if there was no Jump outcome
				{
					NodeCurrentId++;
					GotoNode();
				}
			}
		}
	}
	
	private void DoDialogueAction(ChoiceOutcome co)
	{
		switch(co.GetType())
		{
			case ChoiceOutcome.OutcomeType.JumpToNode: // CHANGED
				NodeCurrentId = TopicCurrent.GetJumpIndex(((OutcomeJump)co).GetNodeId()); // ((OutcomeJump)co).GetNodeId();
				GotoNode();
			break;
			case ChoiceOutcome.OutcomeType.AddToInventory:
				if(inv)
					inv.Add(((OutcomeItem)co).GetItemId());
			break;
			case ChoiceOutcome.OutcomeType.MoodMod:
				OutcomeMood om = (OutcomeMood)co;
				CharacterData cd = db.GetCharacter(om.GetCharacterId());
				cd.MoodMod(om.GetMoodMod());
				if(!cd.InGoodMood())
					HandleBadMood();
			break;
			case ChoiceOutcome.OutcomeType.EndConversation:
				ReturnToTopicList();
			break;
		}
	}
	
	private void HandleBadMood()
	{
		//print ("BAD MOOD");
		dui.Hide(); // hide dialogue UI
		fui.Show(); // show fail popup
		ConversationPartner.MoodReset();
	}
	
	private void GotoNode()
	{
		if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrentId < NodeList.Count)
			{
				NodeCurrent = NodeList[NodeCurrentId];
				switch(NodeCurrent.GetType())
				{
				case DialogueNode.NodeType.Action:
					HandleActionNode();
					break;
				case DialogueNode.NodeType.Sprite:
					HandleSpriteNode();
					break;
				default:
					UpdateGUI();
					break;
				}
			}
			else
			{
				// if we reach end of topic, go back to the topic list
				ReturnToTopicList();
			}
		}
	}
	
	private void HandleActionNode()
	{
		DialogueAction da = (DialogueAction)NodeCurrent;
		bool no_jump = da.GetAction().GetType() != ChoiceOutcome.OutcomeType.JumpToNode;
		DoDialogueAction(da.GetAction());
		if(no_jump)
		{
			NodeCurrentId++;
			GotoNode();
		}
	}
	
	private void HandleSpriteNode()
	{
		DialogueSprite ds = (DialogueSprite)NodeCurrent;
		
		Texture2D t = (db.GetCharacter(ds.GetCharacterId())).GetSprite(ds.GetSpriteKey());
		dui.SetSprite(t); //(ds.GetPosition(), t);
		
		NodeCurrentId++;
		GotoNode();
	}
	
	private void ReturnToTopicList()
	{
        if (TopicCurrent.GetFlag() == "")
        {
            //do nothing
        }
        else
        {
            ps.ChangePlotStatus(ps.GetEnum(TopicCurrent.GetFlag()));
        }
		ConversationPartner.RemoveAvaliableTopic(CurrentTopicId);
		State = DialogueState.PICK_TOPIC;
		UpdateGUI();
	}
	
	private void EndConvo()
	{
		dui.Hide();
		State = DialogueState.INACTIVE;
		
		im.SetMode(InteractMode.gMode.GM_WORLD);
	}
	
	private void UpdateGUI()
	{
		if(State == DialogueState.PICK_TOPIC)
		{
			List<string> ls = new List<string>();
			List<int> li = ConversationPartner.GetAvaliableTopics();
			for(int i = 0; i < li.Count; i++)
			{
				ls.Add(ConversationPartner.GetTopic(li[i]).GetName());
			}
			ls.Add ("Goodbye.");
			
			dui.Display(DialogueNode.NodeType.Choice, ls);
			
			//dui.SetSprite(0, (db.GetCharacter(0)).GetSpriteDefault());
			dui.SetSprite(ConversationPartner.GetSpriteDefault());//(1, ConversationPartner.GetSpriteDefault());
		}
		else if(State == DialogueState.IN_TOPIC)
		{
			dui.Display(NodeCurrent);
		}
	}
}
