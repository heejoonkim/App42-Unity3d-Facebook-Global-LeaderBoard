using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.social;

public class GlobalLeaderBoard : App42CallBack
{
	
	IList<string> ids = new List<string> ();
	Dictionary <string,object> playerDetails1 = new Dictionary<string, object> ();
	Dictionary <string,object> playerDetails2 = new Dictionary<string, object> ();
	Dictionary <string,string> metaDetails = new Dictionary<string, string> ();
	IList<object> scorerDetails = new List<object> ();
	static ServiceAPI App42API = new ServiceAPI ("d794ed6fd8fa49da69e8cb6f3e19ac4a63a22f92d19f1aa7e658ba1d09b645be", "3421b54ec141f0a7605662577a6aea355ba3b97f4d7143697888fa606f7a852b");
	
	public void OnSuccess (object response)
	{
		if (response is Game) {
			SocialService App42socialService = App42API.BuildSocialService ();
			Game game = (Game)response;
			IList<Game.Score> scoreList = game.GetScoreList();
			for (int i=0; i< scoreList.Count; i++) {
				string rank = (i+1).ToString();
				string score = scoreList [i].GetValue().ToString();
				string name = scoreList [i].GetUserName();
				Debug.Log("Rank Is ::::::: :::::::: " + rank);
				Debug.Log("UserName Is ::::::: :::::::: " + name);
				Debug.Log("Score Is ::::::: :::::::: " + score);
				IList<string> slist = new List<string>();
				slist.Add(rank);
				slist.Add(score);
				playerDetails1.Add (name, slist);
				ids.Add (name.ToString());
			}
			App42socialService.GetFacebookProfilesFromIds (ids, this);
		}
		if (response is com.shephertz.app42.paas.sdk.csharp.social.Social)
		{
			com.shephertz.app42.paas.sdk.csharp.social.Social social = (com.shephertz.app42.paas.sdk.csharp.social.Social)response;
			IList<com.shephertz.app42.paas.sdk.csharp.social.Social.PublicProfile> FBProfileDetails = social.GetPublicProfile ();
			for (int i=0; i< FBProfileDetails.Count; i++) 
			{
				string FbUserName = FBProfileDetails [i].GetName ();
				string FbUserProfilePic = FBProfileDetails [i].GetPicture ();
				string FbUserId = FBProfileDetails [i].GetId();
				Debug.Log("FACEBOOK   UserName Is ::::::: :::::::: " + FbUserName);
				Debug.Log("FACEBOOK   ProfilePic Is ::::::: :::::::: " + FbUserProfilePic);
				IList<string> slist1 = new List<string>();
				slist1.Add(FbUserProfilePic);
				slist1.Add(FbUserName);
				playerDetails2.Add(FbUserId, slist1);
			}
			GetFinalInfo();
		}
		
		
	}
	
	public void OnException (Exception e)
	{
		App42Exception ex = (App42Exception)e;
		Debug.Log ("Exception Occurred : " + ex.ToString ());
		Debug.Log ("Exception Occurred : " + ex.GetHttpErrorCode ());
		Debug.Log ("Exception Occurred : " + ex.GetAppErrorCode ());
	}


	public void GetFinalInfo()
	{
		IList<object> FinalInfo = new List<object> ();
		
		
		foreach (var rankAndScore in playerDetails1)
		{

			if(playerDetails2.ContainsKey(rankAndScore.Key))
			{
				IList<object> info = new List<object> ();
				IList<string> getInfo = (List<string>)playerDetails1[rankAndScore.Key];
				info.Add(getInfo[0]);
				IList<string> getInfo1 = (List<string>)playerDetails2[rankAndScore.Key];
				info.Add(getInfo1[0]);
				info.Add(getInfo1[1]);
				info.Add(getInfo[1]);
				FinalInfo.Add(info);
				FBLeaderBoard.GetInstance().ExecuteShow(getInfo1[0],getInfo1[1]);
			}

		}
		LeaderBoardCallBack.SetFList (FinalInfo);
	}

}
