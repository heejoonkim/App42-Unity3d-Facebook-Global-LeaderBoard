App42-Unity3D-Facebook-Leaderboard
==================================

App42 Unity3D SDK Facebook Leaderboard Sample.

This is a sample for saving facebook user score and displaying leaderboard among facebook friends.

_Features List :_

1. Saving facebook user score.
2. Displaying leaderboard among facebook friends.

# Running Sample:

1. [Register] (https://apphq.shephertz.com/register) with App42 platform
2. Go to dashboard and click on the Create App.
3. Fill all the mandatory fields to get your APIKey and SecretKey.
4. Download App42 Unity3D Facebook Leaderboard Sample App and unzip it on your machine.
5. Import project in unity editor.

You have to create a game, So that you can access all GameService methods.

__Creating game:__	

For Creating game, you have to follow these steps :-
Login in with [AppHq console] (https://apphq.shephertz.com/register/app42Login).

1. Click on dashBoard , you can see it in Upper-Left in AppHq console.
2. Go to Business Service Manager from left tab.
3. Click on Game Service and select Game.
4. Select App, regarding which you want to create game.
5. Create game "sampleGame" by clicking on "Add Game" button from right tab in AppHQ.
	
Now one can call associated methods of that service by passing this gameName, e.g. save user score, get top rankers from facebook etc.

__Initialize App42 :__

In order to use the various functions available in a specific API. 
A developer has to create an instance of ServiceAPI by passing the apiKey and secretKey which will be created after the app creation from AppHQ dashboard.

Edit AppConstant.cs script and put your API_KEY and SECRET_KEY (which were received in step#2 & #3) as shown below.
```
public class AppConstant : MonoBehaviour 
{
  public static string API_KEY = "YOUR_API_KEY";
  public static string SECRET_KEY = "YOUR_SECRET_KEY";
}
```
__Import Statement__
```
using com.shephertz.app42.paas.sdk.csharp;  
using com.shephertz.app42.paas.sdk.csharp.game;  
```

#Design Details:

__App42 ScoreBoard:__

Initialize App42ScoreBoard
To build an instance of ScoreBoardService, BuildScoreBoardService() method needs to be called.

```
  ScoreBoardService scoreBoardService = sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.
```

Saving Facebook User Score.

For saving facebook user score, we need at least a gameName, facebook userId to whome score have to be save, score value, and a callback.

```
public class App42Console : MonoBehaviour,App42CallBack
{
  public void SaveUserScore(string userId, string score)
  {
    scoreService.SaveUserScore(constants.GameName, userId, Convert.ToDouble(score), callback);
  }
}
```
Here is the CallBack for Response of saved score :-
```
public class SaveCallback : MonoBehaviour,App42CallBack 
{
	public void OnSuccess (object response)
	{
		AppConstant.SetSaved(true);
	}
	
	public void OnException (Exception e)
	{
		// Debug.Log("Exception Occurred : " + e);
	}
}
```
Now We have to show the leaderboard of facebook game players.
```
public class App42Console : MonoBehaviour,App42CallBack 
{
  public void SocialConnectWithApp42(string fbAccessToken)
  	{
  	 sp = AppConstant.GetServce();
  	 scoreService = AppConstant.GetScoreService(sp);
  	 scoreService.GetTopNRankersFromFacebook(constants.GameName, fbAccessToken, 10, this);
  	}
}
```
Here is the CallBack for Response of Top Rankers From Facebook :-
```
public class App42Console : MonoBehaviour,App42CallBack 
{
  public void OnSuccess (object response)
  {
    if (response is Game){
    Game gameObj = (Game)response;
    IList<Game.Score> scoreList = gameObj.GetScoreList();
    Debug.Log(scoreList);
    for (int i=0 ;i< scoreList.Count;i++)
    {
      string userName = scoreList[i].GetFacebookProfile().GetName();
      IList<string> list = new List<string>();
      string rank = (i+1).ToString();
      list.Add(rank);
      list.Add(userName);
      list.Add(scoreList[i].GetValue().ToString());
      fList.Add(list);
    }
  }
  public void OnException (Exception e)
  {
    Debug.Log("Exception Occurred : " + e.ToString());
  }
}
```
__Note:__ Facebook Unity3d SDK doesn’t  support desktop applications at the moment. 
You must run this app on apps.facebook.com/. 
For more information about Unity3D Facebook SDK visit [Getting-started] (https://developers.facebook.com/docs/unity/getting-started/) with App42 platform with Unity Facebook.
