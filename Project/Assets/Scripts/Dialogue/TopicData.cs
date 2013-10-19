using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopicData
{
	
	private string Name;
	
	private string Flag; // topic flag used by Hamishes act system
	
	private List<DialogueNode> Nodes;
	
	private Dictionary<int,int> JumpList;
	
	public TopicData()
	{
		Name = "Unknown";
		Flag = "";
		Nodes = null;
		JumpList = new Dictionary<int, int>();
	}
	
	public TopicData(string n, List<DialogueNode> ldn)
	{
		Name = n;
		Flag = "";
		Nodes = ldn;
		JumpList = new Dictionary<int, int>();
	}
	
	public void SetName(string n)
	{
		Name = n;
	}
	
	public void SetNodeList(List<DialogueNode> ldn)
	{
		Nodes = ldn;
	}
	
	public string GetName()
	{
		return Name;	
	}
	
	public List<DialogueNode> GetNodeList()
	{
		return Nodes;
	}
	
	public void AddJump(int key, int index)
	{
		JumpList.Add(key, index);
	}
	
	public int GetJumpIndex(int key)
	{
		if(JumpList.ContainsKey(key))
			return JumpList[key];
		else
			return -1;
	}
	
	public void SetFlag(string s)
	{
		Flag = s;
	}
	
	public string GetFlag()
	{
		return Flag;
	}
}
