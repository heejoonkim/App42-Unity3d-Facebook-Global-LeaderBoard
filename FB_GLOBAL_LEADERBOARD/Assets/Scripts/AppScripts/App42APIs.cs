using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.social;

public class App42APIs {

	ScoreBoardService scoreBoardService = null;
	SocialService socialService = null;
	
	public static string fbAccessToken = ""; 

	
	/// <summary>
	/// Connects with facebook.
	/// </summary>
	public void ConnectWithFacebook(){
		socialService = App42API.BuildSocialService ();
		// Making facebook Permissions Array.
		string[] perms = new string[10];
		perms [0] = FBPerms.email;		
		perms [1] = FBPerms.user_friends;
		socialService.DoFBOAuthAndGetToken (AppConstants.FB_APP_ID, perms, false, new LeaderBoardCallBack ());
	}


	/// <summary>
	/// Global leader board.
	/// </summary>
	/// <param name="isGlobalLeaderBoard">If set to <c>true</c> is global leader board.</param>
	public void GlobalLeaderBoard(bool isGlobalLeaderBoard){
		LeaderBoardCallBack.fList.Clear ();
		LeaderBoardCallBack.fromLeaderBoard = true;
		scoreBoardService = App42API.BuildScoreBoardService ();
		Query q = QueryBuilder.Build ("userId","",Operator.LIKE);
		scoreBoardService.SetQuery (AppConstants.collectionName,q);
		if (isGlobalLeaderBoard) 
			scoreBoardService.GetTopNRankers (AppConstants.gameName, 10, new LeaderBoardCallBack ());
	    else {
			if(LeaderBoardCallBack.fbAccessToken == ""){
				FBLeaderBoard.isError = true;
				FBLeaderBoard.exceptionMessage = "Please Connect With Facebook.";
			}
			else{
			LeaderBoardCallBack.fromFriends = true;
			scoreBoardService.GetTopNRankersFromFacebook (AppConstants.gameName, LeaderBoardCallBack.fbAccessToken, 10, new LeaderBoardCallBack ());
			}
			}
	}

	/// <summary>
	/// Saves the score.
	/// </summary>
	public void SaveScore(){
		LeaderBoardCallBack.fromSaveScore = true;
		scoreBoardService = App42API.BuildScoreBoardService ();
		Dictionary<string,object> playerFBProfile = new Dictionary<string, object> ();
		playerFBProfile.Add ("userId",LeaderBoardCallBack.fbUserId);
		playerFBProfile.Add ("name",LeaderBoardCallBack.fbUserName);
		playerFBProfile.Add ("profilePic",LeaderBoardCallBack.fbUserProfilePic);
		scoreBoardService.AddJSONObject (AppConstants.collectionName,playerFBProfile);
		scoreBoardService.SaveUserScore(AppConstants.gameName, LeaderBoardCallBack.fbUserId, MyGame.scoreValue,new LeaderBoardCallBack ());
	}


}
