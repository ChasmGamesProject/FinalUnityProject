using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class LoadTopic //: MonoBehaviour
{
	StreamReader File;
	TopicData Topic;
	
	public LoadTopic()
	{		
		Topic = null;
	}
	
	/*
	public void Start()
	{
		ProcessChoiceOutcome("Mood(0,-10)"); // test to ensure it get seperate this string correctly
	}
	*/
	
	public void Load(string fileName)
	{
		File = new StreamReader(fileName, Encoding.Default);
		
		if(File != null)
		{
			Topic = new TopicData("Unknown", new List<DialogueNode>());
			string line;
			
			do
			{
				line = GetValidLine();
				if(line != null)
					ProcessNode(line);
			} while(line != null);
		}
	}
	
	public TopicData GetTopicData()
	{
		TopicData td = Topic; // only allow you to get topic once
		Topic = null;
		return td;
	}
	
	private string GetValidLine()
	{
		string line;
		do
		{
			line = File.ReadLine();
			
			if(line != null)
			{
				if(line.Length == 0)
					continue;
				else if(line[0] == '#') // for comments
					continue;
				else
					return line.Trim(); // return string without leading whitespace
			}
		} while(line != null);
		
		return null;
	}
	
	private void ProcessNode(string line)
	{
		line = line.ToLower();
		DialogueNode Node = null;
		
		switch(line)
		{
		case "line":
			Node = ProcessNodeLine();
			break;
		case "choice":
			Node = ProcessNodeChoice();
			break;
		case "action":
			Node = ProcessNodeAction();
			break;
		case "info":
			ProcessInfo();
			break;
		case "sprite":
			Node = ProcessSprite();
			break;
		}
		
		if(Node != null)
			Topic.GetNodeList().Add(Node);
	}
	
	private DialogueSprite ProcessSprite()
	{
		string line;
		DialogueSprite SpriteNode = new DialogueSprite();
		do
		{
			line = GetValidLine();
			
			if(line!=null)
			{
				if(line[0] == '}')
					break;
				else
				{
					string[] elements = line.Split('=');
					switch(elements[0].ToLower())
					{
/*
					case "position":
						if(elements[1].CompareTo("left") == 0)
							SpriteNode.SetPosition(0);
						else
							SpriteNode.SetPosition(1);
						break;
*/
					case "character_id":
						SpriteNode.SetCharacterId(int.Parse(elements[1]));
						break;
					case "sprite":
						SpriteNode.SetSpriteKey(elements[1]);
						break;
						default:
						AttemptToAddJump(elements);
						break;
					}
				}
			}
		} while(line!=null);
		
		return SpriteNode;
	}
	
	private void ProcessInfo()
	{
		string line;
		do
		{
			line = GetValidLine();
			
			if(line!=null)
			{
				if(line[0] == '}')
					break;
				else
				{
					string[] elements = line.Split('=');
					switch(elements[0].ToLower())
					{
					case "topic_name":
						Topic.SetName(elements[1]);
						break;
					case "flag":
						Topic.SetFlag(elements[1]);
						break;
					}
				}
			}
		}while(line!=null);
	}
	
	private DialogueLine ProcessNodeLine()
	{
		string line;
		DialogueLine LineNode = new DialogueLine();
		do
		{
			line = GetValidLine();
			
			if(line != null)
			{
				if(line[0] == '}')
					break;
				else
				{
					string[] elements = line.Split('=');
					switch(elements[0].ToLower())
					{
					case "speaker_id":
						LineNode.SetSpeakerId(int.Parse(elements[1]));
						break;
					case "text":
						LineNode.SetText(elements[1]);
						break;
					default:
						AttemptToAddJump(elements);
						break;
					}
				}
			}
		} while(line != null);
		return LineNode;
	}
	
	private DialogueAction ProcessNodeAction()
	{
		string line;
		DialogueAction ActionNode = new DialogueAction();
		
		do
		{
			line = GetValidLine();
			if(line != null)
			{
				if(line[0] == '}')
					break;
				else
				{
					string[] elements = line.Split('=');
					switch(elements[0].ToLower())
					{
					case "action":
						ChoiceOutcome co = ProcessChoiceOutcome(elements[1]);
						if(co != null)
							ActionNode.AddAction(co);
						break;
					}
				}
			}
		} while(line != null);
		
		return ActionNode;
	}
	
	private DialogueChoice ProcessNodeChoice()
	{
		DialogueChoice ChoiceNode = new DialogueChoice();
		string line;
		
		do
		{
			line = GetValidLine();
			if(line != null)
			{
				if(line[0] == '}')
					break;
				else
				{
					line = line.ToLower();
					if(line.CompareTo("option") == 0)
					{
						Choice c = ProcessChoiceOption();
						if(c != null)
							ChoiceNode.AddChoice(c);
					}
					else
						AttemptToAddJump(line.ToLower().Split('='));
				}
			}
			
		} while(line != null);
		
		return ChoiceNode;
	}
	
	private Choice ProcessChoiceOption()
	{
		Choice c = new Choice();
		string line;
		
		do
		{
			line = GetValidLine();
			if(line != null)
			{
				if(line[0] == '}')
					break;
				else
				{
					string[] elements = line.Split('=');
					switch(elements[0].ToLower())
					{
					case "text":
						c.SetText(elements[1]);
						break;
					case "outcome":
						ChoiceOutcome co = ProcessChoiceOutcome(elements[1]);
						if(co != null)
							c.AddOutcome(co);
						break;
					}
				}
			}
		} while(line!=null);
		
		return c;
	}
	
	private ChoiceOutcome ProcessChoiceOutcome(string line)
	{
		ChoiceOutcome Outcome = null;
		
		char[] delim = {'(', ',', ')'};
		string[] elements = line.Split(delim, System.StringSplitOptions.None);
		
		switch(elements[0].ToLower())
		{
		case "endconvo":
			Outcome = new OutcomeEnd();
			break;
		case "jump":
			if(elements.Length > 1)
				Outcome = new OutcomeJump(int.Parse(elements[1]));
			break;
		case "mood":
			if(elements.Length > 3)
				Outcome = new OutcomeMood(int.Parse(elements[1]), int.Parse(elements[2]));
			break;
		case "item":
			if(elements.Length > 1)
				Outcome = new OutcomeItem(int.Parse(elements[1]));
			break;
		}
		
		return Outcome;
	}
	
	private void AttemptToAddJump(string[] elements)
	{
		if(elements[0].CompareTo("jump_label") == 0)
		{
			Topic.AddJump(int.Parse(elements[1]), Topic.GetNodeList().Count);
		}
	}
}
