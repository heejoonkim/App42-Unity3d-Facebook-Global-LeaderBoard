using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.social;

public class AppConstants{

	// LogIn or signUp to AppHQ Console to get API_Key and Secret_Key https://apphq.shephertz.com/register/app42Login.

	public static string API_KEY = "API_KEY";
	public static string SECRET_KEY = "SECRET_KEY";
	public static string gameName = "UnityGame";  // Create a Game in AppHQ console. About How to create Game see README.md.
	public static string collectionName = "FBUserDetails";
	public static string DB_NAME = "UnityDB";
	public static string FB_APP_ID = "FB_APP_ID"; // Create An App on Facebook and Paste your Facebook AppId here.

	/// <summary>
	/// Gets the score service.
	/// </summary>
	/// <returns>The score service.</returns>
	public static ScoreBoardService GetScoreService()
	{
		ScoreBoardService scoreboardService = App42API.BuildScoreBoardService ();
		return scoreboardService;
	}

	/// <summary>
	/// Gets the social service.
	/// </summary>
	/// <returns>The social service.</returns>
	public static SocialService GetSocialService()
	{
		SocialService socialService = App42API.BuildSocialService ();
		return socialService;
	}
}
