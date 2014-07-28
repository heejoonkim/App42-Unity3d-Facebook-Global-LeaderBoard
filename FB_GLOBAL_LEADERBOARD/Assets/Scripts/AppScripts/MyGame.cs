using UnityEngine;
using System.Collections;

public class MyGame : MonoBehaviour {

	public static double scoreValue = 100;
	public GUISkin GameSkin;

	void Awake(){
		scoreValue = 100;
	}

	void OnGUI()
	{
		GUI.skin = GameSkin;
		GUILayout.BeginArea(new Rect (Screen.width/2 - 150, Screen.height/2 - 100, 500,500));
		GUILayout.BeginVertical();
		GUILayout.Label ("Your Score : "+ scoreValue,GUILayout.Width(400),GUILayout.Height(100));
		if	(GUILayout.Button("Click Me! To Increase Score...", GUILayout.Height(70),GUILayout.Width(400)))
		{
			scoreValue += 100;
		}
		
		if	(GUILayout.Button("Kill Me, Save My Score !!!" , GUILayout.Height(70),GUILayout.Width(400)))
		{	
			new App42APIs().SaveScore();
		}

		if	(GUILayout.Button("QUIT", GUILayout.Height(70),GUILayout.Width(400)))
		{
			Application.Quit();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
