﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class GooglePlayServices : MonoBehaviour {

    private bool isFirstTry = true;
    private bool isAuth = false;
    private static GooglePlayServices instance;


    private Hashtable resMap = new Hashtable(){
        {"achievementFirstStep","CgkI8PPD1pcBEAIQAQ"},
        {"achievement1000Point","CgkI8PPD1pcBEAIQAg"},
        {"achievement10000Points","CgkI8PPD1pcBEAIQAw"},
        {"achievement100000Points","CgkI8PPD1pcBEAIQBA"},
        {"achievementHeyStopItd","CgkI8PPD1pcBEAIQBQ"},
        {"leaderboardMoveCrushLeaderboard","CgkI8PPD1pcBEAIQBg"}
    };
    public static GooglePlayServices Instance
    {
        get
        {
            return instance;
        }
    }


    
	void Start () {
        instance = this;
        isAuth = Authenticate();
	}

   

    public void UpdateRecord(string leaderBoardName, int score)
    {
        string name = (string)resMap[leaderBoardName];
        if (isAuth)
        {
            Social.ReportScore(score, name, (bool success) =>
            {

            });
        }
        else
        {
            if (isFirstTry)
            {
                isFirstTry = false;
                Authenticate();
                UpdateRecord(leaderBoardName, score);
            }
        }
    }

    private bool Authenticate()
    {

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        bool res=true;

        Social.localUser.Authenticate((bool success) =>
        {
            res = success;
        });
        return res;
    }

    public void ShowLeaderBoard(string leaderBoardName)
    {
        string name = (string)resMap[leaderBoardName];
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(name);
    }

    public void UpdateAchieveProgress(string achName, double progress)
    {
        string name = (string)resMap[achName];
        progress += GetAchieveProgress(name);
        progress = progress > 100 ? 100 : progress;
        Social.ReportProgress(name, progress, (bool success) =>
        {
        });
    }

    private double GetAchieveProgress(string achName)
    {
        double res = -1;
        Social.LoadAchievements(achievements =>
        {
            if (achievements.Length == 0){
                return;
            }
            foreach (IAchievement achievement in achievements)
            {
                if (achievement.id == achName)
                {
                    res= achievement.percentCompleted;
                }
            }
        });
        return res;
    }

    public void ShowAchievements()
    {
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }


}
