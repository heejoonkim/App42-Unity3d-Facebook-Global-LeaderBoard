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
		if(AppConstants.API_KEY == "API_KEY" || AppConstants.SECRET_KEY == "SECRET_KEY"){
			float width = 700f;
			float height = 100f;
			float left = Screen.width / 2 - 350f;
			float top = Screen.height / 2 - height;
			Rect rect = new Rect(left, top, width, height);
			GUI.Label (rect, " 1. Please Enter API_KEY and SECRET_KEY in AppConstants.cs File.\n 2. Create a game and paste the GameName in that file.\n 3. Create an app on facebook and paste Facebook AppId. \n 4. Go to AppHQ console BusinessServiceManager->SocialService->Facebook Settings-> \n  Paste Facebook AppId & AppSecret");
		}
		GUILayout.BeginArea(new Rect (Screen.width/2 - 150, Screen.height/2 - 100, 300,300));
		GUILayout.BeginVertical();
		GUI.skin = Myskin;
		if	(!LeaderBoardCallBack.isConnected && AppConstants.API_KEY != "API_KEY" && GUILayout.Button("FACEBOOK CONNECT", GUILayout.Height(70),GUILayout.Width(300)))
		{	 
			 LoadingMessage.SetMessage("Connecting To Facebook...");
			 new App42APIs().ConnectWithFacebook();
		}

		if	(LeaderBoardCallBack.isConnected && GUILayout.Button("Play Game", GUILayout.Height(70),GUILayout.Width(300)))
		{	 
			Application.LoadLevel("GameScene");
		}
		
		if	(AppConstants.API_KEY != "API_KEY" && GUILayout.Button("GLOBAL LEADERBOARD", GUILayout.Height(70),GUILayout.Width(300)))
		{	
			LoadingMessage.SetMessage("Please Wait...");
			new App42APIs().GlobalLeaderBoard(true);
		}
		
		if	(AppConstants.API_KEY != "API_KEY" && GUILayout.Button("QUIT", GUILayout.Height(70),GUILayout.Width(300)))
		{
			Application.Quit();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
