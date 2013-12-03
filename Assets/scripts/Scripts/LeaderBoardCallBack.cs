using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public class LeaderBoardCallBack : App42CallBack {

	public static IList<object> fList = new List<object> ();
	public static IList<string> metaDetails = new List<string> ();

	public void OnSuccess (object response)
	{
		if (response is Game){
			Game gameObj = (Game)response;
			IList<Game.Score> scoreList = gameObj.GetScoreList();

			for (int i=0 ; i< scoreList.Count; i++)
			{
				string userName = scoreList[i].GetFacebookProfile().GetName();
				string fbUserProfilePic = scoreList[i].GetFacebookProfile().GetPicture();
				FBLeaderBoard.GetInstance().ExecuteShow(fbUserProfilePic,userName);	
				IList<object> list = new List<object>();
				string rank = (i+1).ToString();
				string score = scoreList[i].GetValue().ToString();

				list.Add(rank);
				list.Add(fbUserProfilePic);
				list.Add(userName);
				list.Add(score);
				fList.Add(list);
			}
		}
	}
	
	public static IList<object> GetFList(){
		return fList;
	}

	public static void SetFList(IList<object> obj)
	{
		fList = new List<object>(obj);
	}

	public void OnException (Exception e)
	{
		App42Exception ex  = (App42Exception)e;

		int appErrorCode = ex.GetAppErrorCode();
		Debug.Log("Exception Occurred : " + ex.ToString());
		Debug.Log("Exception Occurred : " + ex.GetAppErrorCode());
		Debug.Log("Exception Occurred : " + ex.GetHttpErrorCode());

		if(appErrorCode == 3025){
			FBLeaderBoard.defaultLoadingMessage = "Please LogIn To Continue..";
		}

	}


}
