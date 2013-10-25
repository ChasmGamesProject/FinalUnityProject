using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {
	int songtracker;
	public AudioClip Arona;
	public AudioClip Dreams;
	public AudioClip SerialThriller;
	float timepassed;
	float SongLength;
	bool NewSong;
	// Use this for initialization
	void Start () {
		songtracker = 1;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Time.time - timepassed > SongLength)
		{
			NewSong = true;
			if(songtracker < 3)
				songtracker++;
			else
				songtracker = 1;
		}
		
		
		
		//Debug.Log(Time.time - timepassed);
		
		if((NewSong)&&(songtracker == 1))
		{
			timepassed = Time.time;
			audio.clip = Arona;
			audio.Play();
			SongLength = audio.clip.length;
			NewSong = false;
			
		}
		else if((NewSong)&&(songtracker == 2))
		{
			timepassed = Time.time;
			audio.clip = Dreams;
			audio.Play();
			SongLength = audio.clip.length;
			NewSong = false;
		}
		else if((NewSong)&&(songtracker == 3))
		{
			timepassed = Time.time;
			audio.clip = SerialThriller;
			audio.Play();
			SongLength = audio.clip.length;
			NewSong = false;
		}
		
	}
}
