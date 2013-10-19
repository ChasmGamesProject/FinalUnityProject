/// Class	LoadFromFile
/// Desc	Loads data from files and sends it to database
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class WorldObjectData
{
	public string Name;
	public string Desc;
	public bool isCollectable;
	public string IconFileName;
	public bool isCombinable;
	public int CombinesWith;
	
	public WorldObjectData()
	{
		Reset ();
	}	
	
	public void Reset()
	{
		Name = "Unknown";
		Desc = "Unknown";
		isCollectable = false;
		IconFileName = "";
		isCombinable = false;
		CombinesWith = -1;
	}
}

public class LoadFromFile : MonoBehaviour
{
	private Database db;
	
	private const string ROOT_PATH = "Assets/text/";
	
	void Awake()
	{
		db = GlobalVars.database;
		
		List<string[]> contents = Load (ROOT_PATH + "PickUpSuccessMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpSuccessMessage(contents[i][0]);
		
		contents = Load (ROOT_PATH + "PickUpFailMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpFailMessage(contents[i][0]);
		
		contents = Load (ROOT_PATH + "UseWithFailMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddUseWithFailMessage(contents[i][0]);
		
		
		LoadObjects(ROOT_PATH + "ObjectsList.txt");
		
		LoadCharacters(ROOT_PATH + "CharacterList.txt");
		
		/*
		Combination c = new Combination();
		c.ObjectIdA = 0;
		c.ObjectIdB = 0;
		c.ObjectIdResult = 3;
		db.AddCombination(c);
		*/
		
		LoadCombinations(ROOT_PATH + "ObjectCombinationList.txt");
	}

 
	private List<string[]> Load(string fileName) // Returns each line as an array
	{
		List<string[]> data = new List<string[]>();
	    try
	    {
	        string line;
	        StreamReader file = new StreamReader(fileName, Encoding.Default);
			
	        
	        using (file)
	        {
	            do
	            {
	                line = file.ReadLine();
	 
	                if (line != null)
						data.Add (line.Split(';'));
	            }
	            while (line != null);
	 
	            file.Close();
	            return data; // success
	        }
		}
		
        catch (System.Exception e)
        {
            print( e.Message);
            return data; // fail
        }
	}
	
	
	private void LoadCombinations(string fileName)
	{
		string line;
        StreamReader file = new StreamReader(fileName, Encoding.Default);
		Combination c;
		
		char[] seperators = new char[2]{'+', '='};
		if(file != null)
        {
			do
            {
                line = file.ReadLine();
				if (line != null)
				{
					if(line.Length > 0)
					{
						string[] elements = line.Split(seperators);
						if(elements.Length == 3)
						{
							c = new Combination();
							c.ObjectIdA = int.Parse(elements[0]);
							c.ObjectIdB = int.Parse(elements[1]);
							c.ObjectIdResult = int.Parse(elements[2]);
							db.AddCombination(c);
						}
					}
				}
			} while(line != null);
		}
	}
	
	void LoadObjects(string fileName)
	{
        string line;
        StreamReader file = new StreamReader(fileName, Encoding.Default);
        
        if(file != null)
        {
			WorldObjectData objectTemp = new WorldObjectData();
			
            do
            {
                line = file.ReadLine();
 
                if (line != null)
				{
					if(line[0] == '{')
					{
						objectTemp.Reset();
						bool done = false;
						//string[] elements;
						do
						{
							line = file.ReadLine();
							if(line == null)
								done = true;
							else if(line[0] == '}')
								done = true;
							else
							{
								string[] elements = line.Split('=');
								if(elements.Length < 2)
									done = true;
								else
									ProcessObjectFileLine(elements, ref objectTemp);
							}
						}
            			while (!done);
						
						Base_Object b;
						if(objectTemp.isCollectable)
						{
							Texture2D texture = (Texture2D)Resources.Load("textures/items/" + objectTemp.IconFileName);
							b = new Collectable(objectTemp.Name, objectTemp.Desc, texture);
						}
						else
						{
							b = new Base_Object(objectTemp.Name, objectTemp.Desc);
						}
						db.AddObject(b);
					}
				}
            }
            while (line != null);
 
            file.Close();
        }
	}
	
	private void ProcessObjectFileLine(string[] elements, ref WorldObjectData objectTemp)
	{
		elements[0] = elements[0].Trim(); // gets rid of leading whitespace
										
		switch(elements[0])
		{
		case "name":
			objectTemp.Name = elements[1];
			break;
		case "desc":
			objectTemp.Desc = elements[1];
			break;
		case "collectable":
			string t = elements[1].ToLower();
			if(t.CompareTo("true") == 0 || t.CompareTo("1") == 0)
				objectTemp.isCollectable = true;
			break;
		case "icon":
			objectTemp.IconFileName = elements[1];
			break;
		}
	}
	
	void LoadCharacters(string fileName)
	{
        string line;
        StreamReader file = new StreamReader(fileName, Encoding.Default);
		
		CharacterData charTemp;
        
        if(file != null)
        {
            do
            {
                line = file.ReadLine();
 
                if (line != null)
				{
					if(line.Length > 0)
					if(line[0] == '{')
					{
						charTemp = new CharacterData();
						bool done = false;
						//string[] elements;
						do
						{
							line = file.ReadLine();
							if(line == null)
								done = true;
							else if(line.Length == 0)
								done = true;
							else if(line[0] == '}')
								done = true;
							else
							{
								string[] elements = line.Split('=');
								if(elements.Length < 2)
									done = true;
								else
									ProcessCharacterFileLine(elements, ref charTemp);
							}
						}
            			while (!done);
						db.AddCharacter(charTemp);
					}
				}
			} while (line != null);
 
            file.Close();
		}
	}
	
	private void ProcessCharacterFileLine(string[] elements, ref CharacterData charTemp)
	{
		elements[0] = elements[0].Trim(); // gets rid of leading whitespace
										
		switch(elements[0])
		{
		case "name":
			charTemp.SetName(elements[1]);
			break;
		case "bio":
			charTemp.SetBio(elements[1]);
			break;
		case "mood":
			charTemp.SetMood(int.Parse(elements[1]));
			break;
		case "sprite":
			charTemp.AddSprite((Texture2D)Resources.Load("textures/characters/" + elements[1]));
			break;
		case "topic_list":
			ProcessTopicList(elements[1], ref charTemp);
			break;
		case "available_topics":
			elements = elements[1].Split(',');
			for(int i = 0; i < elements.Length; i++)
				charTemp.AddAvaliableTopic(int.Parse(elements[i]));
			break;
		}
	}

    private void ProcessTopicList(string fileName, ref CharacterData charTemp)
    {
        StreamReader file = new StreamReader(ROOT_PATH + fileName, Encoding.Default);

        if (file != null)
        {
            LoadTopic lt = new LoadTopic();
            TopicData td = null;
            int i = 0;
            string line;
            do
            {
                line = file.ReadLine();

                if (line != null)
                {
                    if (line[0] == '{')
                    {

                        bool done = false;
                        string PlotID = null;
                        do
                        {
                            line = file.ReadLine();
                            if (line == null)
                                done = true;
                            else if (line[0] == '}')
                                done = true;
                            else
                            {
                                string[] elements = line.Split('=');
                                if (elements.Length == 2)
                                {
                                    if (elements[0] == "plot_marker")
                                    {
                                        PlotID = elements[1];
                                    }
                                }
                                else
                                {
                                    lt.Load(ROOT_PATH + line);
                                    td = lt.GetTopicData();
                                    if (td != null)
                                    {
                                        charTemp.AddTopic(td);
                                        if (PlotID != null)
                                        {
                                            charTemp.AddPlotTopic(PlotID, i);
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                        while (!done);
                    }
                }

            } while (line != null);
        }
    }
}