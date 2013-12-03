using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.SocialPlatforms;
using System.Text.RegularExpressions;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Collections.Generic;

public class FBLeaderBoard : MonoBehaviour {

	//===============Initialising Callbacks============================
	LeaderBoardCallBack callback = new LeaderBoardCallBack();
	SaveCallback saveCallback = new SaveCallback();
	GlobalLeaderBoard globalLBCallBack = new GlobalLeaderBoard();
	//===============Initialising Callbacks============================

	AppConstant constants = new AppConstant();
	Dictionary <string , object> dist = new Dictionary<string, object>();

	ServiceAPI sp = null;
	ScoreBoardService scoreBoardService = null;
	private static FBLeaderBoard con = null;

	private Vector2 scrollPosition = Vector2.zero;
	private bool app42 = true;
	public string scoreFromTxtFld;
	public static string defaultLoadingMessage;

	// Use this for initialization
	void Start () {

		defaultLoadingMessage = "Click Global Leaderboard ...";
	}
	
	// Update is called once per frame
	void Update () {
	
		if(AppConstant.GetSaved())
		{
			defaultLoadingMessage = "SuccessFully Saved...";
			AppConstant.SetSaved(false);
			ShowApp42LeaderBoard(FB.AccessToken);
			defaultLoadingMessage = "";
			LeaderBoardCallBack.fList = new List<object>();
		}

		if(FB.AccessToken !="" && app42)
		{
			app42 = false;
			ShowApp42LeaderBoard(FB.AccessToken);
			LeaderBoardCallBack.fList = new List<object>();
		}

	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect (100,50,700,600));
		GUILayout.BeginVertical();

		if	(GUILayout.Button("Connect", GUILayout.Height(30),GUILayout.Width(510)))
		{
			defaultLoadingMessage = "Please Wait...";
			CallFBInit();
		}

		GUILayout.BeginHorizontal();
		GUILayout.Label("RANK", GUILayout.Width(150));
		GUILayout.Label("IMAGE", GUILayout.Width(150));
		GUILayout.Label("NAME", GUILayout.Width(150));
		GUILayout.Label("SCORE", GUILayout.Width(50));
		GUILayout.EndHorizontal();

		GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(350),GUILayout.Width(520));
		for(int i = 0; i < LeaderBoardCallBack.GetFList().Count; i++)
		{
			IList<object> details = (IList<object>)LeaderBoardCallBack.GetFList()[i];
			GUILayout.BeginHorizontal();
			GUILayout.Label(details[0].ToString(), GUILayout.Width(150));
			if(dist.ContainsKey(details[2].ToString())){
				Texture2D tex = (Texture2D)dist[details[2].ToString()];
				GUILayout.Label(tex, GUILayout.Width(150));
			}

			GUILayout.Label(details[2].ToString(), GUILayout.Width(150));
			GUILayout.Label(details[3].ToString(), GUILayout.Width(50));

			GUILayout.EndHorizontal();
		}
		GUILayout.BeginHorizontal();
		GUILayout.Space(220);
		GUILayout.Label(defaultLoadingMessage, GUILayout.Width(200));
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();

	//	scoreFromTxtFld = GUILayout.TextField(scoreFromTxtFld, 4, GUILayout.Height(30),GUILayout.Width(510));
	//	if	(GUILayout.Button("Save Score", GUILayout.Height(30),GUILayout.Width(510)))
	//	{
	//		defaultLoadingMessage = "Saving Score...";
	//		scoreFromTxtFld = Regex.Replace(scoreFromTxtFld, @"[^0-9]", "");
	//
	//		SaveScoreForFacebookUser(FB.UserId, scoreFromTxtFld);
	//	}
		if	(GUILayout.Button("Global LeaderBoard", GUILayout.Height(30),GUILayout.Width(510)))
		{
			defaultLoadingMessage = "Please Wait....";
			ShowApp42GlobalLeaderBoard();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();

	}//OnGUI End.

	public void ShowApp42LeaderBoard(string fbAccessToken)
	{
		sp = AppConstant.GetServce();
		scoreBoardService = AppConstant.GetScoreService(sp);
		scoreBoardService.GetTopNRankersFromFacebook(constants.GameName, fbAccessToken, 10, callback);
	}

	public void ShowApp42GlobalLeaderBoard()
	{
		sp = AppConstant.GetServce();
		scoreBoardService = AppConstant.GetScoreService(sp); // Initializing scoreBoardService.
		scoreBoardService.GetTopNRankers(constants.GameName, 10, globalLBCallBack);
	}

	public void SaveScoreForFacebookUser(string userId, string score)
	{
		if(userId =="" || userId==null){
			defaultLoadingMessage = "Please LogIn \nTo Save Score...";
		}

		if(score =="" || score==null){
			defaultLoadingMessage = "Please Enter Score \nValue...";
		}

		sp = AppConstant.GetServce();
		scoreBoardService = AppConstant.GetScoreService(sp); // Initializing scoreBoardService.
		scoreBoardService.SaveUserScore(constants.GameName, userId, Convert.ToDouble(score), saveCallback);
	}

	private bool isInit = false;

	//==============FB_INIT==============================
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		isInit = true;
		GUI.enabled = isInit && !FB.IsLoggedIn;
		CallFBLogin();
		defaultLoadingMessage = "Loading Data \nPlease Wait ...";
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	private void CallFBLogin()
	{
		defaultLoadingMessage = "";

		FB.Login("email");
	}
	//==============FB_INIT==============================


	public static FBLeaderBoard GetInstance ()
	{
		if (con == null) {
			con = (new GameObject ("FBLeaderBoard")).AddComponent<FBLeaderBoard> ();
			return con;
		} else {
			return con;
		}
		
	}

	IEnumerator ShowAllImages (string uri, string userName)
	{
		IEnumerator e = executeShowAll (uri,userName);
		while (e.MoveNext())
		{
			yield return e.Current;
		}
	}
	

	public string ExecuteShow (string url ,string userName)
	{
		string responseFromApp42 = null;
		StartCoroutine (ShowAllImages (url,userName));
		return responseFromApp42;
	}
	
	void Awake ()
	{
		// First we check if there are any other instances conflicting
		if (con != null && con != this) {
			// If that is the case, we destroy other instances
			Destroy (gameObject);
		}
		
		// Here we save our singleton instance
		con = this;
		
		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		DontDestroyOnLoad (gameObject);
	}
	

	IEnumerator executeShowAll (string url, string userName)
	{
		WWW www = new WWW (url);
		while (!www.isDone) 
		{
			yield return null;  
		}
		if (www.isDone)
		{
			if(!dist.ContainsKey(userName))
			{
				dist.Add(userName,www.texture);
			}
			defaultLoadingMessage = "";
		}
		
	}

}
