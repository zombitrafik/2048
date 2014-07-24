using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MainClass : MonoBehaviour {

	private static MainClass instance;
	public static MainClass Instance
	{
		get
		{
			return instance;
		}
	}

	public GUIStyle style;
	int[,] arr = new int[10, 10];
    private int SwipeID = -1;
    private Vector2 StartPos;
    private float minMovement = Screen.width/ 15;
    private float swipeCD = 0.4f;
    private float lastSwipe = 0;

    public bool isGameOver = false;

	public bool isPauseMenu = false;

	void Start () {
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("game");
        }

		instance = this;

		gameObject.GetComponent<Blur>().enabled = false;
        if (Ini.HaveSavedGame())
        {
            Generating.Instance.Init(arr);
            Generating.Instance.GeneratePositions(Ini.LoadGameState());
            GameObject.Find("Score").GetComponent<TextMesh>().text = "Score: " + Ini.LoadScore();
        }
        else
        {
            GeneratePos();
        }
		Ini.LoadRecord();

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!MenuGUI.Instance.IsShow()) MenuGUI.Instance.ShowBackMenu();
			else MenuGUI.Instance.HideBackMenu();
		}

        if (Time.time-lastSwipe>=swipeCD && !isGameOver)
        {
            
            Swipes();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastSwipe = Time.time;
				if (MovieControlls.MoveItemsLeft(arr)) GeneratePos(); else Neon.Instance.NeonLight("left");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastSwipe = Time.time;
				if (MovieControlls.MoveItemsRight(arr)) GeneratePos(); else Neon.Instance.NeonLight("right");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                lastSwipe = Time.time;
				if (MovieControlls.MoveItemsUp(arr)) GeneratePos(); else Neon.Instance.NeonLight("up");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                lastSwipe = Time.time;
				if (MovieControlls.MoveItemsDown(arr)) GeneratePos(); else Neon.Instance.NeonLight("down");
            }
        }
	}
	
	public void Swipes()
	{
		foreach (var T in Input.touches)
		{
			var P = T.position;
			if (T.phase == TouchPhase.Began && SwipeID == -1)
			{
				SwipeID = T.fingerId;
				StartPos = P;
			}
			else if (T.fingerId == SwipeID)
			{
				var delta = P - StartPos;
				if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement)
				{
					SwipeID = -1;
					if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
					{
						if (delta.x > 0)
						{
							lastSwipe = Time.time;
							if (MovieControlls.MoveItemsRight(arr)) GeneratePos(); else Neon.Instance.NeonLight("right"); 
						}
						else
						{
							lastSwipe = Time.time;
							if (MovieControlls.MoveItemsLeft(arr)) GeneratePos(); else Neon.Instance.NeonLight("left"); 
						}
					}
					else
					{
						if (delta.y > 0)
						{
							lastSwipe = Time.time;
							if (MovieControlls.MoveItemsUp(arr)) GeneratePos(); else Neon.Instance.NeonLight("up"); 
						}
						else
						{
							lastSwipe = Time.time;
							if (MovieControlls.MoveItemsDown(arr)) GeneratePos(); else Neon.Instance.NeonLight("down"); 
						}
					}
				}
			}
		}
	}

	private void GeneratePos()
	{
		Generating.Instance.Init(arr);
		arr = Generating.Instance.GeneratePositions();
	}

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !isGameOver)
        {
            Ini.SaveGameState(arr);
            Ini.SaveScore(Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", "")));
        }
    }

}