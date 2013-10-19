/// Class	DialogueNode (and various others)
/// Desc	Datastructures used for storing dialogue information
/// Author	Cameron A. Gardner
/// Date	01/10/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ******************************************
// lists could be arrays, file loader puts
// data into list than gives to these classes
// AS ARRAYS
// ******************************************

public class DialogueNode
{
	public enum NodeType
	{
		Unknown,
		Line,
		Choice,
		Action,
		Sprite
	};
	
	protected NodeType Type;
	
	public DialogueNode()
	{
		Type = NodeType.Unknown;
	}
	
	public NodeType GetType()
	{
		return Type;
	}
}

public class DialogueLine : DialogueNode
{
	private int SpeakerId;
	//private int SpriteId; // to show emotion eg sally_happy // may be subject to change
	private string Text;
	
	public DialogueLine()
	{
		Type = DialogueNode.NodeType.Line;
		Set(0, "...");
	}
	
	public DialogueLine(int id, string t)
	{
		Type = DialogueNode.NodeType.Line;
		Set(id, t);
	}
	
	public void Set(int id, string t)
	{
		SpeakerId = id;
		Text = t;
	}
	
	public void SetSpeakerId(int id)
	{
		SpeakerId = id;
	}
	
	public void SetText(string t)
	{
		Text = t;
	}
	
	public int GetSpeakerId()
	{
		return SpeakerId;
	}
	
	public string GetText()
	{
		return Text;
	}
}

public class DialogueSprite : DialogueNode
{
	//private int sprite_pos; // 0 = left, 1 = right
	private int char_id;
	private string sprite_key; // eg happy
	
	public DialogueSprite()
	{
		Type = DialogueNode.NodeType.Sprite;
		//sprite_pos = 0;
		char_id = -1;
		sprite_key = "";
	}
	
/*
	public void SetPosition(int p)
	{
		sprite_pos = p;
	}
	
	public int GetPosition()
	{
		return sprite_pos;
	}
*/
	
	public void SetCharacterId(int id)
	{
		char_id = id;
	}
	
	public int GetCharacterId()
	{
		return char_id;
	}
	
	public void SetSpriteKey(string key)
	{
		sprite_key = key;
	}
	
	public string GetSpriteKey()
	{
		return sprite_key;
	}
}

public class DialogueChoice : DialogueNode
{
	private List<Choice> ChoiceList;
	
	public DialogueChoice()
	{
		Type = DialogueNode.NodeType.Choice;
		ChoiceList = new List<Choice>();
	}
	
	public void AddChoice(Choice c)
	{
		ChoiceList.Add(c);
	}
	
	public List<Choice> GetChoices()
	{
		return ChoiceList;
	}
}

public class DialogueAction : DialogueNode
{
	private ChoiceOutcome Action;
	
	public DialogueAction()
	{
		Type = DialogueNode.NodeType.Action;
		Action = null;
	}
	
	public DialogueAction(ChoiceOutcome co)
	{
		Type = DialogueNode.NodeType.Action;
		Action = co;
	}
	
	public ChoiceOutcome GetAction()
	{
		return Action;
	}
}

public class Choice
{
	private string Text;
	private List<ChoiceOutcome> OutcomeList;
	
	public Choice()
	{
		SetText("...");
		OutcomeList = new List<ChoiceOutcome>();
	}
	
	public void SetText(string t)
	{
		Text = t;
	}
	
	public void AddOutcome(ChoiceOutcome co)
	{
		OutcomeList.Add(co);
	}
	
	public string GetText()
	{
		return Text;
	}
	
	public List<ChoiceOutcome> GetOutcomes()
	{
		return OutcomeList;
	}
}

public class ChoiceOutcome // should be renamed
{
	public enum OutcomeType
	{
		Unknown,
		JumpToNode,
		AddToInventory,
		MoodMod,
		EndConversation
	}
	
	protected OutcomeType Type;
	
	public ChoiceOutcome()
	{
		Type = OutcomeType.Unknown;
	}
	
	public OutcomeType GetType()
	{
		return Type;
	}
}

public class OutcomeJump : ChoiceOutcome
{
	private int NodeId;
	
	public OutcomeJump()
	{
		NodeId = 0;
		Type = OutcomeType.JumpToNode;
	}
	
	public OutcomeJump(int id)
	{
		NodeId = id;
		Type = OutcomeType.JumpToNode;
	}
	
	public int GetNodeId()
	{
		return NodeId;
	}
}

public class OutcomeItem : ChoiceOutcome
{
	private int ItemId;
	
	public OutcomeItem()
	{
		Type = OutcomeType.AddToInventory;
		ItemId = -1;
	}
	
	public OutcomeItem(int id)
	{
		Type = OutcomeType.AddToInventory;
		ItemId = id;
	}
	
	public int GetItemId()
	{
		return ItemId;
	}
}

public class OutcomeMood : ChoiceOutcome
{
	private int CharacterId;
	private int MoodMod;
	
	public OutcomeMood()
	{
		Type = OutcomeType.MoodMod;
		CharacterId = 0;
		MoodMod = 0;
	}
	
	public OutcomeMood(int id, int mod)
	{
		Type = OutcomeType.MoodMod;
		CharacterId = id;
		MoodMod = mod;
	}
	
	public int GetCharacterId()
	{
		return CharacterId;
	}
	
	public int GetMoodMod()
	{
		return MoodMod;
	}
}

public class OutcomeEnd : ChoiceOutcome
{
	public OutcomeEnd()
	{
		Type = OutcomeType.EndConversation;
	}
}