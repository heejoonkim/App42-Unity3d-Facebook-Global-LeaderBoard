using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;


public class AppConstant : MonoBehaviour {
	
	public static string API_KEY = "d794ed6fd8fa49da69e8cb6f3e19ac4a63a22f92d19f1aa7e658ba1d09b645be";
	public static string SECRET_KEY = "3421b54ec141f0a7605662577a6aea355ba3b97f4d7143697888fa606f7a852b";
	public string GameName = "Unity3dLeaderboard";
	private static string userName;
	public static bool isSaved = false;

	public static bool GetSaved(){
		return isSaved;
	}
	
	public static void SetSaved(bool saved){
		isSaved = saved;
	}
	
	public static ServiceAPI GetServce(){
		return new ServiceAPI(API_KEY, SECRET_KEY);
	}
	
	public static ScoreBoardService GetScoreService(ServiceAPI sp){
		return sp.BuildScoreBoardService();
	}
	
}
