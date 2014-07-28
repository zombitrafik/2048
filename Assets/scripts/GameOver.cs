using UnityEngine;
using System.Collections;
using System;

public class GameOver : MonoBehaviour {

    private const string SCORE_LEADER_BOARD = "leaderboardMoveCrushLeaderboard";
    private const string ACHIEVE_EPIC_FAIL = "achievementEpicFail";

	private static GameOver instance;
	public static GameOver Instance
	{
		get
		{
			return instance;
		}
	}
    
	private GameObject cam;

	void Start()
	{
		instance = this;
		cam = GameObject.Find("Main Camera");
	}



	public void Over()
	{
        MainClass.Instance.isGameOver = true;
        Ini.DeleteSavedGame();
        int score = Convert.ToInt32(GameObject.Find("Score").GetComponent<TextMesh>().text.Replace("Score: ", ""));
        GooglePlayServices.Instance.UpdateRecord(SCORE_LEADER_BOARD, score);
        if (score == 0)
        {
            GooglePlayServices.Instance.UpdateAchieveProgress(ACHIEVE_EPIC_FAIL, 100);
        }
        
        MenuGUI.Instance.ShowGameOverMenu();
		/*
		cam.GetComponent<Blur>().enabled = true;
		iTween.ValueTo(cam, iTween.Hash("from", 0, "to", 1.5f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlur"));
		iTween.ValueTo(cam, iTween.Hash("from", 0, "to", 3.5f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlurBlurSize", "onComplete", "ShowMenu", "onCompleteTarget", gameObject));
		iTween.ValueTo(cam, iTween.Hash("from", 3, "to", 3f, "time", .5f, "delay", 0.5f, "onupdate", "changeMotionBlurBlurIterations"));
		*/
	 }

	void changeMotionBlurDownSample(float newValue)
	{
		cam.GetComponent<Blur>().downsample = (int)newValue;
	}
	void changeMotionBlurBlurSize(float newValue)
	{
		cam.GetComponent<Blur>().blurSize = newValue;
	}
	void changeMotionBlurBlurIterations(float newValue)
	{
		cam.GetComponent<Blur>().blurIterations = (int)newValue;
	}
    
}
