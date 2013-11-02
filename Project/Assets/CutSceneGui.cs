using UnityEngine;
using System.Collections;

public class CutSceneGui : MonoBehaviour {
	public Texture2D[] Images;
	public Camera cutscene;
	public Camera MainRoom;
	public float x,y;
	float passed;
	int iterator;
	// Use this for initialization
	void Start () {
		passed = Time.time;
		iterator = 0;
		MainRoom.enabled = false;
		cutscene.enabled = true;
		
		guiTexture.transform.localScale = new Vector3(x,y,0);
		//guiTexture.enabled = false;
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if((iterator == 5)&&(Time.time - passed > 10))
		{
			guiTexture.
				guiTexture.enabled = false;
				cutscene.enabled = false;
				MainRoom.enabled = true;
				this.enabled = false;
		
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
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			
			cutscene.enabled = false;
			MainRoom.enabled = true;
			guiTexture.enabled = false;
			this.enabled = false;
		}
	}
}
