using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2, 100, 50), "Start"))
		{
			Application.LoadLevel("main");
		}
	}
}
