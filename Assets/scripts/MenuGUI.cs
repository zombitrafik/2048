using UnityEngine;
using System.Collections;
using System;

public class MenuGUI : MonoBehaviour {

	public static void ShowGameOverMenu(GUIStyle style)
	{
		int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
		int bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", ""));
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 240, 100, 40), "Score: " + score.ToString(), style);
		GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 180, 200, 40), "Best score: " + bestScore.ToString(), style);

		if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 100, 50), "Retry"))
		{
			Application.LoadLevel("main");
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2, 100, 50), "Exit"))
		{
			Application.LoadLevel("main_menu");
		}
		if (score == bestScore) Ini.SaveRecord(bestScore);
	}

	public static bool ShowBackMenu(GUIStyle style, bool menuController)
	{
		if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 100, 50), "Back"))
		{
			menuController = false;
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2, 100, 50), "Exit"))
		{
			Application.LoadLevel("main_menu");
		}
		return menuController;
	}
}
