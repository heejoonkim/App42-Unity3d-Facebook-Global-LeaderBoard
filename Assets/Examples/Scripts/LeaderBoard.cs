//using System;
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using com.shephertz.app42.paas.sdk.csharp;
//using com.shephertz.app42.paas.sdk.csharp.game;
//using com.shephertz.app42.paas.sdk.csharp.social;
//
//public class LeaderBoard : App42CallBack
//{
//
//		IList<string> li = new List<string> ();
//		Dictionary <string,object> obj = new Dictionary<string, object> ();
//		Dictionary <string,object> socialProfile = new Dictionary<string, object> ();
//		static ServiceAPI sp = new ServiceAPI ("d794ed6fd8fa49da69e8cb6f3e19ac4a63a22f92d19f1aa7e658ba1d09b645be", "3421b54ec141f0a7605662577a6aea355ba3b97f4d7143697888fa606f7a852b");
//
//		public void OnSuccess (object response)
//		{
//				if (response is Game) {
//						SocialService ss = sp.BuildSocialService ();
//						Game game = (Game)response;
//			    IList<Game.Score> scoreList = game.GetScoreList();
//						for (int i=0; i< scoreList.Count; i++) {
//								string userName = scoreList [i].GetUserName();
//								li.Add (userName);
//								obj.Add (userName, scoreList [i].GetValue ());
//						}
//						ss.GetFacebookProfilesFromIds (li, this);
//				}
//				if (response is com.shephertz.app42.paas.sdk.csharp.social.Social) {
//						com.shephertz.app42.paas.sdk.csharp.social.Social social = (com.shephertz.app42.paas.sdk.csharp.social.Social)response;
//						IList<com.shephertz.app42.paas.sdk.csharp.social.Social.PublicProfile> names = social.GetPublicProfile ();
//						for (int i=0; i< names.Count; i++) {
//								string name = names [i].GetName ();
//								string picture = names [i].GetPicture ();
//								string id = names [i].GetId ();
//								Dictionary <string,string> ids = new Dictionary<string, string> ();
//								ids.Add (name, picture);
//								socialProfile.Add (id, ids);
//						}
//				}
//
//		
//		}
//	
//		public void OnException (Exception e)
//		{
//				App42Exception ex = (App42Exception)e;
//				Debug.Log ("Exception Occurred : " + ex.ToString ());
//				Debug.Log ("Exception Occurred : " + ex.GetHttpErrorCode ());
//				Debug.Log ("Exception Occurred : " + ex.GetAppErrorCode ());
//		}
//
//		public static void GetUserList(){
//			IList<object> list = new List<object>();
//			foreach(var pair in obj){
//				IList<object> person = new List<object>();
//				string uName = pair.Key;
//				string scoreVal = pair.Value + "";
//				if(socialProfile.ContainsKey(uName)){
//					Dictionary <string,string> prof = (Dictionary<string, string> )socialProfile[uName];
//					foreach(var per in prof){
//
//					}
//				}
//		}
//	}
//}
