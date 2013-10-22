/// Class	RoomManager
/// Desc	Keeps track of the room player is in and changing rooms
/// Author	Cameron A. Gardner
/// Date	19/09/2013

using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{
	public int room_cur = 0;
	
	public GameObject[] rooms;
	//Pather = get
	public MainCharacterPathing Pather;
	
	private Camera[] room_cams;
	private AudioListener[] room_lis; // wiretaps
	private Vector3[] room_spawn_points;
	
	private bool room_visible;
	
	private DescriptionUI dia;
	private InteractMode im;
	private Transform PlayerTF;
	
	//Transition Vars
	private bool Transitioning;
	private enum TransitionState{TO_BLACK, FROM_BLACK};
	private TransitionState TransitionStateCurrent;
	private float TransitionTimeRemaining;
	private int NextRoom;
	private Rect FullScreenRect;
	private Texture2D OverlayTexture;
	private Color Col;
	
	
	public void Awake()
	{
		GlobalVars.room_manager = this;
	}
	
	void Start ()
	{
		room_visible = true;
		dia = GlobalVars.description_ui;
		im = GlobalVars.interact_mode;
		PlayerTF = GlobalVars.player_transform;
		
		room_cams = new Camera[rooms.Length];
		room_lis = new AudioListener[rooms.Length];
		room_spawn_points = new Vector3[rooms.Length];
		for(int i = 0; i < rooms.Length; i++)
		{
			Camera c = (Camera)(rooms[i].GetComponentInChildren<Camera>());
			c.enabled = false; // turn off camera
			room_cams[i] = c;
			
			AudioListener al = (AudioListener)(rooms[i].GetComponentInChildren<AudioListener>());
			al.enabled = false;
			room_lis[i] = al;
			
			room_spawn_points[i] = rooms[i].transform.Find("SPAWN_POINT").transform.position;
		}
		
		FullScreenRect = new Rect(0, 0, Screen.width, Screen.height);
		Col = Color.black;
		
		OverlayTexture = (Texture2D)Resources.Load ("textures/white");
		
		EnterRoom (room_cur);
	}
	
	public void Update()
	{
		if(Transitioning)
		{
			TransitionTimeRemaining -= Time.deltaTime;
			if(TransitionTimeRemaining <= 0)
			{
				if(TransitionStateCurrent == TransitionState.TO_BLACK)
				{
					TransitionStateCurrent = TransitionState.FROM_BLACK;
					ExitRoom(room_cur);
					EnterRoom(NextRoom);
					TransitionTimeRemaining = 1.0f;
				}
				else
				{
					Transitioning = false;
					im.SetMode(InteractMode.gMode.GM_WORLD);
				}
			}
		}
	}
	
	public void OnGUI()
	{
		if(Transitioning)
		{
			if(TransitionStateCurrent == TransitionState.TO_BLACK)
				Col.a = 1.0f - TransitionTimeRemaining;
			else
				Col.a = TransitionTimeRemaining;
			GUI.color = Col;
			GUI.DrawTexture(FullScreenRect, OverlayTexture);
			Col.a = 1.0f;
			GUI.color = Col;
		}
	}
	
	public void ToggleRoom()
	{
		if(room_visible)
		{
			ExitRoom(room_cur);
			room_visible = false;
		}
		else
		{
			EnterRoom(room_cur);
			room_visible = true;
		}
	}
	
	public void ChangeRoom(int id)
	{
		if(dia)
			dia.Hide(); // hides dialogue box messages
		
		im.SetMode(InteractMode.gMode.GM_NONE);
		
		NextRoom = id;
		Transitioning = true;
		TransitionTimeRemaining = 1.0f;
		TransitionStateCurrent = TransitionState.TO_BLACK;
	}
	
	private void ExitRoom(int id)
	{
		if(room_cams[id])
			room_cams[id].enabled = false;
		if(room_lis[id])
			room_lis[id].enabled = false;
	}
	
	private void EnterRoom(int id)
	{
		//enable room render components?
		PlayerTF.position = room_spawn_points[id];
		Pather.targetPosition = room_spawn_points[id];
		Pather.targetPosition2 = room_spawn_points[id];
		Pather.yheight = Pather.targetPosition.y;
		Pather.Scanner();
		Pather.cam = room_cams[id];
		
		if(room_cams[id])
			{
				Debug.Log(id);
				room_cams[id].enabled = true; // enable the rooms camera
			}
		if(room_lis[id])
			room_lis[id].enabled = true;
		
		//teleport player
		
		//Set hud to show current room name?
		
		room_cur = id;
	}
}
