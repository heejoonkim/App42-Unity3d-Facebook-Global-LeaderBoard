using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.social;

public class FBUserConnect : MonoBehaviour {

	public static string fbAccessToken = "";
	public GUISkin Myskin;

	void Awake(){
		FBLeaderBoard.exceptionMessage = "";
		FBLeaderBoard.isError = false;
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect (Screen.width/2 - 150, Screen.height/2 - 100, 300,300));
		GUILayout.BeginVertical();
		GUI.skin = Myskin;

		if	(!LeaderBoardCallBack.isConnected && GUILayout.Button("FACEBOOK CONNECT", GUILayout.Height(70),GUILayout.Width(300)))
		{	 
			 LoadingMessage.SetMessage("Connecting To Facebook...");
			 new App42APIs().ConnectWithFacebook();
		}

		if	(LeaderBoardCallBack.isConnected && GUILayout.Button("Play Game", GUILayout.Height(70),GUILayout.Width(300)))
		{	 
			Application.LoadLevel("GameScene");
		}
		
		if	(GUILayout.Button("GLOBAL LEADERBOARD", GUILayout.Height(70),GUILayout.Width(300)))
		{	
			LoadingMessage.SetMessage("Please Wait...");
			new App42APIs().GlobalLeaderBoard(true);
		}
		
		if	(GUILayout.Button("QUIT", GUILayout.Height(70),GUILayout.Width(300)))
		{
			Application.Quit();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
