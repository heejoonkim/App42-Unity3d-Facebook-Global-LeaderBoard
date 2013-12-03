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

	ServiceAPI App42API = null;
	SocialService App42socialService = null;

	public void OnSuccess (object response)
	{
		if (response is Game) {
			FBLeaderBoard.defaultLoadingMessage = "Fetching Details..";
			Game game = (Game)response;
			IList<Game.Score> scoreList = game.GetScoreList();
			for (int i=0; i< scoreList.Count; i++) {
				string rank = (i+1).ToString();
				string score = scoreList [i].GetValue().ToString();
				string name = scoreList [i].GetUserName();
				IList<string> slist = new List<string>();
				slist.Add(rank);
				slist.Add(score);
				if(!playerDetails1.ContainsKey(name))
				{
					playerDetails1.Add (name, slist);
				}
				ids.Add (name.ToString());
			}

			App42API = AppConstant.GetServce();
			App42socialService = App42API.BuildSocialService (); // Initializing Social Service.
			App42socialService.GetFacebookProfilesFromIds (ids, this);
		}



		if (response is com.shephertz.app42.paas.sdk.csharp.social.Social)
		{
			FBLeaderBoard.defaultLoadingMessage = "Fetching Social Details..";
			com.shephertz.app42.paas.sdk.csharp.social.Social social = (com.shephertz.app42.paas.sdk.csharp.social.Social)response;
			IList<com.shephertz.app42.paas.sdk.csharp.social.Social.PublicProfile> FBProfileDetails = social.GetPublicProfile ();
			for (int i=0; i< FBProfileDetails.Count; i++) 
			{
				string FbUserName = FBProfileDetails [i].GetName ();
				string FbUserProfilePic = FBProfileDetails [i].GetPicture ();
				string FbUserId = FBProfileDetails [i].GetId();
				IList<string> slist1 = new List<string>();
				slist1.Add(FbUserProfilePic);
				slist1.Add(FbUserName);
				if(!playerDetails2.ContainsKey(FbUserId))
				{
					playerDetails2.Add(FbUserId, slist1);
				}
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
