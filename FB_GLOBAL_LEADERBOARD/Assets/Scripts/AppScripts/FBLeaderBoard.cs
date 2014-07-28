using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.social;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class FBLeaderBoard : MonoBehaviour {

	public static string exceptionMessage;
	public static bool isError = false;
	public GUISkin Myskin;
	private Vector2 scrollPosition = Vector2.zero;
	public static Dictionary <string , object> dist = new Dictionary<string, object>();

	/// <summary>
	/// Raises the GUI event.
	/// </summary>
	void OnGUI()
	{
		GUI.skin = Myskin;
		GUILayout.BeginArea(new Rect (Screen.width/2 - 250, Screen.height/2 - 240, 800,1000));
		GUILayout.BeginVertical();

		GUILayout.Box ("LEADERBOARD", GUILayout.Width (500), GUILayout.Height (50));

		GUILayout.BeginHorizontal ();
		GUILayout.Space (20);
		GUILayout.Label ("Rank", GUILayout.Width (130), GUILayout.Height (50));
		GUILayout.Label ("ProfilePic", GUILayout.Width (150), GUILayout.Height (50));
		GUILayout.Label ("Name", GUILayout.Width (150), GUILayout.Height (50));
		GUILayout.Label ("Score", GUILayout.Width (150), GUILayout.Height (50));
		GUILayout.EndHorizontal ();
		GUILayout.BeginScrollView (scrollPosition,GUILayout.Height(300),GUILayout.Width(650));
		for(int i = 0; i< LeaderBoardCallBack.GetList().Count; i++)
		{
			IList<string> details = (IList<string>)LeaderBoardCallBack.GetList()[i];
			string userName = details[1].ToString();
			string score = details[3].ToString();
			int rank = i+1;
			
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			GUILayout.Label (rank.ToString(), GUILayout.Width (150), GUILayout.Height (100));
			if(dist.ContainsKey(details[0].ToString())){
				Texture2D pp = (Texture2D)dist[details[0].ToString()];
				GUILayout.Label (pp, GUILayout.Width (120), GUILayout.Height (100));
			}
			GUILayout.Label (userName, GUILayout.Width (170), GUILayout.Height (100));
			GUILayout.Label (score, GUILayout.Width (150), GUILayout.Height (100));
			GUILayout.EndHorizontal ();
		}

		GUILayout.EndScrollView ();

		GUILayout.BeginArea (new Rect (120, 400, 300,100));
		GUILayout.BeginVertical ();
		if	(GUILayout.Button("Friends LeaderBoard", GUILayout.Height(50),GUILayout.Width(300)))
		{
			new App42APIs().GlobalLeaderBoard(false);
		}
		if	(GUILayout.Button("HOME", GUILayout.Height(50),GUILayout.Width(300)))
		{
			Application.LoadLevel("MainScene");
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		if(isError)
		{
			float width = 700f;
			float height = 60f;
			float left = Screen.width / 2 - 350f;
			float top = Screen.height / 2 - height;
			Rect rect = new Rect(left, top, width, height);
			GUI.Label (rect, exceptionMessage);
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

	}





}
