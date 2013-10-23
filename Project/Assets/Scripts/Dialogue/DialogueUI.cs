/// Class	DialogueUI
/// Desc	Interface for displaying conversation
/// Author	Cameron A. Gardner
/// Date	01/10/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
	private DialogueManager dm;
	private Database db;
	
	private bool show;
	
	private DialogueNode.NodeType Type;
	private List<string> DisplayText;
	
	private Rect TextBox;
	private Rect NextBox;
	
	private Rect ChoiceBox;
	private int ChoiceStartY;
	
	private Texture2D CharacterSprite; // was array (for 2 chars)
	private Rect CharacterBox;
	//private int CharacterPositionsX; // was array (for 2 chars)
	
	void Awake()
	{
		GlobalVars.dialogue_ui = this;
	}

	void Start ()
	{
		dm = GlobalVars.dialogue_manager;
		db = GlobalVars.database;
		
		Type = DialogueNode.NodeType.Unknown;
		DisplayText = new List<string>();
		
		show = false;
		
		TextBox = new Rect(0, (Screen.height * 3) / 4, Screen.width, Screen.height / 4);
		int[] NextBoxSize = {Screen.width / 20, 32};
		NextBox = new Rect(Screen.width / 2 - NextBoxSize[0] / 2, (Screen.height * 3) / 4 + NextBoxSize[0], NextBoxSize[0], NextBoxSize[1]);
		int[] ChoiceBoxSize = {Screen.width / 4, Screen.width / 40};
		ChoiceBox = new Rect(Screen.width / 2 - ChoiceBoxSize[0] / 2, 0, ChoiceBoxSize[0], ChoiceBoxSize[1]);
		ChoiceStartY = (Screen.height * 3) / 4;
		
		int[] CharacterBoxSize = {Screen.width / 3, Screen.width / 3};
		CharacterBox = new Rect(Screen.width / 2 - CharacterBoxSize[0] / 2, Screen.width / 4 - CharacterBoxSize[1] / 2, CharacterBoxSize[0], CharacterBoxSize[1]);
		//CharacterPositionsX = new int[2]{Screen.width / 4 - CharacterBoxSize[0] / 2, Screen.width - Screen.width / 4 - CharacterBoxSize[0] / 2};
		
		CharacterSprite = null;
	}
	
	void OnGUI()
	{
		if(show)
		{
			GUI.depth = 2;
			if(DisplayText.Count > 0)
			{
				GUI.skin.label.fontSize = 18;
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				if(Type == DialogueNode.NodeType.Line)
				{
					GUI.Label (TextBox, DisplayText[0]);
					if(GUI.Button (NextBox, "Next"))
					{
						dm.Next();
					}
				}
				else if(Type == DialogueNode.NodeType.Choice)
				{
					for(int i = 0; i < DisplayText.Count; i++)
					{
						ChoiceBox.y = ChoiceStartY + 48 * i;
						if(GUI.Button(ChoiceBox, DisplayText[i]))
						{
							dm.PickChoice(i);
						}
					}
				}
				DrawCharacters();
			}
		}
	}
	
	private void DrawCharacters()
	{
		// Draw left character (player)
		if(CharacterSprite != null)
		{
			//CharacterBox.x = CharacterPositionsX[0];
			GUI.DrawTexture(CharacterBox, CharacterSprite);
		}
		
/*
		// Draw right character (conversation partner)
		if(CharacterSprites[1] != null)
		{
		CharacterBox.x = CharacterPositionsX[1];
		GUI.DrawTexture(CharacterBox, CharacterSprites[1]);
		}
*/
	}
	
	public void Show()
	{
		show = true;
	}
	
	public void Hide()
	{
		show = false;
		DisplayText.Clear();
	}
	
	public void Display(DialogueNode dn)
	{
		Type = dn.GetType();
		DisplayText.Clear();
		if(Type == DialogueNode.NodeType.Line)
		{
			DialogueLine dl = (DialogueLine)dn;
			string s = db.GetCharacter(dl.GetSpeakerId()).GetName();
			if(dl.GetSpeakerId() == 0)
				s += " (You)";
			s += ":\n\"" + dm.AddNames(dl.GetText()) + "\"";
			DisplayText.Add(s);
		}
		else if(Type == DialogueNode.NodeType.Choice)
		{
			List<Choice> lc = ((DialogueChoice)dn).GetChoices();
			for(int i = 0; i < lc.Count; i++)
				DisplayText.Add(dm.AddNames(lc[i].GetText()));
		}
	}
	
	public void Display(DialogueNode.NodeType t, List<string> ls)
	{
		// we have to pass type, in case we have choice with one option for some reason
			// EG. there is only one topic avaliable
		DisplayText.Clear();
		
		Type = t;
		
		DisplayText.AddRange(ls);
	}
	
	public void SetSprite(Texture2D tex)//(int pos, Texture2D tex)
	{
		//if(pos < 2)
		CharacterSprite = tex;
	}
}
