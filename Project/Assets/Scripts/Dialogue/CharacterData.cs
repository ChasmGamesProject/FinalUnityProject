using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterData
{
	private string Name;
	private string Bio; // a breif desc of character ?
	private int Mood;
	
	private List<Texture2D> Sprites; //array of sprites eg sally_happy
	
	private const int MOOD_MIN = 0;
	private const int MOOD_MAX = 100;
	
	private List<TopicData> TopicList;
	private List<int> AvaliableTopicList;

    private Dictionary<PlotPointer, List<int>> PlotTopics;

    private PlotSystem PS;

	public CharacterData()
	{
		Name = "Unknown";
		Bio = "[Classified]";
		Mood = 100; // does not need to be the case though
		TopicList = new List<TopicData>();
		AvaliableTopicList = new List<int>();
		
		Sprites = new List<Texture2D>();
	}
	
	public CharacterData(string n)
	{
		SetName(n);
		Bio = "[Classified]";
		Mood = 100;
		TopicList = new List<TopicData>();
		AvaliableTopicList = new List<int>();
		
		Sprites = new List<Texture2D>();
	}
	
	public void SetName(string n)
	{
		Name = n;
	}
	
	public string GetName()
	{
		return Name;
	}
	
	public void SetBio(string b)
	{
		Bio = b;
	}
	
	public string GetBio()
	{
		return Bio;
	}
	
	public void SetMood(int m)
	{
		Mood = m;
	}
	
	public bool InGoodMood()
	{
		return Mood > 0;
	}
	
	public void MoodMod(int mod)
	{
		Mood += mod;
		
		if(Mood > MOOD_MAX)
			Mood = MOOD_MAX;
		else if(Mood < MOOD_MIN)
			Mood = MOOD_MIN;
	}
	
	public void MoodReset()
	{
		Mood = MOOD_MAX;
	}
	
	public void AddTopic(TopicData td)
	{
		TopicList.Add (td);
	}
	
	public void AddAvaliableTopic(int id)
	{
		if(!AvaliableTopicList.Contains(id)) // don't add duplicates
			AvaliableTopicList.Add(id);
	}
	
	public void RemoveAvaliableTopic(int id)
	{
		AvaliableTopicList.Remove(id);
	}
	
	public void ClearAllAvaliableTopics()
	{
		AvaliableTopicList.Clear();
	}
	
	public List<int> GetAvaliableTopics()
	{
		return AvaliableTopicList;
	}
	
	public TopicData GetTopic(int id)
	{
		if(id < TopicList.Count && id > -1)
			return TopicList[id];
		else
			return null;
	}
	
	public void AddSprite(Texture2D t)
	{
		if(t != null)
			Sprites.Add(t);
	}
	
	public Texture2D GetSprite(string s)
	{
		Texture2D tex = null;
		
		foreach(Texture2D t in Sprites) // loop through till we find texture sith desired filename
			if(t.name.CompareTo(s) == 0)
				tex = t;
		
		if(tex == null)
			return GetSpriteDefault();
		else
			return tex;
	}
	
	public Texture2D GetSpriteDefault()
	{
		if(Sprites.Count > 0)
			return Sprites[0];
		else
			return null;
	}

    public void AddPlotTopic(string PlotID, int TD)
    {
        if (PlotTopics == null)
        {
            PS = GlobalVars.plot_system;
            PlotTopics = new Dictionary<PlotPointer, List<int>>();        
        }
        if (!PlotTopics.ContainsKey(PS.GetEnum(PlotID)))
        {
            Debug.Log("Topic: " +PlotID);
            PlotTopics[PS.GetEnum(PlotID)] = new List<int>();
        }
        Debug.Log(TD);
        PlotTopics[(PS.GetEnum(PlotID))].Add(TD); //.Add(TD);

    }

    public List<int> GetPlotTopics(PlotPointer ID)
    {
        return PlotTopics[ID];
    }

    public bool CheckPlotTopics(PlotPointer ID)
    {
        if (PlotTopics.ContainsKey(ID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
