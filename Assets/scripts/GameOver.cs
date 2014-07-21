﻿using UnityEngine;
using System.Collections;
using System;

public class GameOver : MonoBehaviour {

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
        int bestScore = Convert.ToInt32(GameObject.Find("BestScore").GetComponent<TextMesh>().text.Replace("Your best score: ", ""));
        GooglePlayServices.Instance.UpdateRecord(bestScore);
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