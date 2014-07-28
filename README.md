App42-Unity3d-Facebook-Global-LeaderBoard 
=========================================


This is a sample for saving facebook user score and displaying leaderboard among facebook friends.

_Features List :_

1. Saving facebook user score.
2. Displaying leaderboard among facebook friends.
3. Displaying global leaderboard.

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
