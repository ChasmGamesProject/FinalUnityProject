using UnityEngine;
using System.Collections;

public class BoxManager : MonoBehaviour {
	public string[,] Floor = new string[5,5];
	public Transform[] Boxes;
	public Vector3 temp1;
	public Vector3 temp2;
	float x;
	float z;
	// Use this for initialization
	void Start () {
		for(int i = 0; i<5; i++)
			{
				for(int f = 0; f<5; f++)
				{
					Floor[i,f] = "NotTaken";
				}
			}
		CheckFloor();
	}
	
	
	public void CheckFloor()
	{
		for(int i = 0; i<5; i++)
			{
				for(int f = 0; f<5; f++)
				{
					Floor[i,f] = "NotTaken";
				}
			}
		
		for(int i = 0; i<Boxes.Length; i++)
		{
			if(Boxes[i].localPosition.x>0)
			{
			x = Boxes[i].localPosition.x/2;
			}
			else{
				x = Boxes[i].localPosition.x;
			}
			if(Boxes[i].localPosition.z>0)
			{
			z = Boxes[i].localPosition.z/2;
			}
			else{
				z = Boxes[i].localPosition.z;
			}
			
			Floor[(int)x,(int)z] = "Taken";
			
			
		}
		Floor[4,4] = "Taken";
	}
	// Update is called once per frame
	void Update () {
	
	}
}
