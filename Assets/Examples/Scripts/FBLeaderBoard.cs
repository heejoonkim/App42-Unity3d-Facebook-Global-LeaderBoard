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
	AppConstant constants = new AppConstant();

	//static ServiceAPI sp = new ServiceAPI("d794ed6fd8fa49da69e8cb6f3e19ac4a63a22f92d19f1aa7e658ba1d09b645be", "3421b54ec141f0a7605662577a6aea355ba3b97f4d7143697888fa606f7a852b");

	//ScoreBoardService scoreBoardService = null;

	LeaderBoardCallBack callback = new LeaderBoardCallBack();
	SaveCallback saveCallback = new SaveCallback();
	ServiceAPI sp =null;
	ScoreBoardService scoreBoardService = null;
	private Vector2 scrollPosition = Vector2.zero;

	//IList<object> listOfImages = new List<object>();
	Dictionary <string , object> dist = new Dictionary<string, object>();
	private static FBLeaderBoard con = null;
	private bool app42 = true;

	GlobalLeaderBoard globalLBCallBack = new GlobalLeaderBoard();

	public string scoreFromTxtFld;
	private string defaultLoadingMessage;

	// Use this for initialization
	void Start () {
		defaultLoadingMessage = "Click Connect ...";
	}
	
	// Update is called once per frame
	void Update () {
	
		if(AppConstant.GetSaved())
		{
			AppConstant.SetSaved(false);
			ShowApp42LeaderBoard(FB.AccessToken);
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
		 

//		if(AppConstant.GetSaved())
//		{
//			if(FB.AccessToken != "")
//			{
//				AppConstant.SetSaved(false);
//				ShowApp42LeaderBoard("CAAG0kEdZAftwBAEpAmFZCdjZAKIC2jORdNtsFFAnaKvHIT04vnZBtMScfC5pePNt5O5KsGJuCkde4kQZAuW7ZBcPy3mmtE6bPTODQ1cfeYrI0Mlpq35148dqPaWPp9Mc9uOKEGWkiQDQkIHfUf533ZCRA4BoHz79eka3iKKLKMu0rq4QyfmRZB3ZAaswfT8blZBTPVhaLMnCVQuQZDZD");
//				fList = new List<object>();
//			}
//		}
		GUILayout.BeginArea(new Rect (300,50,700,600));
		GUILayout.BeginVertical();

		if	(GUILayout.Button("Connect", GUILayout.Height(30),GUILayout.Width(510)))
		{
			CallFBInit();
		}
		defaultLoadingMessage = "";

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
		GUILayout.Label("dsfsdfgfdgdfhdhfdhf", GUILayout.Width(150));
		GUILayout.EndScrollView();

		scoreFromTxtFld = GUILayout.TextField(scoreFromTxtFld, 4, GUILayout.Height(30),GUILayout.Width(510));
		if	(GUILayout.Button("Save Score", GUILayout.Height(30),GUILayout.Width(510)))
		{
			scoreFromTxtFld = Regex.Replace(scoreFromTxtFld, @"[^0-9]", "");

			SaveScoreForFacebookUser(FB.UserId, scoreFromTxtFld);
		}
		if	(GUILayout.Button("Global LeaderBoard", GUILayout.Height(30),GUILayout.Width(510)))
		{
			ShowApp42GlobalLeaderBoard();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();

	}//OnGUI End.

	public void ShowApp42LeaderBoard(string fbAccessToken)
	{
		//scoreBoardService = sp.BuildScoreBoardService(); // Initializing scoreBoardService.
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
		sp = AppConstant.GetServce();
		scoreBoardService = AppConstant.GetScoreService(sp); // Initializing scoreBoardService.
		scoreBoardService.SaveUserScore(constants.GameName, userId, Convert.ToDouble(score), saveCallback);
	}

//	private string status = "";
	private bool isInit = false;
//	private bool app42 = true;
//	//App42Console app42Connect = new App42Console();
//	public string score;
//	Vector2 scrollVector;
//	private bool save = false;
//	//	public Texture2D fbUserProfilePic;	
//	private string loadingData = "Click Connect ...";
	
	
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
//		save = true;
//		status = "Login called";
		defaultLoadingMessage = "Loading Data Please Wait ...";
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		//Debug.Log("Is game showing? " + isGameShown);
	}
	
	private void CallFBLogin()
	{
		defaultLoadingMessage = "Something Happened";

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
		}
		
	}

}
