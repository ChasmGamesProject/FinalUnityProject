using UnityEngine;
using System.Collections;

public class CutSceneGui : MonoBehaviour {
	public Texture2D[] Images;
	public Camera cutscene;
	public Camera MainRoom;
	float passed;
	int iterator;
	// Use this for initialization
	void Start () {
		passed = Time.time;
		iterator = 0;
		MainRoom.enabled = false;
		cutscene.enabled = true;
		
		
		//guiTexture.enabled = false;
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if((iterator == 5)&&(Time.time - passed > 10))
		{
				cutscene.enabled = false;
				MainRoom.enabled = true;
		
		}
		else if(Time.time - passed > 10)
		{
			
			passed = Time.time;
			guiTexture.texture = Images[iterator];
			iterator++;
			
		}
		else{
			MainRoom.enabled = false;
			cutscene.enabled = true;
		}
		Debug.Log(guiTexture.texture.width);
	}
}
