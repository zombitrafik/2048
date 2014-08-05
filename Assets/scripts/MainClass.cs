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

	private bool swipeControlSelected = true; 

	void Start () {

		GameObject.Find("Score").GetComponent<TextMesh>().text = Localization.GetWord("Score") + ": 0";
		GameObject.Find("BestScore").GetComponent<TextMesh>().text = Localization.GetWord("You best score") + ": 0";

		if (Ini.LoadControl() != "swipe") swipeControlSelected = false;

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
            GameObject.Find("Score").GetComponent<TextMesh>().text = Localization.GetWord("Score") + ": " + Ini.LoadScore();
            GameObject.Find("Combo").GetComponent<TextMesh>().text = "x " + Ini.LoadCombo();
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
            if (!MenuGUI.Instance.IsShow() && !isGameOver)
            {
                isPauseMenu = true;
                MenuGUI.Instance.ShowBackMenu();
                SaveProgress();
            }
            else
            {
                isPauseMenu = false;
                MenuGUI.Instance.HideAll();
            }
		}

        if (Time.time-lastSwipe>=swipeCD && !isGameOver && !isPauseMenu)
        {
			if (swipeControlSelected) Swipes();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
				MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
				MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
				MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
				MoveDown();
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
			else
            {
                if (T.fingerId == SwipeID)
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
                    if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
                    {
                        SwipeID = -1;
                        return;
                    }
                }
			}
		}
	}

	public void GeneratePos()
	{
		Generating.Instance.Init(arr);
		arr = Generating.Instance.GeneratePositions();
	}

    public void SaveProgress()
    {
        Ini.SaveGameState(arr);
        int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
        Ini.SaveScore(score);
        int combo = Convert.ToInt32(GameObject.Find("Combo").GetComponent<TextMesh>().text.Replace("x ", ""));
        Ini.SaveCombo(combo);
        GooglePlayServices.Instance.UpdateRecord(GooglePlayServices.SCORE_LEADER_BOARD, score);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !isGameOver)
        {
            SaveProgress();
        }
    }


	public void MoveLeft()
	{
		if (Time.time - lastSwipe >= swipeCD && !isGameOver && !isPauseMenu)
		{
			lastSwipe = Time.time;
			if (MovieControlls.MoveItemsLeft(arr)) GeneratePos(); else Neon.Instance.NeonLight("left");
		}
		else return;
		
	}

	public void MoveRight()
	{
		if (Time.time - lastSwipe >= swipeCD && !isGameOver && !isPauseMenu)
		{
			lastSwipe = Time.time;
			if (MovieControlls.MoveItemsRight(arr)) GeneratePos(); else Neon.Instance.NeonLight("right");
		}
		else return;
		
	}

	public void MoveUp()
	{
		if (Time.time - lastSwipe >= swipeCD && !isGameOver && !isPauseMenu)
		{
			lastSwipe = Time.time;
			if (MovieControlls.MoveItemsUp(arr)) GeneratePos(); else Neon.Instance.NeonLight("up");
		}
		else return;
		
	}

	public void MoveDown()
	{
		if (Time.time - lastSwipe >= swipeCD && !isGameOver && !isPauseMenu)
		{
			lastSwipe = Time.time;
			if (MovieControlls.MoveItemsDown(arr)) GeneratePos(); else Neon.Instance.NeonLight("down");
		}
		else return;
		
	}
}