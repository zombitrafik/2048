using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class GooglePlayServices : MonoBehaviour {

    private bool isFirstTry = true;
    private bool isAuth = false;
    private static GooglePlayServices instance;

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

   

    public void UpdateRecord(int score)
    {
        bool res = true;
        if (isAuth)
        {
            Social.ReportScore(score, "CgkI8PPD1pcBEAIQBg", (bool success) =>
            {
                res = success;

            });
        }
        else
        {
            if (isFirstTry)
            {
                isFirstTry = false;
                Authenticate();
                UpdateRecord(score);
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

    public void ShowLeaderBoard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI8PPD1pcBEAIQBg");
    }

}
